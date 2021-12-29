import socket
import json

def get_socket(host="127.0.0.1", port=8053):
    """
    Get scoket connection to the host

    Args:
        host (str, optional): IP address of the server. Defaults to "127.0.0.1".
        port (int, optional): Port of the server. Defaults to 8053.

    Returns:
        Socket: socket connection to the server
    """
    try:
        sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        sock.connect((host,port))
        return sock
    except Exception as e:
        print(e)
        
def send_data(data_dict:dict, sock:socket):
    """
    Send all data to the server
    
    Args:
        data_dict (dict): Dict with data
        sock (socket): Socket connection aquired from `get_socket` method
    """
    data = json.dumps(data_dict)
    sock.sendall(data.encode("utf-8"))  

def receive_data(sock):
    """
    Receives data from server
    
    Args:
        sock (socket): Socket connection aquired from `get_socket` method
    Returns:
        data(dict): Data Received from the server
    """
    BUFF_SIZE = 4096 # 4 KiB
    data = b''
    while True:
        part = sock.recv(BUFF_SIZE)
        data += part
        if len(part) < BUFF_SIZE:
            # either 0 or end of data
            break
    data = eval(data)
    return data

        
