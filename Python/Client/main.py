import socket
import time
import json
if __name__=="__main__":
    host, port =  "127.0.0.1" , 8053
    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    sock.connect((host,port))
    for i in range(0,1):
        dict_ = {
        "MsgType":"Incoming",
        "InputControlType": "Code",
        "sunPresent": False,
        "sunLocation_x": 0.0,
        "sunLocation_y": 0.0,
        "sunLocation_z": 0.0,
        "sunRotation_x": 0.0,
        "sunRotation_y": 0.0,
        "sunRotation_z": 0.0,
        "AirplaneDrag": 0.01,
        "AirplaneAngularDrag": 0.1,
        "AirplanemaxMPH": 150.0,
        "maxLiftPower": 200.0,
        "Pitch":0,
        "Roll": 0.0,
        "Yaw": 0.0,
        "Throttle": 0.0,
        "StickyThrottle": 1.0,
        "Brake": 0,
        "Flaps":0
        }

        dict__ = {
            "MsgType": "Transcation",
            "Version": "0.0.3",
            "InputControlType": "Code",
            "LevelReload": "false",
            "ActiveCamera": 1,
            "GetOutput": "true"

        }

        # time.sleep(5)

        # dict_ = { "type" : "Camera",
        #     "InputDataStruct":{"pitch":1.245,"roll":0.0,"yaw":5555,"throttle":0.0,"brake":0.0,"flaps":0},
        #     "Camera":{"active":2},
        #     "Init":{},
        # }

        data = json.dumps(dict_)
        try:
            
            sock.sendall(data.encode("utf-8"))
            data = sock.recv(1024).decode("utf-8")
            print(data)

        except Exception as e:
            print(">>>>>>>>>>>>",e)

    sock.close()

