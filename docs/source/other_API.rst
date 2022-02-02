Aircontrol Other API
====================

Importing requirements
----------------------

.. code:: ipython3

    from AirControl import actions
    
    from AirControl import schemaDef
    from pprint import pprint
    import PIL.Image as Image
    import base64
    import numpy as np
    from io import BytesIO
    from matplotlib.pyplot import  imshow
    import matplotlib.pyplot as plt
    
    A =  actions.Actions()


.. parsed-literal::

    Now play the environment and call call method `Action.get_connected` to get connected


.. code:: ipython3

    # get connected to server
    A.get_connected()

UI and Audio
------------

**UI API** - helps in enabling and disabling the UI components like
Airplane controls on screen

**Audio API** - Helps in enabling and disabling Audio.

Python API
----------

-  InputControlType (str, optional): It can be either ``Code`` or
   ``Other``. This is to control the internal mechanism and prevent
   repeted calling in already set variables.
   If ``InputControlType`` is set to ‘Code’, camera cannot be controlled
   from Keyboard or Joystick. If ``InputControlType`` is set to ‘Other’,
   camera can be only controlled from Keyboard or Joystick. Defaults to
   “Code”.
-  ShowUIElements (bool, optional): Show UI elements if ``True``, hide
   otherwise. Defaults to True.
-  EnableAudio (float, optional): Enable audio if ``True``, mute
   otherwise.. Defaults to 1.0.

.. code:: ipython3

    audio_control = A.set_audio(EnableAudio=False, IsActive=True)
    print(audio_control)


.. parsed-literal::

    {'Log': ''}


.. code:: ipython3

    ui_control = A.set_ui(ShowUIElements=True, IsActive= True)
    print(ui_control)

Level Reset
-----------

Can be used to reset the level at each iteration. Typically
Reinforcement learning loops requires hundreds and thousands environment
reset

Python API
----------

-  InputControlType (str, optional): It can be either ``Code`` or
   ``Other``. This is to control the internal mechanism and prevent
   repeted calling in already set variables.
   If ``InputControlType`` is set to ‘Code’, camera cannot be controlled
   from Keyboard or Joystick. If ``InputControlType`` is set to ‘Other’,
   camera can be only controlled from Keyboard or Joystick. Defaults to
   “Code”.
-  IsOutput (bool, optional): By default ``reset_level`` function only
   sets the internal state. ``reset_level`` only provide log outout and
   not the actual captured image. ``set_control`` when called it returns
   actual output. IF you want to force ``reset_level`` to return image,
   set ``IsOutput`` to True. Defaults to False.

.. code:: ipython3

    output = A.reset(IsOutput=True)
    print(output)


.. parsed-literal::

    {'AGL': 0.0, 'MSL': 38.6391869, 'CurrentRPM': 0.0, 'MaxRPM': 3000.0, 'MaxPower': 3500.0, 'CurrentPower': 0.0, 'CurrentFuel': 0.0, 'CurrentSpeed': 0.00141569716, 'BankAngle': 0.04395718, 'IfCollision': False, 'Latitude': -0.0002827725, 'Longitude': 6.64276831e-06, 'PitchAngle': 0.489656836, 'ScreenCapture': '', 'LidarPointCloud': [100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 27.4915733, 26.7108173, 25.9808674, 25.2972565, 24.6560211, 24.0536346, 23.4869576, 22.9532032, 22.449852, 21.97466, 21.5255852, 21.1007957, 20.6986313, 20.3175831, 19.95628, 19.6134682, 19.28802, 18.9788761, 18.68509, 18.4057827, 18.1401424, 17.8874359, 17.6469727, 17.4181252, 17.2003117, 16.9929848, 16.7956581, 16.6078625, 16.429163, 16.2591724, 16.0975151, 15.9438534, 15.79786, 15.6592436, 15.5277281, 15.4030552, 15.2849884, 15.1733036, 15.0678, 14.968276, 14.8745584, 14.7864857, 14.7039051, 14.6266689, 14.55465, 14.4877291, 14.4257956, 14.3687449, 14.31649, 14.2689457, 14.2260323, 14.1876879, 14.1538477, 14.2291193, 100000.0, 1739.399, 798.429749, 511.143036, 364.384, 294.8295, 247.451462, 211.54483, 184.810669, 164.097992, 147.591949, 134.172623, 122.899017, 113.404884, 105.302261, 98.30814, 92.21161, 86.85198, 82.104866, 77.87221, 74.07604, 70.65333, 67.5526, 64.73144, 62.1546822, 59.792717, 57.6206169, 55.61715, 53.7641335, 52.04595, 50.44908, 48.96781, 47.5896339, 46.3006363, 45.093, 43.9597969, 42.8949051, 41.8928452, 40.9487228, 40.0581551, 39.2171822, 38.4222755, 37.6728668, 36.9638, 36.29177, 35.6544075, 35.0495377, 34.475174, 33.92951, 33.41085, 32.9176979, 32.44863, 32.00235, 31.5776749, 31.1734829, 30.7887745, 30.4226036, 30.0740929, 29.7424316, 29.42686, 29.12671, 28.8413086, 28.57007, 28.3124275, 28.06786, 27.8358974, 27.61607, 27.4079819, 27.2112236, 27.025444, 26.85031, 26.6855087, 26.5307522, 26.3857632, 26.2503071, 26.1241512, 26.00709, 25.8989239, 25.7994766, 25.7086, 25.62614, 25.55195, 25.4859467, 2.162949, 2.10318661, 2.07183, 2.05951762, 1.931648, 1.9301765, 1.92929471, 1.92900085, 1.92929471, 1.93017673, 25.2785034, 25.30605, 25.3413773, 25.3845482, 25.435627, 25.4946976, 25.5618362, 25.6371727, 25.7208061, 25.81287, 25.9135246, 26.0229244, 26.14125, 26.26869, 26.4054661, 26.5518055, 26.7079544, 26.874176, 27.0507832, 27.2380772, 27.4363956, 27.6461048, 27.8676033, 28.1013069, 28.34767, 28.60718, 28.880373, 29.1678, 29.4700642, 29.7878437, 30.1218071, 30.4727478, 30.8414574, 31.2288361, 31.6358261, 32.0634537, 32.5128479, 32.9852028, 33.481842, 34.00417, 34.5537529, 35.132267, 35.74156, 36.38364, 37.0607033, 37.7751541, 38.529686, 39.3271866, 40.17089, 41.06437, 42.0115852, 43.01695, 44.08538, 45.2223473, 46.4340324, 47.72737, 49.11021, 50.591465, 52.18124, 53.8910866, 55.73434, 57.72641, 59.87568, 62.1836, 64.69705, 67.44368, 70.45633, 73.77417, 77.44466, 81.5256348, 86.1379852, 91.34503, 97.2534943, 104.013054, 111.819092, 120.931618, 131.705109, 144.364868, 159.592926, 178.4731, 201.696732, 231.309311, 275.0283, 339.538239, 441.27533, 604.813049, 1101.75525, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 15.9831982, 15.0613651, 15.1135492, 15.1707354, 15.2330122, 15.3004837, 15.3732557, 15.4514542, 15.5352125, 15.6246662, 15.7199707, 15.8212948, 15.9288177, 16.0427341, 16.1632462, 16.2905846, 16.4249878, 16.566721, 16.716053, 16.8732853, 17.0387478, 17.21278, 17.39576, 17.5880833, 17.7901878, 18.0025387, 18.22564, 18.4600372, 18.70631, 18.9650974, 19.2370872, 19.5230255, 19.82373, 20.1400661, 20.4729977, 20.82357, 21.1929264, 21.5823269, 21.99312, 22.426815, 22.8850727, 23.3697128, 23.88277, 24.4264565, 25.0032635, 25.6159649, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0], 'Counter': 1, 'log': '', 'Reward': 0.0, 'MsgType': 'Output', 'Version': '0.0.5'}


Work In Progress
================

Fuel API
--------

Can be used to control initial fuel amount and fuel consumption rate.

Weather API
-----------

Can be used to control environmantal fog and clouds. Planned for HDRP
release only.

Collision Detection
-------------------

Will provide information about collision and object with which it
collided
