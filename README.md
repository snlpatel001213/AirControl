<p align="center">
  <img src="https://github.com/snlpatel001213/AirControl/blob/master/docs/images/logo/banner.png?raw=true" height="100"/>
</p>

<div align="center">

![GitHub release (latest by date)](https://img.shields.io/badge/Release-0.1.5-orange)
![GitHub repo size](https://img.shields.io/github/repo-size/snlpatel001213/AirControl)
![GitHub repo size](https://badgen.net/github/license/micromatch/micromatch)
[![Documentation Status](https://readthedocs.org/projects/aircontrol/badge/?version=master)](https://aircontrol.readthedocs.io/en/master/?badge=master)
![GitHub issues](https://img.shields.io/github/issues/snlpatel001213/AirControl)
![GitHub pull requests](https://img.shields.io/github/issues-pr-raw/snlpatel001213/Aircontrol)
![GitHub closed pull requests](https://img.shields.io/github/issues-pr-closed-raw/snlpatel001213/aircontrol)
![GitHub commit activity](https://img.shields.io/github/commit-activity/m/snlpatel001213/AirControl)
![visitors](https://visitor-badge.glitch.me/badge?page_id=snlpatel001213.visitor-badge.issue.1)
[![Gitter](https://badges.gitter.im/Aircontrol-chat/community.svg)](https://gitter.im/Aircontrol-chat/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)
[![PyPI version](https://badge.fury.io/py/aircontrol-python.svg)](https://badge.fury.io/py/aircontrol-python)

Supported OS

![Apple](https://img.shields.io/badge/iOS-000000?style=for-the-badge&logo=ios&logoColor=white)
![Windows](https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white)
![Linux](https://img.shields.io/badge/Ubuntu-E95420?style=for-the-badge&logo=ubuntu&logoColor=white)

Supported Language

![CSharp](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Python](https://img.shields.io/badge/Python-14354C?style=for-the-badge&logo=python&logoColor=white)
![Jupyter Notebook](https://img.shields.io/badge/jupyter-%23FA0F00.svg?style=for-the-badge&logo=jupyter&logoColor=white)

<!-- ![Python](https://img.shields.io/badge/NVIDIA-GTX1070-76B900?style=for-the-badge&logo=nvidia&logoColor=white) -->

<!-- ![Youtube](https://img.shields.io/badge/YouTube-FF0000?style=for-the-badge&logo=youtube&logoColor=white) -->

</div>
 
# Welcome to Aircontrol

**AirControl is an Open Source, Modular, Cross-Platform, and Extensible Flight Simulator For Deep Learning Research.** AirControl offers a realistic simulation experience with a variety of airplanes. The AirControl is built on [Unity Game engine](https://unity.com). Following are the salient features of the Aircontrol:

* Built with **C#**, it has **Python** API to control it from your favorite Deep learning Framework.
* Complete source code is open on Github.
* Aircontrol takes full advantage of object-oriented programming. Developed fully modular from day one. You can easily introduce new features such as **vertical takeoff**. you can bring your own **alien plane to AirCotrol**. 
* AirControl is truly cross-platform, can be compiled on Linux, macOS, and Windows. Binary will be released for all the platforms.
* The AirControl uses Nvidia [Physx](https://en.wikipedia.org/wiki/PhysX) for the best possible Newtonian physics simulation.
* Aircontrol allows users to take advantage of aerodynamics effects such as [Ground effect](https://en.wikipedia.org/wiki/Ground_effect_(aerodynamics)).
* All the control surfaces (Throttle, Rudder, Ailerons, and Flaps) accepts normalized input between -1 and 1. This makes Aircontrol even more friendly with AI.

## System Requirement
It depends on how big your Unity Environment is. The environmant which comes with the Aircontrol binary releases is the basic one and tested with following config:

- Operating system : Ubuntu, Windows, Mac
- CPU: Intel Core i7
- GPU: Nvidia 1070 or Higher
- RAM: 16 GB
- Flight Controller: xbox [WIP] expected release v0.0.7

Aircontrol may work with lower than the specified requirements, but its not tested.

You can run Aircontrol in server-client mode in two different machines or both in the single machine.

<p align="center">
  <img src="https://github.com/snlpatel001213/AirControl/blob/master/docs/images/FrontImage.png?raw=true" height="400"/>
</p>

## Control Inputs

![Keyboard Mappings](https://github.com/snlpatel001213/AirControl/blob/master/docs/images/keyboard-layout.png?raw=true)
<details close>
<summary></summary>

1. To change the keyboard layout mapping manual, refer to : [keyboard-layout-editor](http://www.keyboard-layout-editor.com/#/gists/53ac4d66840c459705d8bf8637341060)
2. Export the Layout as PNG and replace the file  `AirControl/docs/images/keyboard-layout.png`
3. Submit a pull request

</details>

## Getting started

Version number (MAJOR.MINOR.PATCH) follows Semantic Versioning 2.0.0. Version numbers change as follows:
1. MAJOR version when making incompatible API changes
2. MINOR version when adding API in a backward-compatible manner
3. PATCH version when making backward-compatible bug fixes
Note that semantic versioning also applies to the Pypi release, Git release and Snap releases.

```mermaid
%%{init: {'theme': 'base', 'themeVariables': { 'primaryColor': '#ffcccc', 'edgeLabelBackground':'#ffffee', 'tertiaryColor': '#fff0f0'}}}%%
graph TD
    A[Skills] --> C[Not Familier with Unity Engine]
    C --> C_1[Use Binaries]
    C_1 --> D[Windows]
    C_1 --> E[Ubuntu]
    C_1 --> F[Mac]
    A[Skills] --> B[Familier with Unity Engine]
    B --> H[Wants to Add/Edit Airplane/Assets etc..]
    H --> I[yes]
    H --> J[No]
    I --> K[Build from source]
    J --> C_1
```

1. Windows - Tested
   1. Download Binaries - https://github.com/snlpatel001213/AirControl/releases
   2. Build it from source
2. Linux - Tested
   1. Download Binaries - https://github.com/snlpatel001213/AirControl/releases
   2. Build it from source
3. macOS - Not Tested [`Need Contributors`]
   1. Download Binaries - https://github.com/snlpatel001213/AirControl/releases
   2. Build it from source

Pypi Release-Alpha [![PyPI version](https://badge.fury.io/py/aircontrol-python.svg)](https://badge.fury.io/py/aircontrol-python) : https://pypi.org/project/aircontrol-python/
```bash
pip install aircontrol-python==<LatestVersion>
```
Snap Release-Alpha (May face issue with capturing controls)
```bash
snap install aircontrol
```

## Documentation

1. AirControl Documentation : https://aircontrol.readthedocs.io/
2. C#/Python API Documentation - https://snlpatel001213.github.io/AirControl/html/index.html

## Future Release
Refer to the Project page for the future release, features and bug tracking : https://github.com/snlpatel001213/AirControl/projects/1
![Projects Tab Mappings](https://github.com/snlpatel001213/AirControl/blob/master/docs/images/projects_tab.png)

## Tools and Technology

<div align="center">
        <img src="https://github.com/snlpatel001213/AirControl/blob/master/docs/images/Unity_logo.png?raw=true" width="10%"/>
        <img src="https://github.com/snlpatel001213/AirControl/blob/master/docs/images/Python_logo.png?raw=true" width="10%"/>
        <img src="https://github.com/snlpatel001213/AirControl/blob/master/docs/images/CSharp_logo.png?raw=true" width="10%"/>

</div>

## Research Inspirations
  
**Feel free to list your research topic you love to work on over here. Collaborate and publish.**
  
1. On January 15, 2009, US Airways Flight 1549, an Airbus A320 on a flight from New York City's LaGuardia Airport to Charlotte, North Carolina, struck a flock of birds shortly after take-off, losing all engine power. Unable to reach any airport for an emergency landing due to their low altitude, pilots Chesley "Sully" Sullenberger and Jeffrey Skiles glided the plane to a ditching in the Hudson River off Midtown Manhattan. **Well! Sully did the best but Could we have a different outcome, If Reinforcement learning was controlling this plane?**. [Reference](https://en.wikipedia.org/wiki/US_Airways_Flight_1549)

## Contribute
We love your input! We want to make contributing to AirControl as easy and transparent as possible. Please see our Contributing Guide `CONTRIBUTING.md` to get started. Thank you to all our contributors!
### Current Contributors
<a href = "https://github.com/snlpatel001213/AirControl/graphs/contributors">
<img src = "https://contrib.rocks/image?repo=snlpatel001213/AirControl"/>
</a>


