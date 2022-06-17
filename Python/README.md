  <p align="center">
  <img src="https://github.com/snlpatel001213/AirControl/blob/master/docs/images/logo/banner.png?raw=true" height="100"/>
</p>

<div align="center">

![GitHub release (latest by date)](https://img.shields.io/badge/Release-1.5.0-orange)
![GitHub repo size](https://img.shields.io/github/repo-size/snlpatel001213/AirControl)
![GitHub repo size](https://badgen.net/github/license/micromatch/micromatch)
[![Documentation Status](https://readthedocs.org/projects/aircontrol/badge/?version=master)](https://aircontrol.readthedocs.io/en/master/?badge=master)
![GitHub issues](https://img.shields.io/github/issues/snlpatel001213/AirControl)
![GitHub pull requests](https://img.shields.io/github/issues-pr-raw/snlpatel001213/Aircontrol)
![GitHub closed pull requests](https://img.shields.io/github/issues-pr-closed-raw/snlpatel001213/aircontrol)
![GitHub commit activity](https://img.shields.io/github/commit-activity/m/snlpatel001213/AirControl)
![visitors](https://visitor-badge.glitch.me/badge?page_id=snlpatel001213.visitor-badge.issue.1)
[![Gitter](https://badges.gitter.im/Aircontrol-chat/community.svg)](https://gitter.im/Aircontrol-chat/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)
[![PyPI version](https://badge.fury.io/py/airctrl.svg)](https://badge.fury.io/py/aircontrol-python)
[![aircontrol](https://snapcraft.io/aircontrol/badge.svg)](https://snapcraft.io/aircontrol)

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
 
# Welcome to AirControl

**AirControl is an Open Source, Modular, Cross-Platform, and Extensible Flight Simulator For Deep Learning Research.** AirControl offers a realistic simulation experience with a variety of airplanes. The AirControl is built on [Unity Game engine](https://unity.com). Following are the salient features of the AirControl:

* Built with **C#**, it has **Python** API to control it from your favourite Deep learning Framework.
* Complete source code is open on Github.
* Aircontrol takes full advantage of object-oriented programming. It is developed fully modular from day one. You can easily introduce new features such as **vertical takeoff**. You can bring your own **alien plane to AirCotrol**. 
* AirControl is truly cross-platform and can be compiled on Linux, macOS, and Windows. Binary will be released for all the platforms.
* AirControl uses Nvidia [Physx](https://en.wikipedia.org/wiki/PhysX) for the best possible Newtonian physics simulation.
* AirControl allows users to take advantage of aerodynamic effects such as [Ground effect](https://en.wikipedia.org/wiki/Ground_effect_(aerodynamics)).
* All the control surfaces (Throttle, Rudder, Ailerons, and Flaps) accept normalized input between -1 and 1. This makes AirControl even more friendly with AI.
* Airplane behavior can be finetuned using a configuration file.
* Available for Linux, Windows, Mac, and Linux headless mode.
* Support simultaneous multiple instances at different ports

## Available Airplane

Two airplanes are avialble F4UCorsair and Cessna152. F4UCorsair about 3X in size and weight compared to Cessna152.
<p align="center">
  <img src="docs/images/Airplanes.png" height="400"/>
</p>

## System Requirement
It depends on how big your Unity environment is. The environment which comes with the AirControl binary releases is the **basic** one and tested with the following config:

- Operating System: Ubuntu, Windows, Mac
- CPU: Intel Core i7
- GPU: Nvidia 1070 or Higher
- RAM: 16 GB

AirControl may work with lower than the specified requirements, but it's not tested.

You can run AirControl in server-client mode with two different machines or both in a single machine.

<p align="center">
  <img src="https://github.com/snlpatel001213/AirControl/blob/master/docs/images/FrontImage.png?raw=true" height="400"/>
</p>

## Interfaces

### Controlling with python
AirControl can be fully controlled through python. Along with flight other functions like lidar range, capture camera, scene camera, level reset, host port setting, etc.. can be controlled through python.


| Sr. No.  |  Client Example | Details |
|---|---|---|
| 1 |[Input Output Python API](https://github.com/snlpatel001213/AirControl/blob/master/Python/client_examples/input_output_API.ipynb)|Commanding AirControl through python|
| 2 |[Reinforcment Learning Flight loop](https://github.com/snlpatel001213/AirControl/blob/master/Python/client_examples/flight_loop.ipynb)|Example reinforcement learning flight loop|
| 3 |[Finetuning Airplane](https://github.com/snlpatel001213/AirControl/blob/master/Python/client_examples/flight_loop.ipynb)|Finetune flight experience|
| 4 |[Launch from command line](https://github.com/snlpatel001213/AirControl/blob/master/Python/client_examples/launch_from_python.ipynb)|Launching Aircontrol from command line|
| 5 |[Launch from python](https://github.com/snlpatel001213/AirControl/blob/master/Python/client_examples/launch_from_python.ipynb)|Launching Aircontrol from Python|
| 6 | [Primitive API](https://github.com/snlpatel001213/AirControl/blob/master/Python/client_examples/primitive_API.ipynb)   |  Simple Client to interact with the server. It does not require the AirControl Pypi package. Just for unit tests, not for long runs |
| 7 | [Primitive API - 2](https://github.com/snlpatel001213/AirControl/blob/master/Python/client_examples/primitive_API_2.ipynb)   |  Simple Client to interact with server. More detailed than the previous one. An end-to-end flight loop is demonstrated. It does not require the AirControl Pypi package. Just for unit tests, not for long runs |
| 8 | [Lidar Controls](https://github.com/snlpatel001213/AirControl/blob/master/Python/client_examples/lidar_API.ipynb)   |  Demonstrate how to control lidar from the python client.|
| 9 | [Camera Controls](https://github.com/snlpatel001213/AirControl/blob/master/Python/client_examples/camera_API.ipynb)   |  Demonstrate how to control Camera from the python client. It allows switching the camera. It allows for capturing Depth, Semantic segmentation, Object segmentation, and Optical flow variant of the scene.|
| 10 | [Time of Day Controls](https://github.com/snlpatel001213/AirControl/blob/master/Python/client_examples/time_of_day_API.ipynb)   | Allows controlling the time of day and light conditions. It allows controlling sun position based on Longitude, Latitude, Hour, and Minutes.|
| 11 | [UI and Audio Controls](https://github.com/snlpatel001213/AirControl/blob/master/Python/client_examples/other_API.ipynb)   | Allows controlling the visibility of Airplane control on UI and Airplane Audio.|


### Controlling with keyboard

![Keyboard Mappings](https://github.com/snlpatel001213/AirControl/blob/master/docs/images/keyboard-layout.png?raw=true)
<details close>
<summary></summary>

1. To change the keyboard layout mapping manual, refer to [keyboard-layout-editor](http://www.keyboard-layout-editor.com/#/gists/53ac4d66840c459705d8bf8637341060)
2. Export the Layout as PNG and replace the file  `AirControl/docs/images/keyboard-layout.png`
3. Submit a pull request

</details>

## Getting started
Getting started with Aircontrol is very simple. If you are a beginner with Unity use the build provided for different OS. If you are a Unity pro use provided unity package. 

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
   2. Build it from the source
2. Linux - Tested
   1. Download Binaries - https://github.com/snlpatel001213/AirControl/releases
   2. Build it from the source
3. macOS - Not Tested [`Need Contributors`]
   1. Download Binaries - https://github.com/snlpatel001213/AirControl/releases
   2. Build it from the source

**Python Package Installation**
```bash
  cd Python
  Python3 setup.py install
  OR 
  Use relative imports
```
**Pypi Release-Alpha** `Not Tested` [![PyPI version](https://badge.fury.io/py/airctrl.svg)](https://badge.fury.io/py/airctrl) : https://pypi.org/project/aircontrol-python/
```
  pip install airctrl
```

**Snap Release-Alpha** [![Get it from the Snap Store](https://snapcraft.io/static/images/badges/en/snap-store-black.svg)](https://snapcraft.io/aircontrol)

1. Install: `snap install aircontrol
2. Invoke: `aircontrol.AirControl`
3. Fly manually or use python API.


## Documentation
1. AirControl Documentation: https://aircontrol.readthedocs.io/
2. C#/Python API Documentation - https://snlpatel001213.github.io/AirControl/html/index.html

## Future Release
Refer to the Project page for the future release, features, and bug tracking: https://github.com/snlpatel001213/AirControl/projects/1
![Projects Tab Mappings](https://github.com/snlpatel001213/AirControl/blob/master/docs/images/projects_tab.png)

## Tools and Technology

<div align="center">
        <img src="https://github.com/snlpatel001213/AirControl/blob/master/docs/images/Unity_logo.png?raw=true" width="10%"/>
        <img src="https://github.com/snlpatel001213/AirControl/blob/master/docs/images/Python_logo.png?raw=true" width="10%"/>
        <img src="https://github.com/snlpatel001213/AirControl/blob/master/docs/images/CSharp_logo.png?raw=true" width="10%"/>

</div>

## Contribute
We love your input! We want to make contributing to AirControl as easy and transparent as possible. Please see our Contributing Guide `CONTRIBUTING.md` to get started. Thank you to all our contributors!
[![Open in Gitpod](https://gitpod.io/button/open-in-gitpod.svg)](https://gitpod.io/#https://github.com/snlpatel001213/AirControl)

### Current Contributors
<a href = "https://github.com/snlpatel001213/AirControl/graphs/contributors">
<img src = "https://contrib.rocks/image?repo=snlpatel001213/AirControl"/>
</a>
  
