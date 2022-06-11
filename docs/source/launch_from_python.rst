Launching Unity Environment From Python
=======================================

It is possible to launch and terminate the environment from Python.

This provide additional level of fexibilty where you will not have to
lauch environment manually everytime.

.. code:: ipython3

    from airctrl.utils import unity

Use ``Launch()`` class from ``unity`` subpackage to create and object

.. code:: ipython3

    L = unity.Launch()

Use function ``launch_executable()`` to launch an environment.
``launch_executable()`` takes path where the executable is located. Use
suitable path according to OS.

.. code:: ipython3

    process = L.launch_executable("/home/sunil/Games/AirControl_2020/Build/Linux/v1.0.0-AirControl.x86_64")

Once done you can call ``terminate()`` function call to kill the
environment.

.. code:: ipython3

    process.terminate()

