import json
import socket
import numpy as np

INPUT_CONTROL_TYPE = ["Code", "Other"]


def bool2string(booltype):
    """
    Convert python booll to string
    Such string can be consumed by C# and parsed as bool agin

    Args:
        booltype (bool): 'True' or 'False'

    Returns:
        True: "true"
        False:"false" 
    """
    if booltype:
        return "true"
    else:
        return "false"


def check_input_type(InputControlType: str):
    # proper input control type not provided
    if InputControlType not in INPUT_CONTROL_TYPE:
        raise Exception(
            "Set control type {} not defualts {}".format(
                InputControlType, INPUT_CONTROL_TYPE
            )
        )


def set_camera(
    InputControlType="Code",
    ActiveCamera=1,
    IsCapture=False,
    CaptureCamera=1,
    CaptureType=1,
    CaptureWidth=256,
    CaptureHeight=256,
    IsOutput=False,
):
    """[summary]

    Args:
        InputControlType (str, optional): It can be either `Code` or `Other`. This is to control the internal mechanism and prevent repeted calling in already set variables. \
        If `InputControlType` is set to 'Code', camera cannot be controlled from Keyboard or Joystick. If `InputControlType` is set to 'Other', camera can be only controlled from Keyboard or Joystick.  Defaults to "Code". 
        ActiveCamera (int, optional): Aircontrol Airplane has two camera inside the Cockpit and outside the Airplane. The Camera inside the Cockpit is indexed as 0.The outside the Airplane is indexed as 1. `ActiveCamera` can be used to select the scene camera. Defaults to 1.
        IsCapture (bool, optional): `Iscapture` if true the screenshot will be captured. Defaults to False.
        CaptureCamera (int, optional): `CaptureCamera` defines which camera should be used for capturing the scene. Defaults to 1.
        CaptureType (int, optional): . Defaults to 1.
        CaptureWidth (int, optional): Width of the captured Image. Defaults to 256.
        CaptureHeight (int, optional): Height of the captured Image. Defaults to 256.
        IsOutput (bool, optional): By default `set_camera` function only sets the internal state. `set_camera` only provide log outout and not the actual captured image. `set_control` when called it returns actual output. IF you want to force `set_camera` to return image, set `IsOutput` to True. Defaults to False.

    Returns:
        [type]: [description]
    """

    check_input_type(InputControlType)

    camera_schema = {
        "MsgType": "Camera",
        "InputControlType": InputControlType,
        "ActiveCamera": ActiveCamera,
        "IsCapture": bool2string(IsCapture),
        "CaptureCamera": CaptureCamera,
        "CaptureType": CaptureType,
        "CaptureWidth": CaptureWidth,
        "CaptureHeight": CaptureHeight,
        "IsOutput": bool2string(IsOutput),
    }
    # communicator.send_data(data_dict=camera_schema, sock=socket)
    return eval(str(camera_schema))


def set_control(
    InputControlType="Code",
    Pitch=0.0,
    Roll=0.0,
    Yaw=0.0,
    Throttle=0.0,
    StickyThrottle=0.0,
    Brake=0,
    Flaps=0,
    IsOutput=True,
):
    """[summary]

    Args:
        InputControlType (str, optional): It can be either `Code` or `Other`. This is to control the internal mechanism and prevent repeted calling in already set variables. \
        If `InputControlType` is set to 'Code', camera cannot be controlled from Keyboard or Joystick. If `InputControlType` is set to 'Other', camera can be only controlled from Keyboard or Joystick.  Defaults to "Code". 
        Pitch (float, optional): The aircraft nose can rotate up and down about the y-axis, a motion known as pitch. Pitch control is typically accomplished using an elevator on the horizontal tail. Defaults to 0.0.
        Roll (float, optional): The wingtips can rotate up and down about the x-axis, a motion known as roll. Roll control is usually provided using ailerons located at each wingtip.. Defaults to 0.0.
        Yaw (float, optional): The nose can rotate left and right about the z-axis, a motion known as yaw.Yaw control is most often accomplished using a rudder located on the vertical tail.. Defaults to 0.0.
        Throttle (float, optional): Controls the engine power. Defaults to 0.0.
        StickyThrottle (float, optional): Tyoically Airplane have sticky throttles. Throttle values stay same unless moved. Defaults to 0.0.
        Brake (int, optional): Applies brake to wheels, to control ground movement. Defaults to 0.
        Flaps (int, optional): Flaps helps in controlling descent. Defaults to 0.
    """
    check_input_type(InputControlType)
    # limitting value
    Pitch = np.clip(Pitch, -1, 1)
    Roll = np.clip(Roll, -1, 1)
    Yaw = np.clip(Yaw, -1, 1)
    Throttle = np.clip(Throttle, 0, 1)
    StickyThrottle = np.clip(StickyThrottle, 0, 1)
    Brake = np.clip(Brake, 0, 1)
    Flaps = max(min(Flaps, 2), 0)

    control_schema = {
        "MsgType": "ControlInput",
        "InputControlType": InputControlType,
        "Pitch": Pitch,
        "Roll": Roll,
        "Yaw": Yaw,
        "Throttle": Throttle,
        "StickyThrottle": StickyThrottle,
        "Brake": Brake,
        "Flaps": Flaps,
        "IsOutput": bool2string(IsOutput),
    }
    # communicator.send_data(data_dict=control_schema, sock=socket)
    return eval(str(control_schema))


def set_fuel(InputControlType="Code", IsOutput=False):
    """[summary]

    Args:
        socket (socket): [description]
        InputControlType (str, optional): [description]. Defaults to "Code".
    """
    check_input_type(InputControlType)
    fuel_schema = {
        "MsgType": "Fuel",
        "InputControlType": InputControlType,
        "IsOutput": bool2string(IsOutput),
    }
    # communicator.send_data(data_dict=fuel_schema, sock=socket)
    return eval(str(fuel_schema))


def reset_level(
    InputControlType="Code", IsActive=False, LevelReload=False, IsOutput=False
):
    """[summary]

    Args:
        InputControlType (str, optional): It can be either `Code` or `Other`. This is to control the internal mechanism and prevent repeted calling in already set variables. \
        If `InputControlType` is set to 'Code', camera cannot be controlled from Keyboard or Joystick. If `InputControlType` is set to 'Other', camera can be only controlled from Keyboard or Joystick.  Defaults to "Code". 
        IsActive (bool, optional): Internal mechanism to save compute, set to `True` when level reset required
        LevelReload (bool, optional): set to `True` to reset the level. Defaults to False.
        IsOutput (bool, optional): By default `reset_level` function only sets the internal state. `reset_level` only provide log outout and not the actual captured image. `set_control` when called it returns actual output. IF you want to force `reset_level` to return image, set `IsOutput` to True. Defaults to False.
    """
    check_input_type(InputControlType)
    level_schema = {
        "MsgType": "Level",
        "InputControlType": InputControlType,
        "IsActive": bool2string(IsActive),
        "LevelReload": bool2string(LevelReload),
        "IsOutput": bool2string(IsOutput),
    }
    # communicator.send_data(data_dict=level_schema, sock=socket)
    return eval(str(level_schema))


def set_lidar(
    InputControlType="Code", Range=100000.0, Density=360, IsActive=False, IsOutput=False
):
    """[summary]

    Args:
        InputControlType (str, optional): It can be either `Code` or `Other`. This is to control the internal mechanism and prevent repeated calling in already set variables. \
        If `InputControlType` is set to 'Code', the camera cannot be controlled from Keyboard or Joystick. If `InputControlType` is set to 'Other', the camera can be only controlled from Keyboard or Joystick.  Defaults to "Code". 
        Range (float, optional): Range of the Lidar. Defaults to 100000.0.
        Density (int, optional): Number of Raycast spread across 360 degrees. Defaults to 360.
        IsActive (bool, optional): If lidar is set to active or not. Defaults to False.
        IsOutput (bool, optional): By default `set_lidar` function only sets the internal state. `set_lidar` only provides log output and not the actual captured image. `set_control` when called it returns the actual output. IF you want to force `set_lidar` to return the image, set `IsOutput` to True. Defaults to False.
    Returns:
        [type]: [description]
    """
    check_input_type(InputControlType)
    lidar_schema = {
        "MsgType": "Lidar",
        "InputControlType": InputControlType,
        "Range": Range,
        "Density": Density,
        "IsActive": IsActive,
        "IsOutput": bool2string(IsOutput),
    }
    # communicator.send_data(data_dict=lidar_schema, sock=socket)
    return eval(str(lidar_schema))


def set_TOD(
    IsActive=False,
    SunLatitude=-826.39,
    SunLongitude=-1605.4,
    Hour=10,
    Minute=5,
    IsOutput=False,
):
    """[summary]

    Args:
        IsActive (bool, optional): Active if set to `True`. Internal effective compute mechanism. Defaults to False.
        SunLatitude (float, optional): Controls sun Latitude. Defaults to -826.39.
        SunLongitude (float, optional): Controls sun Longitude. Defaults to -1605.4.
        Hour (int, optional): Set Hour. Defaults to 10.
        Minute (int, optional): Set Minutes. Defaults to 5.
    """

    tod_schema = {
        "MsgType": "TOD",
        "IsActive": bool2string(IsActive),
        "SunLatitude": SunLatitude,
        "SunLongitude": SunLongitude,
        "Hour": Hour,
        "Minute": Minute,
        "IsOutput": bool2string(IsOutput),
    }
    # communicator.send_data(data_dict=tod_schema, sock=socket)
    return eval(str(tod_schema))


def set_uiaudio(
    InputControlType="Code", ShowUIElements=True, EnableAudio=True, IsOutput=False
):
    """[summary]

    Args:
        InputControlType (str, optional): It can be either `Code` or `Other`. This is to control the internal mechanism and prevent repeted calling in already set variables. \
        If `InputControlType` is set to 'Code', camera cannot be controlled from Keyboard or Joystick. If `InputControlType` is set to 'Other', camera can be only controlled from Keyboard or Joystick.  Defaults to "Code". 
        ShowUIElements (bool, optional): Show UI elements if true, hide otherwise. Defaults to True.
        EnableAudio (float, optional): Enable audio if true, mute otherwise.. Defaults to 1.0.
    """
    check_input_type(InputControlType)
    uiaudio_schema = {
        "MsgType": "UIAudio",
        "InputControlType": InputControlType,
        "ShowUIElements": bool2string(ShowUIElements),
        "EnableAudio": bool2string(EnableAudio),
        "IsOutput": bool2string(IsOutput),
    }
    # communicator.send_data(data_dict=uiaudio_schema, sock=socket)
    return eval(str(uiaudio_schema))


def set_weather(InputControlType="Code", IsClouds=False, IsFog=False, IsOutput=False):
    """[summary]

    Args:
        socket (socket): [description]
        InputControlType (str, optional): [description]. Defaults to "Code".
        IsClouds (bool, optional): [description]. Defaults to False.
        IsFog (bool, optional): [description]. Defaults to False.
    """
    check_input_type(InputControlType)
    weather_schema = {
        "MsgType": "Weather",
        "InputControlType": InputControlType,
        "IsClouds": bool2string(IsClouds),
        "IsFog": bool2string(IsFog),
        "IsOutput": bool2string(IsOutput),
    }
    # communicator.send_data(data_dict=weather_schema, sock=socket)
    return eval(str(weather_schema))
