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

.. code:: python

   control_schema = schemaDef.set_control(Pitch=0.0, Roll=0.0, Yaw=0.0,Throttle=0.0, StickyThrottle=0.5, Brake=0, Flaps=0,IsOutput=True)
   connection.send_data(control_schema)
   pprint(connection.receive_data())

::

   {'AGL': 0.0,
    'BankAngle': -2.93484618e-07,
    'CurrentFuel': 0.0,
    'CurrentPower': 4666.8457,
    'CurrentRPM': 2333.42285,
    'CurrentSpeed': 0.0,
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
                        26.1899033,
                        25.8373413,
                        25.501812,
                        25.1824684,
                        24.8784943,
                        24.58918,
                        24.3138351,
                        24.05183,
                        23.8025932,
                        23.5655651,
                        23.3402615,
                        23.1261883,
                        22.9229374,
                        22.73009,
                        22.5472622,
                        22.3741283,
                        22.2103329,
                        22.0555973,
                        21.9096222,
                        21.7721653,
                        21.6429653,
                        21.5218048,
                        21.4084873,
                        21.302803,
                        21.2045918,
                        21.113678,
                        21.0299244,
                        20.95319,
                        20.8833466,
                        20.8202953,
                        2.16295123,
                        2.10318828,
                        2.07183218,
                        2.05952,
                        1.93164682,
                        1.93017542,
                        1.92929351,
                        1.92899966,
                        1.92929351,
                        1.9301753,
                        20.5536346,
                        20.5672379,
                        20.58713,
                        20.613348,
                        20.6459274,
                        20.6849289,
                        20.7303963,
                        20.7824154,
                        20.8410511,
                        20.906414,
                        20.9785957,
                        21.05771,
                        21.1438961,
                        21.2372818,
                        21.3380241,
                        21.4462948,
                        21.56226,
                        21.6861439,
                        21.8181286,
                        21.9584751,
                        22.1074066,
                        22.2652,
                        22.432148,
                        22.6085567,
                        22.7947559,
                        22.9911118,
                        23.1980152,
                        23.4158649,
                        23.6451187,
                        23.8862572,
                        24.13979,
                        24.4062767,
                        24.6863136,
                        24.9805546,
                        25.28969,
                        25.6144676,
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
                        77.71343,
                        83.01117,
                        89.11312,
                        96.21514,
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
    'MSL': -4.151844,
    'MaxPower': 5000.0,
    'MaxRPM': 2500.0,
    'MsgType': 'Output',
    'PitchAngle': 1.66752621e-08,
    'ScreenCapture': '',
    'Version': '0.0.4'}

Output
------

When ``IsOutput`` is set to ``True``, the Aircontrol reply with the
following output.

::

   {'AGL': 0.0,
    'BankAngle': -2.93484618e-07,
    'CurrentFuel': 0.0,
    'CurrentPower': 4666.8457,
    'CurrentRPM': 2333.42285,
    'CurrentSpeed': 0.0,
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
    'MaxRPM': 2500.0,
    'MsgType': 'Output',
    'PitchAngle': 1.66752621e-08,
    'ScreenCapture': '',
    'Version': '0.0.4'}

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

Glossary
--------

**AGL** : Above Ground Level, or AGL, describes the literal height above
the ground over which you’re flying.This also accounts for the building
and mounntain heights

**MSL** : Mean Sea Level, or MSL, is your true altitude or elevation.
This is measured as differrence of current height and 0.0 in XZ plane

**BankAngle** : The roll angle is also known as bank angle on a
fixed-wing aircraft, which usually “banks” to change the horizontal
direction of flight

**PitchAngle** : angle between the aircraft longitudinal axis and
horizontal;

**CurrentFuel** [WIP] : Current fuel in gallons

**CurrentPower** : Current engine power

**CurrentRPM** : Current Engine RPM

**CurrentSpeed** : Current Airplane Speed in Knots

**MaxRPM** : Max engine RPM

**ScreenCapture** : Captured Screenshot, if proper trigger set from
``set_camera`` function

**LidarPointCloud** : Captured Lidar point cloud, if proper trigger set
from ``set_lidar`` function
