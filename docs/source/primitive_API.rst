Aircontrol Primitive Communication
==================================

This is just for to debug things. Not to be used as actual API

Importing Requirements
----------------------

.. code:: ipython3

    from pprint import pprint
    import PIL.Image as Image
    import base64
    import numpy as np
    from io import BytesIO
    import socket
    import json

.. code:: ipython3

    sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    sock.connect(("127.0.0.1", 8053))

.. code:: ipython3

    
    def send_data( data_dict: dict):
            """
            Send all data to the server
            
            Args:
                data_dict (dict): Dict with data
                sock (socket): Socket connection aquired from `get_socket` method
            """
            data = json.dumps(data_dict)
            sock.sendall(data.encode("utf-8"))
    
        

.. code:: ipython3

    def receive_data():
            """
            Receives data from server
            
            Args:
                sock (socket): Socket connection aquired from `get_socket` method
            Returns:
                data(dict): Data Received from the server
            """
            BUFF_SIZE = 4096  # 4 KiB
            data = b""
            while True:
                part = sock.recv(BUFF_SIZE)
                data += part
                if len(part) < BUFF_SIZE:
                    # either 0 or end of data
                    break
            data = eval(data)
            return data

.. code:: ipython3

    control_schema = {
                "MsgType": "ControlInput",
                "InputControlType": "Code",
                "Pitch": 0.0,
                "Roll": 0.0,
                "Yaw": 0.0,
                "Throttle": 0.0,
                "StickyThrottle": 0.5,
                "Brake": 0,
                "Flaps": 0,
                "IsOutput": True,
            }
    print(control_schema)


.. parsed-literal::

    {'MsgType': 'ControlInput', 'InputControlType': 'Code', 'Pitch': 0.0, 'Roll': 0.0, 'Yaw': 0.0, 'Throttle': 0.0, 'StickyThrottle': 0.5, 'Brake': 0, 'Flaps': 0, 'IsOutput': True}


.. code:: ipython3

    send_data(control_schema) 
    output =  receive_data()


.. code:: ipython3

    output




.. parsed-literal::

    {'AGL': 0.0,
     'MSL': 39.7559471,
     'CurrentRPM': 0.0,
     'MaxRPM': 3000.0,
     'MaxPower': 3500.0,
     'CurrentPower': 0.0,
     'CurrentFuel': 0.0,
     'CurrentSpeed': 0.0,
     'BankAngle': 0.0151821785,
     'IfCollision': 'false',
     'Latitude': -0.000135355062,
     'Longitude': -6.03607248e-07,
     'PitchAngle': 0.332173437,
     'ScreenCapture': '',
     'LidarPointCloud': [100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      3933.74536,
      100000.0,
      3602.73315,
      3671.42578,
      100000.0,
      4638.247,
      100000.0,
      4270.67236,
      4269.20752,
      4343.896,
      100000.0,
      3438.182,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      2216.89575,
      2181.80615,
      2203.258,
      2249.40332,
      2298.23682,
      2349.96973,
      2404.83472,
      2463.09155,
      2525.02856,
      100000.0,
      100000.0,
      100000.0,
      3087.768,
      3042.05,
      100000.0,
      4013.38135,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      3271.56934,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      1799.65356,
      1069.42676,
      737.932068,
      564.5641,
      461.597382,
      382.359924,
      330.848969,
      295.169983,
      263.271942,
      238.179977,
      217.8625,
      200.795319,
      186.288574,
      174.025009,
      163.3231,
      153.905258,
      145.49498,
      137.82193,
      17.3426857,
      16.5231571,
      15.7821779,
      15.1092062,
      14.4955168,
      13.9338217,
      13.4179564,
      12.9427338,
      12.5036936,
      12.0970316,
      11.7194414,
      11.3680725,
      11.0404243,
      10.73431,
      10.4478092,
      10.179225,
      9.927052,
      9.689954,
      9.466729,
      9.256316,
      9.057752,
      8.870174,
      8.69280052,
      8.524927,
      8.365917,
      8.215184,
      8.072203,
      7.936491,
      7.80760431,
      7.68514252,
      7.56873,
      7.45803165,
      7.35273027,
      7.25254154,
      7.15719652,
      7.0664506,
      6.980077,
      6.89786339,
      6.8196187,
      6.74515963,
      6.674321,
      6.606947,
      6.5428915,
      6.482023,
      6.42421436,
      6.36935043,
      6.317324,
      6.268036,
      6.221389,
      6.1772995,
      6.13568735,
      6.09647751,
      6.059597,
      6.024987,
      5.992583,
      5.96233463,
      5.93418741,
      5.908095,
      5.88401556,
      5.86191,
      5.841744,
      2.16294837,
      2.10318637,
      2.071829,
      2.05951834,
      1.93164718,
      1.93017566,
      1.92929387,
      1.92900014,
      1.92929387,
      1.93017566,
      5.74084663,
      5.74229956,
      5.74550247,
      5.750463,
      5.75718737,
      5.7656846,
      5.77597046,
      5.78806,
      5.80196953,
      5.8177247,
      5.83534527,
      5.85486126,
      5.87630749,
      5.899714,
      5.92511845,
      5.95256424,
      5.98209858,
      6.01376534,
      6.04762173,
      6.0837245,
      6.12213945,
      6.162929,
      6.20617,
      6.251941,
      6.30032444,
      6.35141134,
      6.40530443,
      6.462101,
      6.52191973,
      6.58488369,
      6.651116,
      6.72076464,
      6.793978,
      6.870922,
      6.951769,
      7.03671074,
      7.12595224,
      7.21971226,
      7.31823254,
      7.421768,
      7.53060436,
      7.645043,
      7.765414,
      7.892082,
      8.02543449,
      8.165902,
      8.313952,
      8.470097,
      8.634898,
      8.808976,
      8.993013,
      9.187764,
      9.394062,
      9.612833,
      9.8450985,
      10.0920238,
      10.354887,
      10.6351452,
      10.93441,
      11.2545309,
      11.5976,
      11.966011,
      12.3624773,
      12.7901487,
      13.2526455,
      13.7541933,
      14.29974,
      14.8950615,
      15.5470457,
      16.2638988,
      132.500839,
      17.9338779,
      18.9136677,
      20.0131683,
      21.25526,
      22.6691036,
      24.2923965,
      26.17465,
      28.3824577,
      30.97756,
      34.14586,
      38.0491333,
      43.01585,
      49.4295349,
      58.11168,
      70.5198059,
      636.846069,
      908.975037,
      1372.70361,
      3015.34839,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0,
      100000.0],
     'MsgType': 'Output',
     'Version': '0.0.5'}



