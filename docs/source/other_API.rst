Aircontrol Other API
====================

Importing requirements
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

.. code:: python

   ui_audio_schema = schemaDef.set_uiaudio( ShowUIElements=False,EnableAudio=True,IsOutput=False)
   connection.send_data(ui_audio_schema)
   output =  connection.receive_data()

Level Reset
-----------

Can be used to reset the level at each iteration. Typically
Reinforcement learning loops requires hundreds and thousands environment
reset

.. _python-api-1:

Python API
----------

-  InputControlType (str, optional): It can be either ``Code`` or
   ``Other``. This is to control the internal mechanism and prevent
   repeted calling in already set variables.
   If ``InputControlType`` is set to ‘Code’, camera cannot be controlled
   from Keyboard or Joystick. If ``InputControlType`` is set to ‘Other’,
   camera can be only controlled from Keyboard or Joystick. Defaults to
   “Code”.
-  IsActive (bool, optional): Internal mechanism to save compute, set to
   ``True`` when level reset required
-  LevelReload (bool, optional): set to ``True`` to reset the level.
   Defaults to False.
-  IsOutput (bool, optional): By default ``reset_level`` function only
   sets the internal state. ``reset_level`` only provide log outout and
   not the actual captured image. ``set_control`` when called it returns
   actual output. IF you want to force ``reset_level`` to return image,
   set ``IsOutput`` to True. Defaults to False.

.. code:: python

   level_reset_schema = schemaDef.reset_level( IsActive=True, LevelReload=True,IsOutput=False)
   connection.send_data(level_reset_schema)
   output =  connection.receive_data()

Work In Progress
===============
Following API are still in development

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
