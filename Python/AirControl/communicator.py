import json
import socket
import time

import numpy as np


class NpEncoder(json.JSONEncoder):
    def default(self, obj):
        if isinstance(obj, np.integer):
            return int(obj)
        if isinstance(obj, np.floating):
            return float(obj)
        if isinstance(obj, np.ndarray):
            return obj.tolist()
        return super(NpEncoder, self).default(obj)

class Communicator:
    def __init__(self, host="127.0.0.1", port=8053):
        """
        Get scoket connection to the host

        Args:
            host (str, optional): IP address of the server. Defaults to "127.0.0.1".
            port (int, optional): Port of the server. Defaults to 8053.

        Returns:
            Socket: socket connection to the server
        """
        self.SEND_BUF_SIZE = 4096 
        self.RECV_BUF_SIZE = 4096 
        try:
            self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

            # Get the size of the socket's send buffer 
            # bufsize = self.sock.getsockopt(socket.SOL_SOCKET, socket.SO_SNDBUF) 
            # print ("Buffer size [Before]:%d" %bufsize) 
            
            # self.sock.setsockopt(socket.SOL_TCP, 
            #                 socket.TCP_NODELAY, 1) 
            
            # self.sock.setsockopt( 
            #         socket.SOL_SOCKET, 
            #         socket.SO_SNDBUF, 
            #         self.SEND_BUF_SIZE) 
            # self.sock.setsockopt( 
            #         socket.SOL_SOCKET, 
            #         socket.SO_RCVBUF, 
            #         self.RECV_BUF_SIZE) 
            # bufsize = self.sock.getsockopt(socket.SOL_SOCKET, socket.SO_SNDBUF) 
            # print ("Buffer size [After]:%d" %bufsize) 

            # self.setblocking(1) 
            self.sock.connect((host, port))   
            self.sock.settimeout(10)      
        except Exception as e:
            print("Faced Error while establishing serverclient connect.",e)

    def send_data(self, data_dict: dict):
        """
        Send all data to the server
        
        Args:
            data_dict (dict): Dict with data
            sock (socket): Socket connection aquired from `get_socket` method
        """
        data = json.dumps(data_dict, cls=NpEncoder)
        self.sock.sendall(data.encode("utf-8"))

    def receive_data(self, timeout=0.1):
        """
        To receive data partwise
        """
        self.sock.setblocking(0)
        #total data partwise in an array
        total_data=b"";        
        #beginning time
        begin=time.time()
        while 1:
            #if you got some data, then break after timeout
            if total_data and time.time()-begin > timeout:
                break
            
            #if you got no data at all, wait a little longer, twice the timeout
            elif time.time()-begin > timeout*2:
                break
            
            #recv something
            try:
                data = self.sock.recv(1024)
                if data:
                    total_data += data
                    #change the beginning time for measurement
                    begin=time.time()
                else:
                    #sleep for sometime to indicate a gap
                    time.sleep(0.01)
            except:
                pass
        
        #join all parts to make final string
        return eval(total_data)