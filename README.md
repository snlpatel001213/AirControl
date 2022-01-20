<p align="center">
  <img src="docs/images/logo_original.png" height="200"/>
</p>
<BR>

![GitHub release (latest by date)](https://img.shields.io/badge/Release-0.0.5-orange)
![GitHub repo size](https://img.shields.io/github/repo-size/snlpatel001213/AirControl?style=plastic)
[![Documentation Status](https://readthedocs.org/projects/aircontrol/badge/?version=master)](https://aircontrol.readthedocs.io/en/master/?badge=master)
![GitHub issues](https://img.shields.io/github/issues/snlpatel001213/AirControl)
![GitHub pull requests](https://img.shields.io/github/issues-pr-raw/snlpatel001213/Aircontrol)
![GitHub closed pull requests](https://img.shields.io/github/issues-pr-closed-raw/snlpatel001213/aircontrol)
![GitHub commit activity](https://img.shields.io/github/commit-activity/m/snlpatel001213/AirControl)
![visitors](https://visitor-badge.glitch.me/badge?page_id=snlpatel001213.visitor-badge.issue.1)
[![Gitter](https://badges.gitter.im/Aircontrol-chat/community.svg)](https://gitter.im/Aircontrol-chat/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)
 
# Welcome to Aircontrol

**AirControl is an Open Source, Modular, Cross-Platform, and Extensible Flight Simulator For Deep Learning Research.** Airsim offers a realistic simulation experience with a variety of airplanes. The Airsim is built on [Unity Game engine](https://unity.com). Following are the salient features of the Aircontrol:

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

## Demo
  
[![AirControl Basic Demo](https://i9.ytimg.com/vi_webp/Lhwb4UVulMs/mqdefault.webp?v=61d6839b&sqp=CIiG2o4G&rs=AOn4CLAEMOJnr4S9jciZ-CBKH26yIQivow)](https://www.youtube.com/watch?v=Lhwb4UVulMs)


## Control Inputs

![Keyboard Mappings](docs/images/keyboard-layout.png)
<details close>
<summary></summary>

1. To change the keyboard layout mapping manual, refer to : [keyboard-layout-editor](http://www.keyboard-layout-editor.com/#/gists/53ac4d66840c459705d8bf8637341060)
2. Export the Layout as PNG and replace the file  `AirControl/docs/images/keyboard-layout.png`
3. Submit a pull request

</details>

## Getting started
1. Windows - Not Tested
   1. Download Binaries - https://github.com/snlpatel001213/AirControl/releases
   2. Build it
2. Linux - Tested
   1. Download Binaries - https://github.com/snlpatel001213/AirControl/releases
   2. Build it
3. macOS - Not Tested
   1. Download Binaries - https://github.com/snlpatel001213/AirControl/releases
   2. Build it

## Documentation

1. AirControl Documentation : https://aircontrol.readthedocs.io/
2. C#/Python API Documentation - https://snlpatel001213.github.io/AirControl/html/index.html

## Future Release
Refer to the Project page for the future release, features and bug tracking : https://github.com/snlpatel001213/AirControl/projects/1
![Projects Tab Mappings](docs/images/projects_tab.png)

## Tools and Technology

<div align="center">
        <img src="docs/images/Unity_logo.png" width="10%"/>
        <img src="docs/images/Python_logo.png" width="10%"/>
        <img src="docs/images/CSharp_logo.png" width="10%"/>

</div>

## Research Inspirations
  
**Feel free to list your research topic you love to work on over here. Collaborate and publish.**
  
1. On January 15, 2009, US Airways Flight 1549, an Airbus A320 on a flight from New York City's LaGuardia Airport to Charlotte, North Carolina, struck a flock of birds shortly after take-off, losing all engine power. Unable to reach any airport for an emergency landing due to their low altitude, pilots Chesley "Sully" Sullenberger and Jeffrey Skiles glided the plane to a ditching in the Hudson River off Midtown Manhattan. **Well! Sully did the best but Could we have a different outcome, If Reinforcement learning was controlling this plane?**. [Reference](https://en.wikipedia.org/wiki/US_Airways_Flight_1549)

## Contribute
If you are keen to contribute to Aircontrol, refer to the contribution guide - `CONTRIBUTING.md`.


