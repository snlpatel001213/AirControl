# Contributing

When contributing to this repository, please first discuss the change you wish to make via issue,
email, or any other method with the owners of this repository before making a change. 

Please note we have a code of conduct,please follow it in all your interactions with the project.

## Pull Request Process

1. If you have changed the version of the project update the same at the following locations
   1. file `VERSION` at the root
   2. file `Assets/AirplanePhysics/Code/commons/CommonConfigs.cs` in the `GET_VERSION()` function
   3. Change `PROJECT_NUMBER` in `docs/doxygen.conf file`
   4. Change `version` in `snap/snap/snapcraft.yaml`
   5. Chnage version at `Python/src/airctrl/__init__.py`
2. Make sure if any third-party plugins are used then it also supports open usage and compile for following Platform.
    1. Windows
    2. Mac 
    3. Ubuntu
    4. Headless Ubuntu
    5. WebGl (Discontunied from 1.5.0 release, due to complexity webGL no longer render the project)
3. Make sure if any third-party plugins are used then Windows, Linux, and MacOS build are tested manually.
4. Make sure to add a docstring to any new python and C# function.
5. update requirements.txt file for any additional python requirement
6. Update the README.md with details of changes to the interface, this includes new environment 
   variables, exposed ports, useful file locations, and version parameters.
7. Regenerate python documentation if any changes done to python API code
   1. Go to `docs` Directory `cd docs`
   2. Build documentation `make html -b coverage` 
8. Regenerate C#/Python API documentation
   1. Go to `docs` Directory `cd docs`
   2. Build API documentation `doxygen  doxygen.conf` 
9. You may merge the Pull Request in once you have the sign-off of two other developers, or if you 
   do not have permission to do that, you may request the second reviewer to merge it for you.

# Setting Up VScode with Unity 

This is not an exhaustive guide but just listing key steps

- Download and install dotnet sdk 
      
   ``` 
      wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
      sudo dpkg -i packages-microsoft-prod.deb`
      Run Update and also install tool for HTTPS support
      sudo apt update
      sudo apt install -y apt-transport-https
      sudo apt install dotnet-sdk-6.0
      sudo apt install mono-complete
   ```

- `sudo bash dotnet-install.sh`
- Download mono from here : https://www.monodevelop.com/download/
- Set appropriate dotnet path ( use command `find / -name dotnet` to get it located  ): in my case it was - `/home/<user>/.dotnet/`
- add dotnet to bashrc `export PATH="/home/.dotnet/:$PATH"`
- Set apparopriate mono path  ( use command `which mono` to get it located ) : in my case it was `/usr/bin/mono`
- Use C# extension - 
- use following settings in the `~/.config/Code/User`
   ```
      "omnisharp.enableEditorConfigSupport": true,
      "omnisharp.useModernNet": false,
      "omnisharp.dotnetPath": "/usr/bin/dotnet",
      "omnisharp.disableMSBuildDiagnosticWarning": true,
      "omnisharp.path": "latest",
      "omnisharp.useGlobalMono": "always",
      "csharp.inlayHints.parameters.enabled": true,
      "omnisharp.enableImportCompletion": true,
      "omnisharp.useEditorFormattingSettings": false,
      "omnisharp.sdkIncludePrereleases": true,
      "csharp.referencesCodeLens.enabled": false,
      "workbench.colorTheme": "eppz!",
      "csharp.suppressDotnetRestoreNotification": true,
      "[csharp]": {
    
      },
      "omnisharp.monoPath": "/usr/lib/mono",
      "omnisharp.organizeImportsOnFormat": true,
   ```
- You may also require lib ssl with unity 2021
- wget http://archive.ubuntu.com/ubuntu/pool/main/o/openssl1.0/libssl1.0.0_1.0.2n-1ubuntu5.9_amd64.deb
- sudo dpkg -i libssl1.0.0_1.0.2n-1ubuntu5.9_amd64.deb

**On suceesful load of mono and dotnet the output on the omnisharp log would look like this** 

   ```
   Starting OmniSharp server at 8/6/2022, 6:28:18 pm
      Target: /home/supatel/Games/AirControl_2021/AirControl_2021.sln

   OmniSharp server started with Mono 6.12.0 (/usr/lib/mono).
      Path: /home/supatel/.vscode/extensions/ms-dotnettools.csharp-1.25.0-linux-x64/.omnisharp/1.39.1-beta.2/omnisharp/OmniSharp.exe
      PID: 108301

   [info]: OmniSharp.Stdio.Host
         Starting OmniSharp on ubuntu 20.4 (x64)
   [info]: OmniSharp.Services.DotNetCliService
         Checking the 'DOTNET_ROOT' environment variable to find a .NET SDK
   [info]: OmniSharp.Services.DotNetCliService
         Using the 'dotnet' on the PATH.
   [info]: OmniSharp.Services.DotNetCliService
         DotNetPath set to dotnet
   [info]: OmniSharp.MSBuild.Discovery.MSBuildLocator
         Located 1 MSBuild instance(s)
               1: Mono 16.10.1 - "/usr/lib/mono/msbuild/Current/bin"
   [warn]: OmniSharp.CompositionHostBuilder
         It looks like you have Mono installed which contains a MSBuild lower than 17.0.0 which is the minimum supported by the configured .NET Core Sdk.
   Try updating Mono to the latest stable or preview version to enable better .NET Core Sdk support.
   [info]: OmniSharp.MSBuild.Discovery.MSBuildLocator
         Registered MSBuild instance: Mono 16.10.1 - "/usr/lib/mono/msbuild/Current/bin"
   [info]: OmniSharp.WorkspaceInitializer
         Invoking Workspace Options Provider: OmniSharp.Roslyn.CSharp.Services.CSharpFormattingWorkspaceOptionsProvider, Order: 0
   [info]: OmniSharp.Cake.CakeProjectSystem
         Detecting Cake files in '/home/supatel/Games/AirControl_2021'.
   [info]: OmniSharp.Cake.CakeProjectSystem
         Did not find any Cake files
   [info]: OmniSharp.MSBuild.ProjectSystem
         Detecting projects in '/home/supatel/Games/AirControl_2021/AirControl_2021.sln'.
   [info]: OmniSharp.MSBuild.ProjectManager
         Queue project update for '/home/supatel/Games/AirControl_2021/XCharts.Examples.Runtime.csproj'
   [info]: OmniSharp.MSBuild.ProjectManager
         Queue project update for '/home/supatel/Games/AirControl_2021/XCharts.Runtime.csproj'
   [info]: OmniSharp.MSBuild.ProjectManager
         Queue project update for '/home/supatel/Games/AirControl_2021/Assembly-CSharp.csproj'
   [info]: OmniSharp.MSBuild.ProjectManager
         Queue project update for '/home/supatel/Games/AirControl_2021/XCharts.Editor.csproj'
   [info]: OmniSharp.MSBuild.ProjectManager
         Queue project update for '/home/supatel/Games/AirControl_2021/Assembly-CSharp-Editor.csproj'
   [info]: OmniSharp.Script.ScriptProjectSystem
         Detecting CSX files in '/home/supatel/Games/AirControl_2021'.
   [info]: OmniSharp.MSBuild.ProjectManager
         Loading project: /home/supatel/Games/AirControl_2021/XCharts.Examples.Runtime.csproj
   [info]: OmniSharp.Script.ScriptProjectSystem
         Did not find any CSX files
   [info]: OmniSharp.WorkspaceInitializer
         Configuration finished.
   [info]: OmniSharp.Stdio.Host
         Omnisharp server running using Stdio at location '/home/supatel/Games/AirControl_2021' on host 107375.
   [info]: OmniSharp.MSBuild.ProjectManager
         Successfully loaded project file '/home/supatel/Games/AirControl_2021/XCharts.Examples.Runtime.csproj'.
   [info]: OmniSharp.MSBuild.ProjectManager
         Adding project '/home/supatel/Games/AirControl_2021/XCharts.Examples.Runtime.csproj'
   [info]: OmniSharp.MSBuild.ProjectManager
         Loading project: /home/supatel/Games/AirControl_2021/XCharts.Runtime.csproj
   [info]: OmniSharp.MSBuild.ProjectManager
         Successfully loaded project file '/home/supatel/Games/AirControl_2021/XCharts.Runtime.csproj'.
   [info]: OmniSharp.MSBuild.ProjectManager
         Adding project '/home/supatel/Games/AirControl_2021/XCharts.Runtime.csproj'
   [info]: OmniSharp.MSBuild.ProjectManager
         Loading project: /home/supatel/Games/AirControl_2021/Assembly-CSharp.csproj
   [info]: OmniSharp.MSBuild.ProjectManager
         Successfully loaded project file '/home/supatel/Games/AirControl_2021/Assembly-CSharp.csproj'.
   [info]: OmniSharp.MSBuild.ProjectManager
         Adding project '/home/supatel/Games/AirControl_2021/Assembly-CSharp.csproj'
   [info]: OmniSharp.MSBuild.ProjectManager
         Loading project: /home/supatel/Games/AirControl_2021/XCharts.Editor.csproj
   [info]: OmniSharp.MSBuild.ProjectManager
         Successfully loaded project file '/home/supatel/Games/AirControl_2021/XCharts.Editor.csproj'.
   [info]: OmniSharp.MSBuild.ProjectManager
         Adding project '/home/supatel/Games/AirControl_2021/XCharts.Editor.csproj'
   [info]: OmniSharp.MSBuild.ProjectManager
         Loading project: /home/supatel/Games/AirControl_2021/Assembly-CSharp-Editor.csproj
   [info]: OmniSharp.MSBuild.ProjectManager
         Successfully loaded project file '/home/supatel/Games/AirControl_2021/Assembly-CSharp-Editor.csproj'.
   [info]: OmniSharp.MSBuild.ProjectManager
         Adding project '/home/supatel/Games/AirControl_2021/Assembly-CSharp-Editor.csproj'
   [info]: OmniSharp.MSBuild.ProjectManager
         Update project: XCharts.Examples.Runtime
   [info]: OmniSharp.MSBuild.ProjectManager
         Update project: XCharts.Runtime
   [info]: OmniSharp.MSBuild.ProjectManager
         Update project: Assembly-CSharp
   [info]: OmniSharp.MSBuild.ProjectManager
         Update project: XCharts.Editor
   [info]: OmniSharp.MSBuild.ProjectManager
         Update project: Assembly-CSharp-Editor
   Received response for /v2/getcodeactions but could not find request.
   Received response for /v2/getcodeactions but could not find request.
   Received response for /quickinfo but could not find request.
   Received response for /v2/getcodeactions but could not find request.
   
   ```

## Code of Conduct

### Our Pledge

In the interest of fostering an open and welcoming environment, we as
contributors and maintainers pledge to making participation in our project and
our community a harassment-free experience for everyone, regardless of age, body
size, disability, ethnicity, gender identity and expression, level of experience,
nationality, personal appearance, race, religion, or sexual identity and
orientation.

### Our Standards

Examples of behavior that contributes to creating a positive environment
include:

* Using welcoming and inclusive language
* Being respectful of differing viewpoints and experiences
* Gracefully accepting constructive criticism
* Focusing on what is best for the community
* Showing empathy towards other community members

Examples of unacceptable behavior by participants include:

* The use of sexualized language or imagery and unwelcome sexual attention or
advances
* Trolling, insulting/derogatory comments, and personal or political attacks
* Public or private harassment
* Publishing others' private information, such as a physical or electronic
  address, without explicit permission
* Other conduct which could reasonably be considered inappropriate in a
  professional setting

### Our Responsibilities

Project maintainers are responsible for clarifying the standards of acceptable
behavior and are expected to take appropriate and fair corrective action in
response to any instances of unacceptable behavior.

Project maintainers have the right and responsibility to remove, edit, or
reject comments, commits, code, wiki edits, issues, and other contributions
that are not aligned to this Code of Conduct, or to ban temporarily or
permanently any contributor for other behaviors that they deem inappropriate,
threatening, offensive, or harmful.

### Scope

This Code of Conduct applies both within project spaces and in public spaces
when an individual is representing the project or its community. Examples of
representing a project or community include using an official project e-mail
address, posting via an official social media account, or acting as an appointed
representative at an online or offline event. Representation of a project may be
further defined and clarified by project maintainers.

### Enforcement

Instances of abusive, harassing, or otherwise unacceptable behavior may be
reported by contacting the project team at snlpatel001213(~~at~~)hotmail.com. All
complaints will be reviewed and investigated and will result in a response that
is deemed necessary and appropriate to the circumstances. The project team is
obligated to maintain confidentiality with regard to the reporter of an incident.
Further details of specific enforcement policies may be posted separately.

Project maintainers who do not follow or enforce the Code of Conduct in good
faith may face temporary or permanent repercussions as determined by other
members of the project's leadership.
