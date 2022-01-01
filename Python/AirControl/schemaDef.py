import json
import socket
import communicator

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

def set_camera(socket:socket,InputControlType:str, ActiveCamera:int, IsCapture:bool, CaptureCamera:int, CaptureType:int, CaptureWidth:int, CaptureHeight:int): 
    camera_schema = {
        "MsgType": "Camera",
        "InputControlType": InputControlType,
        "ActiveCamera": ActiveCamera,
        "IsCapture": bool2string(IsCapture),
        "CaptureCamera": CaptureCamera,
        "CaptureType": CaptureType,
        "CaptureWidth": CaptureWidth,
        "CaptureHeight": CaptureHeight
    }
    communicator.send_data(data_dict=camera_schema, sock=socket)

def set_control( socket:socket,InputControlType:str, Pitch:float, Roll:float, Yaw:float,Throttle:float, StickyThrottle:float, Brake:float, Flaps:int):
    control_schema = {
        "MsgType": "ControlInput",
        "InputControlType": InputControlType,
        "Pitch": Pitch,
        "Roll": Roll,
        "Yaw": Yaw,
        "Throttle": Throttle,
        "StickyThrottle": StickyThrottle,
        "Brake": Brake,
        "Flaps": Flaps
    }
    communicator.send_data(data_dict=control_schema, sock=socket)

def set_fuel(socket:socket,InputControlType:str):
    fuel_schema = {
        "MsgType": "Fuel",
        "InputControlType": InputControlType
    }
    communicator.send_data(data_dict=fuel_schema, sock=socket)

def reset_level(socket:socket,InputControlType:str, IsActive:bool, LevelReload:bool):
    level_schema = {
        "MsgType": "Level",
        "InputControlType": InputControlType,
        "IsActive": bool2string(IsActive),
        "LevelReload": bool2string(LevelReload)
    }
    communicator.send_data(data_dict=level_schema, sock=socket)

def set_lidar(socket:socket,InputControlType:str, Range:float, Density:int):
    lidar_schema = {
        "MsgType": "Lidar",
        "InputControlType": InputControlType,
        "Range": Range,
        "Density": Density
    }
    communicator.send_data(data_dict=lidar_schema, sock=socket)

def set_TOD(socket:socket,IsActive:bool, SunLatitude:float,SunLongitude:float, Hour:int, Minute:int):
    tod_schema = {
        "MsgType": "TOD",
        "IsActive": bool2string(IsActive),
        "SunLatitude": SunLatitude,
        "SunLongitude": SunLongitude,
        "Hour": 10,
        "Minute": 5
    }
    communicator.send_data(data_dict=tod_schema, sock=socket)

def set_uiaudio(socket:socket,InputControlType:str, ShowUIElements:bool,AudioVolume:float):
    uiaudio_schema = {
        "MsgType": "UIAudio",
        "InputControlType": InputControlType,
        "ShowUIElements": bool2string(ShowUIElements),
        "AudioVolume": AudioVolume
        }
    communicator.send_data(data_dict=uiaudio_schema, sock=socket)

def set_weather(socket:socket,InputControlType:str, IsClouds:bool, IsFog:bool):
    weather_schema = {
        "MsgType": "Weather",
        "InputControlType": InputControlType,
        "IsClouds": IsClouds,
        "IsFog": IsFog
    }
    communicator.send_data(data_dict=weather_schema, sock=socket)
