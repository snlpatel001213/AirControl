# Simple taking off through python script
-----------------------------------------

In this notebook, We will implement python client to take off the
Airplane

1. Import the Necessary Packages
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

.. code:: ipython3

    import sys
    sys.path.append("../../")

.. code:: ipython3

    import sys
    from pprint import pprint
    import PIL.Image as Image
    import base64
    import numpy as np
    from io import BytesIO
    from matplotlib.pyplot import  imshow
    import matplotlib.pyplot as plt
    %matplotlib inline

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


2. Instantiate the Environment and Agent
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Initialize the environment in the code cell below.

.. code:: ipython3

    def env_reset():
        output = env.reset(IsOutput=True)
        env.set_audio(IsActive=True, EnableAudio=False)
        env.set_ui(ShowUIElements=True, IsActive= True)
        env.set_camera(ActiveCamera=1, IsActive=True, IsCapture=True, CaptureCamera=1, CaptureType=0,CaptureHeight=540, CaptureWidth=960)
        env.set_lidar(IsActive=False)
        return output

.. code:: ipython3

    def output_to_Features(output):
        """
        output2features(output)
        Args:
            ```
            output ([type]): [description]
            >>> ([0.905434847,
            0.00182869844,
            0.000590562,
            0.0,
            0.0,
            0.021583642933333334,
            0.325318575,
            0.0460257],
            True)
            ```
        Returns:
            [type]: flight status
            [bool]: if collided
        """
        MSL = output['MSL']
        Latitude = output['Latitude']
        Longitude = output['Longitude']
        normalizedRPM =  output['CurrentRPM']
        normalizedPower =  output['CurrentPower']
        normalizedSpeed = output['CurrentSpeed']
        pitchAngle = output['PitchAngle']
        bankAngle = output['BankAngle']
        ifCollision = output['IfCollision']
        collisionObject = output['CollisionObject']
        Reward = output["Reward"] # Normalizing rewards
        IsGrounded = 1.0 if(output["IsGrounded"]) else 0.0
        IsFlying = 1.0 if(output["IsFlying"]) else 0.0
        IsTaxiing = 1.0 if(output["IsTaxiing"]) else 0.0
        PosXAbs = (output["PosXAbs"])
        PosYAbs = (output["PosYAbs"])
        PosZAbs = (output["PosZAbs"])
        PosXRel = (output["PosXRel"])
        PosYRel = (output["PosYRel"])
        PosZRel = (output["PosZRel"])
        RotXAbs = (output["RotXAbs"])
        RotYAbs = (output["RotYAbs"])
        RotZAbs = (output["RotZAbs"])
        RotXRel = (output["RotXRel"])
        RotYRel = (output["RotYRel"])
        RotZRel = (output["RotZRel"])
        LidarPointCloud = 1.0-np.asarray(output['LidarPointCloud'])
    
        feature_vector = [MSL, Latitude, Longitude, normalizedRPM, normalizedPower, normalizedSpeed, pitchAngle, \
                          bankAngle, IsGrounded, IsFlying, IsTaxiing, ifCollision, \
                         PosXAbs, PosYAbs, PosZAbs, PosXRel, PosYRel, PosZRel,RotXAbs,RotYAbs,RotZAbs,RotXRel,RotYRel,RotZRel, ifCollision] + LidarPointCloud.tolist()
    
        return np.asarray(feature_vector),Reward, ifCollision,collisionObject

.. code:: ipython3

    def act(self, state, eps=0.):
            """Returns actions for given state as per current policy.
            
            Params
            ======
                state (array_like): current state
                eps (float): epsilon, for epsilon-greedy action selection
            """
            # Using Random policy
            return [sample.get_random_pitch(), sample.get_random_yaw(), sample.get_random_roll(), sample.get_random_stickythrottle()]

Simple Loop to trigger actions on the plane
-------------------------------------------

.. code:: ipython3

    # watch an untrained agent
    output = env_reset()
    features,reward, ifCollided,_ = output_to_Features(output)
    eps = 1.0
    frames = [] # for storing the generated images
    fig = plt.figure()
    while (True):
            action = act(features, eps)
            pitch =  action[0]
            yaw = action[1]
            roll= action[2]
            stickyThrottle=action[3]
            # print(pitch, yaw, roll, stickyThrottle)
            output = env.step(Pitch=pitch, Yaw=yaw, Roll=roll, StickyThrottle=stickyThrottle)
            features,reward, ifCollided,collisionObject = output_to_Features(output)
            image = output['ScreenCapture']
            if image != "":
                im = Image.open(BytesIO(base64.b64decode(image)))
                frames.append(im)
                # Save into a GIF file that loops forever
                frames[0].save('sample.gif', format='GIF',append_images=frames[0:],save_all=True,duration=300, loop=0)
            if ifCollided:
                if "stuck" in output["log"]:
                    print(output["log"])
                print("🔁 Reset Triggered , Collided with {0} ".format(collisionObject))
                break
                
                


.. parsed-literal::

    🔁 Reset Triggered , Collided with Runway 



.. parsed-literal::

    <Figure size 432x288 with 0 Axes>


Show Created Sequence
~~~~~~~~~~~~~~~~~~~~~

.. figure:: sample.gif
   :alt: segment

   SegmentLocal

