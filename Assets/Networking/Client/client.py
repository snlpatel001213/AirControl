import socket
import time
host, port =  "127.0.0.1" , 25001
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect((host,port))
for i in range(0,10):
    data = "0.22,{},{}".format(i*2,i*5)
    try:
        
        sock.sendall(data.encode("utf-8"))
        data = sock.recv(1024).decode("utf-8")
        print(data)

    except Exception as e:
        print(">>>>>>>>>>>>",e)
    time.sleep(5)
sock.close()