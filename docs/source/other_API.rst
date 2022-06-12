Aircontrol Other API
====================

Importing requirements
----------------------

.. code:: ipython3

    from airctrl import environment 
    from airctrl import sample_generator
    from airctrl.utils import unity
    from airctrl import sample_generator
    port=8999
    sample = sample_generator.samples()

.. code:: ipython3

    env =  environment.Trigger()
    L = unity.Launch()
    process = L.launch_executable("/home/supatel/Games/AirControl_2021/Build/1.3.0/Linux/v1.3.0-AirControl.x86_64", server_port=port)
    env.get_connected(port=port)


.. parsed-literal::

    [32mNow call method `.get_connected(port=<Default 8053>)` to get connected
    [0m
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

    audio_control = env.set_audio(EnableAudio=False, IsActive=True)
    print(audio_control)


.. parsed-literal::

    {'Log': 'Active scene camera - 1 Capture camera - 0 Width - 256  Height - 256: '}


.. code:: ipython3

    ui_control = env.set_ui(ShowUIElements=True, IsActive= True)
    print(ui_control)


.. parsed-literal::

    {'Log': 'Active scene camera - 1 Capture camera - 0 Width - 256  Height - 256: '}


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

    output = env.reset(IsOutput=True)
    print(output)


.. parsed-literal::

    {'AGL': 0.0, 'MSL': 4.629269, 'CurrentRPM': 0.0, 'MaxRPM': 4500.0, 'MaxPower': 1.731072e-05, 'CurrentPower': 0.0, 'CurrentFuel': 0.0, 'CurrentSpeed': 0.0, 'BankAngle': 0.04627315, 'IfCollision': False, 'CollisionObject': '', 'Latitude': -0.000172575281, 'Longitude': 5.26217764e-06, 'PitchAngle': 0.334689736, 'ScreenCapture': '', 'LidarPointCloud': [500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 448.642853, 234.270966, 158.557083, 119.857109, 96.36547, 80.59447, 69.27724, 60.7635, 54.1279564, 48.81236, 44.4597244, 40.8312149, 37.7611237, 35.1299248, 32.85116, 30.85884, 29.1028557, 27.543745, 26.1508026, 24.89903, 23.7687283, 22.7430458, 21.8084869, 20.9541874, 20.17015, 19.4480934, 18.781765, 18.1649017, 17.5923538, 17.0599041, 16.5634632, 16.0998478, 15.6664734, 15.2599268, 14.878726, 14.5203571, 14.1829128, 13.8650055, 13.5649071, 13.2816982, 13.0139561, 12.7605734, 12.5205307, 12.2931213, 12.07737, 11.8725891, 11.6782169, 11.4934578, 11.3180656, 11.1509838, 10.9922924, 10.8411655, 10.6975231, 10.560667, 10.43038, 10.3064928, 10.1884851, 10.076252, 9.969445, 9.8677845, 9.771229, 9.67941, 9.592241, 9.509407, 9.430925, 9.356548, 9.286107, 9.219494, 9.156597, 9.097355, 9.041557, 8.989214, 8.940166, 8.894346, 8.85165, 8.81202, 8.77549, 8.741838, 8.71110249, 8.683243, 8.658138, 8.635839, 8.616283, 8.59939, 8.585234, 8.573659, 8.564738, 8.558472, 8.554833, 8.553766, 8.555275, 8.559478, 8.566202, 8.5756, 8.58765, 8.602289, 8.619665, 8.639746, 8.662611, 8.688187, 8.71656, 8.747832, 8.781985, 8.819149, 8.859278, 8.902578, 8.949003, 8.99863, 9.051601, 9.108125, 9.167935, 9.231453, 9.298783, 9.369947, 9.44515, 9.524359, 9.608, 9.696024, 9.78858948, 9.886199, 9.988784, 10.0965548, 10.2099037, 10.3288908, 10.453907, 10.5853653, 10.7233849, 10.8685217, 11.0208683, 11.18111, 11.3496485, 11.5269909, 11.7134, 11.90961, 12.1162586, 12.3342743, 12.56382, 12.8061886, 13.062355, 13.3328524, 13.6190958, 13.9223022, 14.2436848, 14.5847187, 14.9475231, 15.3331957, 15.744544, 16.1833267, 16.6527042, 17.15538, 17.6949978, 18.2752438, 18.901001, 19.5771732, 20.3098831, 21.1067047, 21.97527, 22.9256248, 23.9692974, 25.1207275, 26.3968716, 27.81858, 29.4116058, 31.2081165, 33.24909, 35.5875359, 38.2924347, 41.45584, 43.89486, 48.2217255, 53.4786873, 60.1293564, 68.6363, 79.97531, 99.9875641, 125.519127, 168.6308, 181.557327, 184.687927, 200.397171, 203.654251, 204.736618, 206.287659, 207.92598, 209.655365, 211.9318, 214.359268, 216.515945, 219.109375, 222.7964, 226.68, 231.220367, 237.020813, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0, 500.0], 'Counter': 0, 'log': '', 'Reward': 0.0034183502709721368, 'IsGrounded': True, 'IsFlying': False, 'IsTaxiing': False, 'PosXAbs': 0.8587413, 'PosYAbs': 1.41092455, 'PosZAbs': -6.854991, 'PosXRel': -0.0112596154, 'PosYRel': -0.359074831, 'PosZRel': -6.88499069, 'RotXAbs': 340.446533, 'RotYAbs': 357.65744, 'RotZAbs': 2.8175633, 'RotXRel': 340.446533, 'RotYRel': 357.65744, 'RotZRel': 2.81756353, 'AngularXVelocity': 0.000147444662, 'AngularYVelocity': -0.00334466086, 'AngularZVelocity': 0.00412675971, 'LinearXVelocity': 0.006331574, 'LinearYVelocity': -0.0038418388, 'LinearZVelocity': -0.55398947, 'AngularXAcceleration': 0.0005955342, 'AngularYAcceleration': -0.0006038579, 'AngularZAcceleration': 0.0, 'LinearXAcceleration': 0.00348708127, 'LinearYAcceleration': -0.000720331445, 'LinearZAcceleration': -0.0345647335, 'MsgType': 'Output', 'Version': '1.3.0'}


Collision Detection
-------------------

Colision detection is an integral part of the Aircontrol. As part of
Aircontrol at each response following data field provide information
regarding collision.

::

   'IfCollision': False, 'CollisionObject': "Ground"

Work In Progress
================

Fuel API
--------

Can be used to control initial fuel amount and fuel consumption rate.

Weather API
-----------

Can be used to control environmantal fog and clouds. Planned for HDRP
release only.
