import json
import socket
import numpy as np

INPUT_CONTROL_TYPE=['Code', "Other"]

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
    
def check_input_type(InputControlType:str):
    # proper input control type not provided 
    if (InputControlType not in INPUT_CONTROL_TYPE):
        raise Exception("Set control type {} not defualts {}".format(InputControlType, INPUT_CONTROL_TYPE))

def set_camera(InputControlType="Code", ActiveCamera=1, IsCapture=False, CaptureCamera=1, CaptureType=1, CaptureWidth=256, CaptureHeight=256,IsOutput=False): 
    
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
        "IsOutput":bool2string(IsOutput),
    }
    # communicator.send_data(data_dict=camera_schema, sock=socket)
    return eval(str(camera_schema))

def set_control( InputControlType="Code", Pitch=0.0, Roll=0.0, Yaw=0.0,Throttle=0.0, StickyThrottle=0.0, Brake=0, Flaps=0,IsOutput=True):
    """[summary]

    Args:
        socket (socket): [description]
        InputControlType (str, optional): [description]. Defaults to "Code".
        Pitch (float, optional): [description]. Defaults to 0.0.
        Roll (float, optional): [description]. Defaults to 0.0.
        Yaw (float, optional): [description]. Defaults to 0.0.
        Throttle (float, optional): [description]. Defaults to 0.0.
        StickyThrottle (float, optional): [description]. Defaults to 0.0.
        Brake (int, optional): [description]. Defaults to 0.
        Flaps (int, optional): [description]. Defaults to 0.
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
        "IsOutput":bool2string(IsOutput),
    }
    # communicator.send_data(data_dict=control_schema, sock=socket)
    return eval(str(control_schema))

def set_fuel(InputControlType="Code",IsOutput=False):
    """[summary]

    Args:
        socket (socket): [description]
        InputControlType (str, optional): [description]. Defaults to "Code".
    """
    check_input_type(InputControlType)
    fuel_schema = {
        "MsgType": "Fuel",
        "InputControlType": InputControlType,
        "IsOutput":bool2string(IsOutput),
    }
    # communicator.send_data(data_dict=fuel_schema, sock=socket)
    return eval(str(fuel_schema))

def reset_level(InputControlType="Code", IsActive=False, LevelReload=False,IsOutput=False):
    """[summary]

    Args:
        socket (socket): [description]
        InputControlType (str, optional): [description]. Defaults to "Code".
        IsActive (bool, optional): [description]. Defaults to False.
        LevelReload (bool, optional): [description]. Defaults to False.
    """
    check_input_type(InputControlType)
    level_schema = {
        "MsgType": "Level",
        "InputControlType": InputControlType,
        "IsActive": bool2string(IsActive),
        "LevelReload": bool2string(LevelReload),
        "IsOutput":bool2string(IsOutput),
    }
    # communicator.send_data(data_dict=level_schema, sock=socket)
    return eval(str(level_schema))

def set_lidar(InputControlType="Code", Range=100000.0, Density=360, IsActive=False,IsOutput=False):
    """[summary]

    Args:
        socket (socket): [description]
        InputControlType (str, optional): [description]. Defaults to "Code".
        Range (float, optional): [description]. Defaults to 100000.0.
        Density (int, optional): [description]. Defaults to 360.
    """
    check_input_type(InputControlType)
    lidar_schema = {
        "MsgType": "Lidar",
        "InputControlType": InputControlType,
        "Range": Range,
        "Density": Density,
        "IsActive": IsActive,
        "IsOutput":bool2string(IsOutput),
    }
    # communicator.send_data(data_dict=lidar_schema, sock=socket)
    return eval(str(lidar_schema))

def set_TOD(IsActive=False, SunLatitude=-826.39,SunLongitude=-1605.4, Hour=10, Minute=5,IsOutput=False):
    """[summary]

    Args:
        socket (socket): [description]
        IsActive (bool, optional): [description]. Defaults to False.
        SunLatitude (float, optional): [description]. Defaults to -826.39.
        SunLongitude (float, optional): [description]. Defaults to -1605.4.
        Hour (int, optional): [description]. Defaults to 10.
        Minute (int, optional): [description]. Defaults to 5.
    """
    
    tod_schema = {
        "MsgType": "TOD",
        "IsActive": bool2string(IsActive),
        "SunLatitude": SunLatitude,
        "SunLongitude": SunLongitude,
        "Hour": Hour,
        "Minute": Minute,
        "IsOutput":bool2string(IsOutput),
    }
    # communicator.send_data(data_dict=tod_schema, sock=socket)
    return eval(str(tod_schema))

def set_uiaudio(InputControlType="Code", ShowUIElements=True,EnableAudio=True,IsOutput=False):
    """[summary]

    Args:
        socket (socket): [description]
        InputControlType (str, optional): [description]. Defaults to "Code".
        ShowUIElements (bool, optional): [description]. Defaults to True.
        AudioVolume (float, optional): [description]. Defaults to 1.0.
    """
    check_input_type(InputControlType)
    uiaudio_schema = {
        "MsgType": "UIAudio",
        "InputControlType": InputControlType,
        "ShowUIElements": bool2string(ShowUIElements),
        "EnableAudio": bool2string(EnableAudio),
        "IsOutput":bool2string(IsOutput),
        }
    # communicator.send_data(data_dict=uiaudio_schema, sock=socket)
    return eval(str(uiaudio_schema))

def set_weather(InputControlType="Code", IsClouds=False, IsFog=False,IsOutput=False):
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
        "IsOutput":bool2string(IsOutput),
    }
    # communicator.send_data(data_dict=weather_schema, sock=socket)
    return eval(str(weather_schema))
