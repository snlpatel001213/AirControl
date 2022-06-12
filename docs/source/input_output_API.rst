Aircontrol Input API
====================

Aircontrol provides fine control over inputs via python API. Following
properties can be controlled using ``set_control`` function.

Python API
----------

-  InputControlType (str, optional): It can be either ``Code`` or
   ``Other``. This is to control the internal mechanism and prevent
   repeted calling in already set variables.
   If ``InputControlType`` is set to ‘Code’, camera cannot be controlled
   from Keyboard or Joystick. If ``InputControlType`` is set to ‘Other’,
   camera can be only controlled from Keyboard or Joystick. Defaults to
   “Code”.
-  Pitch (float, optional): The aircraft nose can rotate up and down
   about the y-axis, a motion known as pitch. Pitch control is typically
   accomplished using an elevator on the horizontal tail. Defaults to
   0.0.
-  Roll (float, optional): The wingtips can rotate up and down about the
   x-axis, a motion known as roll. Roll control is usually provided
   using ailerons located at each wingtip.. Defaults to 0.0.
-  Yaw (float, optional): The nose can rotate left and right about the
   z-axis, a motion known as yaw.Yaw control is most often accomplished
   using a rudder located on the vertical tail.. Defaults to 0.0.
-  Throttle (float, optional): Controls the engine power (Deprecated),
   Use sticky Throttle. Defaults to 0.0.
-  StickyThrottle (float, optional): Tyoically Airplane have sticky
   throttles. Throttle values stay same unless moved. Defaults to 0.0.
-  Brake (int, optional): Applies brake to wheels, to control ground
   movement. Defaults to 0.
-  Flaps (int, optional): A flap is a high-lift device used to reduce
   the stalling speed of an aircraft wing at a given weight. Flaps helps
   in controlling descent. Defaults to 0.

Limits
------

=============== ===== ======================================
Control         type  Limit
=============== ===== ======================================
Pitch           float Between -1.0 and 1.0
Yaw             float Between -1.0 and 1.0
Roll            float Between -1.0 and 1.0
Sticky Throttle float Between 0.0 and 1.0
Brake           int   0 : Dis-engaged, 1 : Engaged
Flaps           int   0 : None, 1 : 15 Degree, 2 : 30 Degree
=============== ===== ======================================

Importing Requirements
----------------------

.. code:: ipython3

    from airctrl import environment
    from pprint import pprint
    import PIL.Image as Image
    import base64
    import numpy as np
    from io import BytesIO
    from matplotlib.pyplot import  imshow
    import matplotlib.pyplot as plt

.. code:: ipython3

    from airctrl import environment 
    from airctrl import sample_generator
    from airctrl.utils import unity
    from airctrl import sample_generator
    port=8999
    sample = sample_generator.samples()
    env =  environment.Trigger()
    L = unity.Launch()


.. parsed-literal::

    [32mNow call method `.get_connected(port=<Default 8053>)` to get connected
    [0m


.. code:: ipython3

    process = L.launch_executable("/home/supatel/Games/AirControl_2021/Build/1.3.0/Linux/v1.3.0-AirControl.x86_64", server_port=port)
    env.get_connected(port=port)


.. parsed-literal::

    [32mLoading environment from /home/supatel/Games/AirControl_2021/Build/1.3.0/Linux/v1.3.0-AirControl.x86_64 at port 8999 client ip 127.0.1.1 client port 8999
    [0m
    ['/home/supatel/Games/AirControl_2021/Build/1.3.0/Linux/v1.3.0-AirControl.x86_64', '--serverPort', '8999', '--clientIP', '127.0.1.1', '--clientPort', '8999']
    [32mSleeping for 5 seconds to allow environment load
    [0m
    [UnityMemory] Configuration Parameters - Can be set up in boot.config
        "memorysetup-bucket-allocator-granularity=16"
        "memorysetup-bucket-allocator-bucket-count=8"
        "memorysetup-bucket-allocator-block-size=4194304"
        "memorysetup-bucket-allocator-block-count=1"
        "memorysetup-main-allocator-block-size=16777216"
        "memorysetup-thread-allocator-block-size=16777216"
        "memorysetup-gfx-main-allocator-block-size=16777216"
        "memorysetup-gfx-thread-allocator-block-size=16777216"
        "memorysetup-cache-allocator-block-size=4194304"
        "memorysetup-typetree-allocator-block-size=2097152"
        "memorysetup-profiler-bucket-allocator-granularity=16"
        "memorysetup-profiler-bucket-allocator-bucket-count=8"
        "memorysetup-profiler-bucket-allocator-block-size=4194304"
        "memorysetup-profiler-bucket-allocator-block-count=1"
        "memorysetup-profiler-allocator-block-size=16777216"
        "memorysetup-profiler-editor-allocator-block-size=1048576"
        "memorysetup-temp-allocator-size-main=4194304"
        "memorysetup-job-temp-allocator-block-size=2097152"
        "memorysetup-job-temp-allocator-block-size-background=1048576"
        "memorysetup-job-temp-allocator-reduction-small-platforms=262144"
        "memorysetup-temp-allocator-size-background-worker=32768"
        "memorysetup-temp-allocator-size-job-worker=262144"
        "memorysetup-temp-allocator-size-preload-manager=262144"
        "memorysetup-temp-allocator-size-nav-mesh-worker=65536"
        "memorysetup-temp-allocator-size-audio-worker=65536"
        "memorysetup-temp-allocator-size-cloud-worker=32768"
        "memorysetup-temp-allocator-size-gfx=262144"
    [32mConnecting with port 8999
    [0m


.. code:: ipython3

    control_schema = env.step(Pitch=0.0, Roll=0.0, Yaw=0.0,Throttle=0.0, StickyThrottle=0.5, Brake=0, Flaps=0,IsOutput=True)
    print(control_schema)


.. parsed-literal::

    {'AGL': 0.0, 'MSL': 4.7329154, 'CurrentRPM': 0.0, 'MaxRPM': 4500.0, 'MaxPower': 1.10416677e-05, 'CurrentPower': 0.0, 'CurrentFuel': 0.0, 'CurrentSpeed': 0.0, 'BankAngle': 0.0017182061, 'IfCollision': False, 'CollisionObject': '', 'Latitude': -1.70281146e-06, 'Longitude': 5.317519e-06, 'PitchAngle': 0.328555942, 'ScreenCapture': '', 'LidarPointCloud': [500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 305.198425, 190.873642, 138.888733, 109.184929, 89.97085, 76.52718, 66.596344, 58.9627533, 52.91375, 48.003334, 43.93948, 40.5215073, 37.60714, 35.0940552, 32.9049, 30.9822521, 29.2800446, 27.7629089, 26.4031944, 25.1775265, 24.0678177, 23.05848, 22.1366081, 21.2921562, 20.515873, 19.7997761, 19.1377239, 18.5238552, 17.3667583, 16.85289, 16.3733921, 15.9251432, 15.5053892, 15.1116667, 14.74183, 14.3939438, 14.0662823, 13.7573051, 13.465621, 13.1899881, 12.9292688, 12.6824474, 12.4485941, 12.226861, 12.01649, 11.8167715, 11.6270676, 11.44679, 11.2754, 11.1124029, 10.9573383, 10.8097925, 10.6693735, 10.5357218, 10.4085121, 10.2874269, 10.1721945, 10.0625477, 9.958239, 9.859042, 9.764748, 9.675158, 9.590094, 9.509384, 9.432868, 9.360406, 9.291856, 9.22709, 9.16599751, 9.108461, 9.054386, 9.003675, 8.95624, 8.912006, 8.870894, 8.83284, 8.797776, 8.765651, 8.736409, 8.710009, 8.686405, 8.66556, 8.647441, 8.632024, 8.619283, 8.609197, 8.601752, 8.596936, 8.594744, 8.595168, 8.598213, 8.603881, 8.612182, 8.623128, 8.636736, 8.653026, 8.672027, 8.693763, 8.718271, 8.745589, 8.775763, 8.808835, 8.844863, 8.8839035, 8.926022, 8.971289, 9.019776, 9.071569, 9.126757, 9.185434, 9.24771, 9.31369, 9.383495, 9.457261, 9.53512, 9.617225, 9.703741, 9.794838, 9.8907, 9.991533, 10.0975485, 10.2089825, 10.3260851, 10.4491243, 10.57839, 10.7142029, 10.8568945, 11.0068417, 11.16443, 11.3301029, 11.504323, 11.6875944, 11.8804846, 12.0835876, 12.2975731, 12.52315, 12.761116, 13.0123444, 13.27779, 13.5585041, 13.8556557, 14.170536, 14.5045881, 14.8594131, 15.2367773, 15.6386967, 16.067421, 16.52549, 17.0157967, 17.5415783, 18.1065712, 18.7150745, 19.372015, 20.0830765, 20.8549271, 21.69534, 22.613512, 23.6203823, 24.728941, 25.9549732, 27.3176689, 28.840662, 30.55339, 32.49287, 34.7065468, 37.2561035, 40.223156, 43.71822, 47.8943748, 52.97047, 59.2702675, 67.29411, 77.85836, 95.64346, 117.666931, 152.927475, 179.50882, 180.588379, 183.865082, 188.81514, 191.950272, 201.559128, 205.546371, 206.6388, 207.495926, 208.869339, 210.394485, 212.201462, 214.395813, 216.70224, 218.8667, 221.949463, 232.394, 231.776016, 231.526917, 237.032074, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0], 'Counter': 0, 'log': '', 'Reward': 0.0035562878254627784, 'IsGrounded': True, 'IsFlying': False, 'IsTaxiing': False, 'PosXAbs': 0.86777246, 'PosYAbs': 1.44259226, 'PosZAbs': -0.0676386356, 'PosXRel': -0.00222843885, 'PosYRel': -0.327407122, 'PosZRel': -0.09763813, 'RotXAbs': 340.814636, 'RotYAbs': 359.966278, 'RotZAbs': 0.105423734, 'RotXRel': 340.814636, 'RotYRel': 359.966278, 'RotZRel': 0.105423853, 'AngularXVelocity': -0.00521957874, 'AngularYVelocity': -8.102572e-05, 'AngularZVelocity': 0.00138697785, 'LinearXVelocity': -0.00184028456, 'LinearYVelocity': 0.007154375, 'LinearZVelocity': -0.09623016, 'AngularXAcceleration': 5.92379365e-05, 'AngularYAcceleration': -0.0001057535, 'AngularZAcceleration': 0.0, 'LinearXAcceleration': -0.000308157178, 'LinearYAcceleration': -0.030831784, 'LinearZAcceleration': -0.18662475, 'MsgType': 'Output', 'Version': '1.3.0'}


Output
------

When ``IsOutput`` is set to ``True``, the Aircontrol reply with the
following output.

::

   {
      "AGL":0.0,
      "MSL":4.7329154,
      "CurrentRPM":0.0,
      "MaxRPM":4500.0,
      "MaxPower":1.10416677e-05,
      "CurrentPower":0.0,
      "CurrentFuel":0.0,
      "CurrentSpeed":0.0,
      "BankAngle":0.0017182061,
      "IfCollision":false,
      "CollisionObject":"",
      "Latitude":-1.70281146e-06,
      "Longitude":5.317519e-06,
      "PitchAngle":0.328555942,
      "ScreenCapture":"Here the screen capture will be returned as base64 o",
      "LidarPointCloud":[
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         305.198425,
         190.873642,
         138.888733,
         109.184929,
         89.97085,
         76.52718,
         66.596344,
         58.9627533,
         52.91375,
         48.003334,
         43.93948,
         40.5215073,
         37.60714,
         35.0940552,
         32.9049,
         30.9822521,
         29.2800446,
         27.7629089,
         26.4031944,
         25.1775265,
         24.0678177,
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         500.0,
         ...,
         ...,
         ...,
      ],
      "Counter":0,
      "log":"",
      "Reward":0.0035562878254627784,
      "IsGrounded":true,
      "IsFlying":false,
      "IsTaxiing":false,
      "PosXAbs":0.86777246,
      "PosYAbs":1.44259226,
      "PosZAbs":-0.0676386356,
      "PosXRel":-0.00222843885,
      "PosYRel":-0.327407122,
      "PosZRel":-0.09763813,
      "RotXAbs":340.814636,
      "RotYAbs":359.966278,
      "RotZAbs":0.105423734,
      "RotXRel":340.814636,
      "RotYRel":359.966278,
      "RotZRel":0.105423853,
      "AngularXVelocity":-0.00521957874,
      "AngularYVelocity":-8.102572e-05,
      "AngularZVelocity":0.00138697785,
      "LinearXVelocity":-0.00184028456,
      "LinearYVelocity":0.007154375,
      "LinearZVelocity":-0.09623016,
      "AngularXAcceleration":5.92379365e-05,
      "AngularYAcceleration":-0.0001057535,
      "AngularZAcceleration":0.0,
      "LinearXAcceleration":-0.000308157178,
      "LinearYAcceleration":-0.030831784,
      "LinearZAcceleration":-0.18662475,
      "MsgType":"Output",
      "Version":"1.3.0"
   }

.. figure:: ../../docs/images/bankAngle.png
   :alt: Bank Angle

   Bank Angle

Angle between vertical component of the Airplane w.r.t global
vertical.\ `Source <https://en.wikipedia.org/wiki/Flight_dynamics_(fixed-wing_aircraft)>`__

.. figure:: ../../docs/images/pitchAngle.png
   :alt: Pitch Angle

   Pitch Angle

Angle between vertical component of the Airplane w.r.t global
vertical.\ `Source <https://en.wikipedia.org/wiki/Flight_dynamics_(fixed-wing_aircraft)>`__

+-----------------------+-----------------------+-----------------------+
| Measure               | Description           | example               |
+=======================+=======================+=======================+
| AGL                   | Above Ground Level,   | 0.0                   |
|                       | or AGL, describes the |                       |
|                       | literal height above  |                       |
|                       | the ground over which |                       |
|                       | you’re flying.This    |                       |
|                       | also accounts for the |                       |
|                       | building and          |                       |
|                       | mounntain heights.    |                       |
+-----------------------+-----------------------+-----------------------+
| MSL                   | Mean Sea Level, or    | 4.7329154             |
|                       | MSL, is your true     |                       |
|                       | altitude or           |                       |
|                       | elevation. This is    |                       |
|                       | measured as           |                       |
|                       | differrence of        |                       |
|                       | current height and    |                       |
|                       | 0.0 in XZ plane.      |                       |
+-----------------------+-----------------------+-----------------------+
| BankAngle             | The roll angle is     | 0.0                   |
|                       | also known as bank    |                       |
|                       | angle on a fixed-wing |                       |
|                       | aircraft, which       |                       |
|                       | usually “banks” to    |                       |
|                       | change the horizontal |                       |
|                       | direction of flight.  |                       |
|                       | Value in radian limit |                       |
|                       | -1 to +1.             |                       |
+-----------------------+-----------------------+-----------------------+
| PitchAngle            | Angle between the     | 0.328555942           |
|                       | aircraft longitudinal |                       |
|                       | axis and horizontal;  |                       |
|                       | Value in radian limit |                       |
|                       | -1 to +1.             |                       |
+-----------------------+-----------------------+-----------------------+
| CurrentFuel           | [WIP] : Current fuel  | 29.4                  |
|                       | in gallons.           |                       |
+-----------------------+-----------------------+-----------------------+
| CurrentPower          | Current engine power. | 1.10416677e-05        |
+-----------------------+-----------------------+-----------------------+
| Current RPM           | Current Engine RPM.   | 0.0                   |
+-----------------------+-----------------------+-----------------------+
| CurrentSpeed          | Current Airplane      |                       |
|                       | Speed in Knots.       |                       |
+-----------------------+-----------------------+-----------------------+
| MaxRPM                | : Max engine RPM.     | 4500.0                |
+-----------------------+-----------------------+-----------------------+
| ScreenCapture         | Captured Screenshot,  | Base 64 Image         |
|                       | if proper trigger set |                       |
|                       | from ``set_camera``   |                       |
|                       | function              |                       |
+-----------------------+-----------------------+-----------------------+
| LidarPointCloud       | Captured Lidar point  | Vector : [500.0,      |
|                       | cloud, if proper      | 500.0, 500.0, …, 25,  |
|                       | trigger set from      | 190.873642,           |
|                       | ``set_lidar``         | 138.888733,           |
|                       | function              | 109.184929]           |
+-----------------------+-----------------------+-----------------------+
| Counter               | Number of request the | 13                    |
|                       | client has sent to    |                       |
|                       | the server.           |                       |
+-----------------------+-----------------------+-----------------------+
| log                   | : Log of events, if   |                       |
|                       | requested.            |                       |
+-----------------------+-----------------------+-----------------------+
| Reward                | Internal example      | 0.0035562878254627784 |
|                       | reward system. user   |                       |
|                       | can define their own  |                       |
|                       | reward system         |                       |
|                       | externally.           |                       |
+-----------------------+-----------------------+-----------------------+
| IsGrounded            | Indicates if the      | true                  |
|                       | Airplane is grounded. |                       |
+-----------------------+-----------------------+-----------------------+
| IsFlying              | Indicates if the      | false                 |
|                       | Airplane is flying.   |                       |
+-----------------------+-----------------------+-----------------------+
| IsTaxiing             | Indicates if the      | false                 |
|                       | Airplane is taxing on |                       |
|                       | runway.               |                       |
+-----------------------+-----------------------+-----------------------+
| PosXAbs               | Absolute position on  | 0.86777246            |
|                       | X axis.               |                       |
+-----------------------+-----------------------+-----------------------+
| PosYAbs               | Absolute position on  | 1.44259226            |
|                       | Y axis.               |                       |
+-----------------------+-----------------------+-----------------------+
| PosZAbs               | Absolute position on  | -0.0676386356         |
|                       | Z axis.               |                       |
+-----------------------+-----------------------+-----------------------+
| PosXRel               | Relative position     | -0.00222843885        |
|                       | w.r.t start position  |                       |
|                       | of the Airplane on X  |                       |
|                       | axis.                 |                       |
+-----------------------+-----------------------+-----------------------+
| PosYRel               | Absolute position     | -0.327407122          |
|                       | w.r.t start position  |                       |
|                       | of the Airplane on Y  |                       |
|                       | axis.                 |                       |
+-----------------------+-----------------------+-----------------------+
| PosZRel               | Absolute position     | -0.09763813           |
|                       | w.r.t start position  |                       |
|                       | of the Airplane on Z  |                       |
|                       | axis.                 |                       |
+-----------------------+-----------------------+-----------------------+
| RotXAbs               | Absolute rotation on  | 340.814636            |
|                       | X axis.               |                       |
+-----------------------+-----------------------+-----------------------+
| RotYAbs               | Absolute rotation on  | 359.966278            |
|                       | Y axis.               |                       |
+-----------------------+-----------------------+-----------------------+
| RotZAbs               | Absolute rotation on  | 0.105423734           |
|                       | Z axis.               |                       |
+-----------------------+-----------------------+-----------------------+
| RotXRel               | Absolute rotation     | 340.814636            |
|                       | w.r.t start rotation  |                       |
|                       | of the Airplane on Y  |                       |
|                       | axis.                 |                       |
+-----------------------+-----------------------+-----------------------+
| RotYRel               | Absolute rotation     | 359.966278            |
|                       | w.r.t start rotation  |                       |
|                       | of the Airplane on Y  |                       |
|                       | axis.                 |                       |
+-----------------------+-----------------------+-----------------------+
| RotZRel               | Absolute position     | 0.105423853           |
|                       | w.r.t start rotation  |                       |
|                       | of the Airplane on Y  |                       |
|                       | axis.                 |                       |
+-----------------------+-----------------------+-----------------------+
| AngularXVelocity      | Angular velocity on X | -0.00521957874        |
|                       | axis.                 |                       |
+-----------------------+-----------------------+-----------------------+
| AngularYVelocity      | Angular velocity on Y | -8.102572e-05         |
|                       | axis.                 |                       |
+-----------------------+-----------------------+-----------------------+
| AngularZVelocity      | Angular velocity on Z | 0.00138697785         |
|                       | axis.                 |                       |
+-----------------------+-----------------------+-----------------------+
| LinearXVelocity       | Linear velocity on X  | -0.00184028456        |
|                       | axis.                 |                       |
+-----------------------+-----------------------+-----------------------+
| LinearYVelocity       | Linear velocity on Y  | 0.007154375           |
|                       | axis.                 |                       |
+-----------------------+-----------------------+-----------------------+
| LinearZVelocity       | Linear velocity on Z  | -0.09623016           |
|                       | axis.                 |                       |
+-----------------------+-----------------------+-----------------------+
| AngularXAcceleration  | Angular acceleration  | 5.92379365e-05        |
|                       | on X axis.            |                       |
+-----------------------+-----------------------+-----------------------+
| AngularYAcceleration  | Angular acceleration  | -0.0001057535         |
|                       | on Y axis.            |                       |
+-----------------------+-----------------------+-----------------------+
| AngularZAcceleration  | Angular acceleration  | 0.0                   |
|                       | on Z axis.            |                       |
+-----------------------+-----------------------+-----------------------+
| LinearXAcceleration   | Linear acceleration   | -0.000308157178       |
|                       | on Z axis.            |                       |
+-----------------------+-----------------------+-----------------------+
| LinearYAcceleration   | Linear acceleration   | -0.030831784          |
|                       | on Z axis.            |                       |
+-----------------------+-----------------------+-----------------------+
| LinearZAcceleration   | Linear acceleration   | -0.18662475           |
|                       | on Z axis.            |                       |
+-----------------------+-----------------------+-----------------------+
| MsgType               | Message type will be  | Output                |
|                       | output. Example       |                       |
+-----------------------+-----------------------+-----------------------+
| Version               | Version of the        | 1.3.0                 |
|                       | server.               |                       |
+-----------------------+-----------------------+-----------------------+

