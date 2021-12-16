import socket
import time
import json
host, port =  "127.0.0.1" , 8090
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect((host,port))
for i in range(0,1):
    dict_ = { "type" : "Camera",
        "InputDataStruct":{"pitch":1.245,"roll":0.0,"yaw":5555,"throttle":0.0,"brake":0.0,"flaps":0},
        "Camera":{"active":1},
        "Init":{},
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
    time.sleep(5)
sock.close()

