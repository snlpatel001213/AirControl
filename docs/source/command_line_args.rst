Command Line Argument To Server
===============================

AirControl takes following commandline arguments while launching the
server

.. code:: bash


   ./Aircontrol_executable port --serverPort <port on which server will start> --clientIP <client IP addess> --clientPort <client Port>

Multiple server can be launced in parallel for simultaneous experiments.

Client info is dispayed on the server screen to help distinguish which
server is connected to which client.

Python API for setting Client info at launch
--------------------------------------------

.. code:: python

   from airctrl.utils import unity
   L = unity.Launch()
   process = L.launch_executable("path to unity build executable",server_port=8053, client_ip=None, sleeptime=5)

The function takes in the name of the executable file, the server port,
the client ip and the sleep time.

It then checks if the executable file is present in the environment
folder. If it is, it launches the executable file with the server port,
client ip and client port as arguments.

It then sleeps for the specified time to allow the environment to load.

::

   file_name: The name of the executable file to launch
   server_port: The port that the server will listen on, defaults to 8053 (optional)
   client_ip: The IP address of the client machine
   sleeptime: The time to wait for the environment to load, defaults to 5 (optional)

The function returns process object (pid) to the server.

This procees object then can be used to kill the process as
``process.kill()``

Python API for setting Client info at runtime
---------------------------------------------

.. code:: python

   from airctrl import environment 
   env =  environment.Trigger()
   env.get_connected()
   env.set_clientInfo(IsActive=False, ClientPort=""):

This function is used to set the client information

::

   IsActive: True or False, defaults to False (optional)
   ClientPort: The port number that the client is listening on
