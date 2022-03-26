import json
import socket
import time

import numpy as np


class NpEncoder(json.JSONEncoder):
    def default(self, obj):
        """
        If the object is a numpy integer, return an integer. If the object is a numpy float, return a
        float. If the object is a numpy array, return the array. If the object is none of the above,
        return the super of the function
        
        :param obj: The object to serialize
        :return: A JSON object with the data from the DataFrame
        """
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
            self.sock.connect((host, port))   
            self.sock.settimeout(10)      
        except Exception as e:
            print("Faced Error while establishing server-client connect.",e)

    def send_data(self, data_dict: dict):
        """
        Send all data to the server
        
        Args:
            data_dict (dict): Dict with data
            sock (socket): Socket connection aquired from `get_socket` method
        """
        data = json.dumps(data_dict, cls=NpEncoder)
        # print("sent : ", data)
        self.sock.sendall(data.encode("utf-8"))

    def receive_data(self, timeout=0.5):
        """
        Receive data partwise
    
        :param timeout: The timeout parameter specifies the time-out as a floating point number in
        seconds, this help is getting time out at first call to server and subsequent calls.
        :return: A list of dictionaries. Each dictionary is a question.
        
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
                    time.sleep(0.1)
            except Exception as e: 
                # print("Exception : ", e)
                pass
        
        #join all parts to make final string
        # print("recived : ", total_data)
        return eval(total_data)