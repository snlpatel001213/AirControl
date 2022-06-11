# Simple taking off through python script
-----------------------------------------

In this notebook, We will implement python client to take off the
Airplane

1. Import the Necessary Packages
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

.. code:: ipython3

    from airctrl import environment
    
    import time
    from pprint import pprint
    import PIL.Image as Image
    from collections import deque
    import base64
    import numpy as np
    import matplotlib.cm as cm
    from io import BytesIO
    from matplotlib.pyplot import  imshow
    import matplotlib.pyplot as plt
    import torch
    import matplotlib.animation as animation
    from airctrl import sample_generator
    
    from tqdm import tqdm
    
    sample = sample_generator.samples()
    A =  environment.Trigger()
    
    %matplotlib inline


.. parsed-literal::

    Now play the environment and call call method `Action.get_connected` to get connected


.. code:: ipython3

    # get connected to server
    A.get_connected()

2. Instantiate the Environment and Agent
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Initialize the environment in the code cell below.

.. code:: ipython3

    
    def reset():
        output = A.reset(IsOutput=True)
        A.set_audio(EnableAudio=False, IsActive=True)
        A.set_ui(ShowUIElements=True, IsActive= True)
        A.set_camera(ActiveCamera=1, IsActive=True, IsCapture=True, CaptureCamera=1, CaptureType=0,CaptureHeight=540, CaptureWidth=960)
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
        normalizedRPM =  output['CurrentRPM']/output['MaxRPM']
        normalizedPower =  output['CurrentPower']/output['MaxPower']
        normalizedSpeed = output['CurrentSpeed']/150
        pitchAngle = output['PitchAngle']
        bankAngle = output['BankAngle']
        ifCollision = output['IfCollision']
        collisionObject = output['CollisionObject']
        Reward = output["Reward"]
        feature_vector = [MSL, Latitude, Longitude, normalizedRPM, normalizedPower, normalizedSpeed, pitchAngle, bankAngle]
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
    output = reset()
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
            output = A.step(Pitch=pitch, Yaw=yaw, Roll=roll, StickyThrottle=stickyThrottle)
            features,reward, ifCollided,collisionObject = output_to_Features(output)
            image = output['ScreenCapture']
            if image != "":
                im = Image.open(BytesIO(base64.b64decode(image)))
                imshow(np.asarray(im))
                frames.append(im)
                # Save into a GIF file that loops forever
                frames[0].save('sample.gif', format='GIF',append_images=frames[0:],save_all=True,duration=300, loop=0)
            if ifCollided:
                if "stuck" in output["log"]:
                    print(output["log"])
                print("🔁 Reset Triggered , Collided with {0} ".format(collisionObject))
                output = reset()
                
                

Show Created Sequence
~~~~~~~~~~~~~~~~~~~~~

.. figure:: sample.gif
   :alt: segment

   SegmentLocal
