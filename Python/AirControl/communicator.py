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
        try:
            self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            self.sock.connect((host, port))   
            self.sock.settimeout(10)      
        except Exception as e:
            print(e)

    def send_data(self, data_dict: dict):
        """
        Send all data to the server
        
        Args:
            data_dict (dict): Dict with data
            sock (socket): Socket connection aquired from `get_socket` method
        """
        data = json.dumps(data_dict, cls=NpEncoder)

        self.sock.sendall(data.encode("utf-8"))

    def receive_data(self):
        """
        Receives data from server
        
        Args:
            sock (socket): Socket connection aquired from `get_socket` method
        Returns:
            data(dict): Data Received from the server
        """
        # sleep stabilizes the TCP connection and bring in oder
        # if not used then the operations will be hightly unstable and event will be missed
        time.sleep(0.05)
        BUFF_SIZE = 1024  # 1 MB
        data = b""
        while True:
            part = self.sock.recv(BUFF_SIZE)
            data += part
            if len(part) < BUFF_SIZE:
                # either 0 or end of data
                break
        data = eval(data)
        return data
