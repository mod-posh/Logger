$script:ProjectName = "Logger"; # The name of your C# Project
$script:DotnetVersion = "net7.0"; # The version of .Net the project is targeted to
$script:GithubOrg = 'mod-posh' # This could be your github username if you're not working in a Github Org
$script:Repository = "https://github.com/$($script:GithubOrg)"; # This is the Github Repo
$script:DeployBranch = 'main'; # The branch that we deploy from, typically master or main
$script:Root = $PSScriptRoot; # This will be the root of your Module Project, not the Repository Root
$script:Source = Join-Path $PSScriptRoot $script:ProjectName; # This will be the root of your Module Project, not the Repository Root
$script:Output = Join-Path $PSScriptRoot 'output'; # The module will be output into this folder
$script:Docs = Join-Path $PSScriptRoot 'docs'; # The root folder for the PowerShell Module
$script:TestFile = ("TestResults_$(Get-Date -Format s).xml").Replace(':', '-'); # The Pester Test output file
$script:DiscordChannel = "https://discord.com/channels/1044305359021555793/1044305781627035811" # Discord Channel
$script:PoshGallery = "https://www.nuget.org/packages/$($script:ProjectName)"        # The PowerShell Gallery URL
$script:Fwlink = "https://raw.githubusercontent.com/$($script:GithubOrg)/$($script:ProjectName)/main/cabs/"

$PowerShellForGitHub = Get-Module -ListAvailable | Where-Object -Property Name -eq PowerShellForGitHub;
if ($PowerShellForGitHub)
{
 Write-Host -ForegroundColor Blue "Info: PowerShellForGitHub Version $($PowerShellForGitHub.Version) Found";
 Write-Host -ForegroundColor Blue "Info: This automation built with PowerShellForGitHub Version 0.16.1";
 Import-Module PowerShellForGitHub;
}
else
{
 throw "Please Install-Module -Name PowerShellForGitHub";
}

Write-Host -ForegroundColor Green "ProjectName    : $($script:ProjectName)";
Write-Host -ForegroundColor Green "DotnetVersion  : $($script:DotnetVersion)";
Write-Host -ForegroundColor Green "GithubOrg      : $($script:GithubOrg)";
Write-Host -ForegroundColor Green "Root           : $($script:Root)";
Write-Host -ForegroundColor Green "Source         : $($script:Source)";
Write-Host -ForegroundColor Green "Output         : $($script:Output)";
Write-Host -ForegroundColor Green "Docs           : $($script:Docs)";
Write-Host -ForegroundColor Green "TestFile       : $($script:TestFile)";
Write-Host -ForegroundColor Green "Repository     : $($script:Repository)";
Write-Host -ForegroundColor Green "DiscordChannel : $($script:DiscordChannel)";
Write-Host -ForegroundColor Green "PoshGallery    : $($script:PoshGallery)";
Write-Host -ForegroundColor Green "DeployBranch   : $($script:DeployBranch)";
Write-Host -ForegroundColor Green "FwLink         : $($script:Fwlink)";

Task default -depends LocalUse
Task LocalUse -Description "Setup for local use and testing" -depends Clean, BuildProject
Task Build -depends LocalUse, TestProject
Task Package -depends UpdateReadme, CreateDocumentation, PackageProject
Task Deploy -depends CheckBranch, ReleaseNotes, PublishProject, NewTaggedRelease, Post2Discord, Post2Bluesky

Task Clean -depends CleanProject {
 $null = Remove-Item $script:Output -Recurse -ErrorAction Ignore
 $null = New-Item -Type Directory -Path $script:Output -ErrorAction Ignore
 $null = New-Item -Type Directory -Path $script:Docs -ErrorAction Ignore
 $null = Remove-Item "$($script:Root)\TestResults*.xml"
}

Task UpdateReadme -Description "Update the README file" -Action {
 $readMe = Get-Item .\README.md

 $TableHeaders = "| Latest Version | Nuget.org | Issues | License | Discord |"
 $Columns = "|-----------------|----------------|----------------|----------------|----------------|"
 $VersionBadge = "[![Latest Version](https://img.shields.io/github/v/tag/$($script:GithubOrg)/$($script:ProjectName))]($($script:Repository)/$($script:ProjectName)/tags)"
 $GalleryBadge = "[![Nuget.org](https://img.shields.io/nuget/dt/$($script:ProjectName))](https://www.nuget.org/packages/$($script:ProjectName))"
 $IssueBadge = "[![GitHub issues](https://img.shields.io/github/issues/$($script:GithubOrg)/$($script:ProjectName))]($($script:Repository)/$($script:ProjectName)/issues)"
 $LicenseBadge = "[![GitHub license](https://img.shields.io/github/license/$($script:GithubOrg)/$($script:ProjectName))]($($script:Repository)/$($script:ProjectName)/blob/master/LICENSE)"
 $DiscordBadge = "[![Discord Server](https://assets-global.website-files.com/6257adef93867e50d84d30e2/636e0b5493894cf60b300587_full_logo_white_RGB.svg)]($($script:DiscordChannel))"

 Write-Output $TableHeaders | Out-File $readMe.FullName -Force
 Write-Output $Columns | Out-File $readMe.FullName -Append
 Write-Output "| $($VersionBadge) | $($GalleryBadge) | $($IssueBadge) | $($LicenseBadge) | $($DiscordBadge) |" | Out-File $readMe.FullName -Append
}

Task NewTaggedRelease -Description "Create a tagged release" -Action {
 $Github = (Get-Content -Path "$($PSScriptRoot)\github.json") | ConvertFrom-Json
 $Credential = New-Credential -Username ignoreme -Password $Github.Token
 Set-GitHubAuthentication -Credential $Credential
 $Project = [xml](Get-Content -Path "$($script:Source)\$($script:ProjectName).csproj");
 $Version = $Project.Project.PropertyGroup.Version.ToString();
 git add .
 git commit . -m "$($script:ProjectName) $($Version) Release"
 git push
 git tag -a v$version -m "$($script:ProjectName) Version $($Version)"
 git push origin v$version
 New-GitHubRelease -OwnerName $script:GithubOrg -RepositoryName $script:ProjectName -Tag "v$($Version)" -Name "v$($Version)"
}

Task Post2Discord -Description "Post a message to discord" -Action {
 $Project = [xml](Get-Content -Path "$($script:Source)\$($script:ProjectName).csproj");
 $Version = $Project.Project.PropertyGroup.Version.ToString();
 $Discord = Get-Content -Path "$($PSScriptRoot)\discord.json" | ConvertFrom-Json
 $Discord.message.content = "Version $($version) of $($script:ProjectName) released. Please visit Github ($($script:Repository)/$($script:ProjectName)) or PowershellGallery ($($script:PoshGallery)) to download."
 Invoke-RestMethod -Uri $Discord.uri -Body ($Discord.message | ConvertTo-Json -Compress) -Method Post -ContentType 'application/json; charset=UTF-8'
}

Task Post2Bluesky -Description "Post a message to bsky.app" -Action {
 $Project = [xml](Get-Content -Path "$($script:Source)\$($script:ProjectName).csproj");
 $Version = $Project.Project.PropertyGroup.Version.ToString();
 $createdAt = Get-Date -Format "yyyy-MM-ddTHH:mm:ss.ffffffZ"
 # Authenticate
 $AuthBody = Get-Content -Path "$($PSScriptRoot)\bluesky.json"
 $Handle = $AuthBody | ConvertFrom-Json | Select-Object -ExpandProperty Identifier
 $Headers = @{}
 $Headers.Add('Content-Type', 'application/json')
 $Response = Invoke-RestMethod -Uri "https://bsky.social/xrpc/com.atproto.server.createSession" -Method Post -Body $AuthBody -Headers $Headers
 # Create post
 $Headers.Add('Authorization', "Bearer $($Response.accessJwt)")
 $Record = New-Object -TypeName psobject -Property @{
  '$type'     = "app.bsky.feed.post"
  'text'      = "Version $($version) of $($script:ProjectName) released. Please visit Github ($($script:Repository)/$($script:ProjectName)) or PowershellGallery ($($script:PoshGallery)) to download."
  "createdAt" = $createdAt
 }
 $Post = New-Object -TypeName psobject -Property @{
  'repo'       = $Handle
  'collection' = 'app.bsky.feed.post'
  record       = $Record
 }

 Invoke-RestMethod -Uri "https://bsky.social/xrpc/com.atproto.repo.createRecord" -Method Post -Body ($Post | ConvertTo-Json -Compress) -Headers $Headers
}

Task CleanProject -Description "Clean the project before building" -Action {
 dotnet clean "$($script:Source)\$($script:ProjectName).sln" -c Release
 dotnet clean "$($script:Source)\$($script:ProjectName).sln" -c Debug
}

Task CreateDocumentation -Description "Create Docs" -Action {
 defaultdocumentation -a "$($script:Source)\bin\Release\$($script:DotnetVersion)\$($script:ProjectName).dll" -o $script:Docs
}

Task BuildProject -Description "Build the project" -Action {
 dotnet build "$($PSScriptRoot)\$($script:ProjectName)\$($script:ProjectName).csproj" -c Release
}

Task CheckBranch -Description "A test that should fail if we deploy while not on master" -Action {
 $branch = git branch --show-current
 if ($branch -ne $script:DeployBranch)
 {
  [System.Net.WebSockets.WebSocketException]$Exception = "You are not on the deployment branch: $($script:DeployBranch)"
  [string]$ErrorId = "Git.WrongBranch"
  [System.Management.Automation.ErrorCategory]$Category = [System.Management.Automation.ErrorCategory]::InvalidOperation
  $PSCmdlet.ThrowTerminatingError(
   [System.Management.Automation.ErrorRecord]::new(
    $Exception,
    $ErrorId,
    $Category,
    $null
   )
  )
 }
}

Task ReleaseNotes -Description "Create release notes file for project" -Action {
 $Github = (Get-Content -Path "$($PSScriptRoot)\github.json") | ConvertFrom-Json
 $Credential = New-Credential -Username ignoreme -Password $Github.Token
 Set-GitHubAuthentication -Credential $Credential
 $Milestone = (Get-GitHubMilestone -OwnerName 'mod-posh' -RepositoryName 'Pipelines' -State Closed | Sort-Object -Descending -Property Number)[0]
 if ($Milestone)
 {
  [System.Text.StringBuilder]$stringbuilder = [System.Text.StringBuilder]::new()
  [void]$stringbuilder.AppendLine( "# $($Milestone.title)" )
  if ($Milestone.description)
  {
   [void]$stringbuilder.AppendLine( "$($Milestone.description)" )
  }
  $i = Get-GitHubIssue -OwnerName $script:GithubOrg -RepositoryName $script:ProjectName -RepositoryType All -Filter All -State Closed -MilestoneNumber $Milestone.Number;
  $headings = $i | ForEach-Object { $_.Labels.Name } | Sort-Object -Unique;
  foreach ($heading in $headings)
  {
   [void]$stringbuilder.AppendLine( "" )
   [void]$stringbuilder.AppendLine( "## $($heading.ToUpper())" )
   [void]$stringbuilder.AppendLine( "" )
   $issues = $i | ForEach-Object { if ($_.Labels.Name -eq $Heading) { $_ } }
   foreach ($issue in $issues)
   {
    [void]$stringbuilder.AppendLine( "* $($issue.title) #$($issue.issuenumber)" )
   }
  }
  Out-File -FilePath "$($PSScriptRoot)\RELEASE.md" -InputObject $stringbuilder.ToString() -Encoding ascii -Force
 }
}

Task TestProject -Description "Test project" -Action {
 dotnet test $script:Source\$script:ProjectName.sln --logger "trx;LogFileName=$($script:Root)\$($script:TestFile)"
}

Task PackageProject -Description "Package the project" -Action {
 dotnet pack $script:Source\$script:ProjectName.sln -o $script:Output -c Release
}

Task PublishProject -Description "Publish project to Nuget.org" -Action {
 dotnet nuget push $script:Output
}