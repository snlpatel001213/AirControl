Aircontrol Lidar API
====================

The Aircontrol Lidar API facilitates communication with simulated LIDAR
in Unity. The lidar script uses the Raycast feature of Unity Game
Engine. The distance array, of float type, consists of 360 members, the
ith member storing the distance at which the ray hit an object at the
ith degree.

Importing Requirements
----------------------

.. code:: python

   from AirControl.communicator import Communicator
   from AirControl import schemaDef
   from pprint import pprint
   import PIL.Image as Image
   import base64
   import numpy as np
   from io import BytesIO
   from matplotlib.pyplot import  imshow
   import matplotlib.pyplot as plt

   connection = Communicator()

Lidar Placement
---------------

Lidar is placed in the cockpit, Attached to the cockpit camera. This
ensures that all the distance are measured with respect to the cockpit.

Python API
----------

-  InputControlType (str, optional): It can be either ``Code`` or
   ``Other``. This is to control the internal mechanism and prevent
   repeated calling in already set variables.
   If ``InputControlType`` is set to ‘Code’, the camera cannot be
   controlled from Keyboard or Joystick. If ``InputControlType`` is set
   to ‘Other’, the camera can be only controlled from Keyboard or
   Joystick. Defaults to “Code”.
-  Range (float, optional): Range of the Lidar. Defaults to 100000.0.
-  Density (int, optional): Number of Raycast spread across 360 degrees.
   Defaults to 360.
-  IsActive (bool, optional): If lidar is set to active or not. Defaults
   to False.
-  IsOutput (bool, optional): By default ``set_lidar`` function only
   sets the internal state. ``set_lidar`` only provides log output and
   not the actual captured image. ``set_control`` when called it returns
   the actual output. IF you want to force ``set_lidar`` to return the
   image, set ``IsOutput`` to True. Defaults to False.

.. code:: python

   lidar_schema = schemaDef.set_lidar(InputControlType="Code", Range=100000.0, Density=360, IsActive=False,IsOutput=True)
   connection.send_data(lidar_schema)
   output =  connection.receive_data()
   lidar_output = output['LidarPointCloud'] # get lidar output

Alternatively one can specify ``IsOutput`` to ``False`` and get output
using reply to ``schemaDef.set_control`` function call.

.. code:: python

   lidar_schema = schemaDef.set_lidar(InputControlType="Code", Range=100000.0, Density=360, IsActive=False,IsOutput=False)
   connection.send_data(lidar_schema)
   output =  connection.receive_data() # just log output
   # use set_control function
   control_schema = schemaDef.set_control(IsOutput=True)
   connection.send_data(control_schema)
   output =  connection.receive_data() # returns the output
   lidar_output = output['LidarPointCloud'] # get lidar output

.. code:: python

   print(lidar_output)

::

   [100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 3541.46948, 100000.0, 100000.0, 4579.34229, 100000.0, 4252.557, 4228.008, 3195.66162, 100000.0, 100000.0, 3383.261, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 3064.81274, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 3247.62964, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 2304.10059, 1537.2821, 1147.36169, 883.810669, 735.712463, 623.99176, 543.328064, 483.7483, 430.048248, 387.699951, 357.491638, 331.899872, 309.815643, 290.2349, 272.333374, 256.70813, 242.8495, 230.4774, 219.368225, 209.341675, 200.162781, 191.678848, 183.938644, 176.851074, 170.385757, 164.598373, 159.238022, 154.2614, 149.630539, 145.312668, 141.2788, 137.50354, 133.9558, 130.569443, 127.387962, 124.3948, 121.575249, 118.91613, 116.4055, 114.032768, 111.788139, 109.662941, 107.649239, 105.739754, 103.927971, 102.207817, 100.573868, 99.02093, 97.54457, 96.1403961, 94.80456, 93.5334549, 92.3236847, 91.17226, 90.07631, 89.033165, 88.04089, 87.097374, 86.19983, 85.34636, 84.53507, 83.7643661, 83.03264, 82.3384857, 81.68048, 80.00795, 79.42381, 78.87199, 78.2124252, 2.16294813, 2.10318685, 2.07182956, 2.05951929, 1.93164361, 1.93017209, 1.92929, 1.92899632, 1.92929018, 1.93017209, 74.47046, 74.2700653, 74.09329, 73.93981, 73.80942, 73.70189, 73.6170654, 73.5548, 73.51503, 73.49767, 74.5169144, 74.5467, 74.59924, 74.67463, 74.77298, 74.89441, 75.04424, 75.22162, 75.42285, 75.64827, 75.89823, 76.17313, 76.47339, 76.7995453, 77.15207, 77.53157, 77.93868, 78.37412, 78.83857, 79.33288, 79.8579, 80.41457, 81.0039, 81.62701, 82.28501, 82.9791946, 83.71092, 84.4816, 85.29285, 86.1463242, 87.04383, 87.9873047, 88.9788742, 90.02076, 91.11543, 92.26549, 93.4738159, 94.74348, 94.20772, 95.55958, 96.98076, 98.47532, 100.047653, 101.702431, 103.444923, 105.280815, 107.216248, 109.2582, 111.406357, 113.6918, 118.931969, 121.601372, 124.4321, 127.437485, 130.632385, 134.033508, 137.6596, 141.5316, 145.67334, 150.088135, 154.8046, 159.877563, 165.346115, 171.152832, 177.261459, 183.866272, 191.042969, 198.8655, 207.421921, 216.8167, 227.191147, 239.23407, 252.7061, 267.872559, 283.752716, 301.516571, 321.7583, 347.208282, 378.6034, 415.9193, 458.241028, 508.348938, 571.6979, 656.359253, 770.511841, 984.6023, 1231.24841, 1627.99158, 2449.77759, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0, 100000.0]

As the Airplane is a tail dagger, the backside rays of lidar interact
with a ground plane. Due to this hit distance for some of the rays in
the above-shown lidar output is very near to airplane.

|image0|

Similarly, lidar interacts with buildings and provides a sense of
nearness.

|image1|

1. `Physics.Raycast <https://docs.unity3d.com/ScriptReference/Physics.Raycast.html>`__

.. |image0| image:: ../images/lidar/5.png
.. |image1| image:: ../images/lidar/9.png
