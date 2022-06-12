Configuring Airplane
====================

Starting with version V1.5.0 Airplane characteristics can be configured
by using ``AirplaneProperties.json``. In the build for each operating
system, this file ``AirplaneProperties.json`` can be found at following
location.

Ubuntu : ``Linux/v1.5.0-AirControl_Data/StreamingAssets`` Ubuntuheadless
: ``Linux/v1.5.0-AirControl_Data/StreamingAssets`` Windows :
``Windows/v1.5.0-AirControl_Data/StreamingAssets`` Mac :
``Mac/v1.5.0-AirControl.app/Contents/Resources/Data/StreamingAssets``

In the editor, it is located at :
``Assets/StreamingAssets/AirplaneProperties.json``

The file looks like as follow

.. code:: json

   {
     "General/docversion": "1.3.0",
     "General/priority": 1,
     "General/activeAirplane": "Cessna152",
     "Cessna152/cameraHeight": 6,
     "Cessna152/cameraDistance": 12,
     "Cessna152/minHeaightFromGround": 4,
     "Cessna152/maxMPH": 150,
     "Cessna152/rbLerpSpeed": 0.1,
     "Cessna152/maxLiftPower": 100,
     "Cessna152/flapLiftPower": 500,
     "Cessna152/dragFactor": 0.01,
     "Cessna152/flapDragFactor": 0.001,
     "Cessna152/pitchSpeed": 5000,
     "Cessna152/rollSpeed": 4000,
     "Cessna152/yawSpeed": 5000,
     "Cessna152/airplaneWeight": 900,
     "Cessna152/smoothSpeed": 4,
     "Cessna152/maxAngle": 30,
     "Cessna152/maxRPM": 4500,
     "Cessna152/shutOffSpeed": 2,
     "Cessna152/propellerSpan": 1.6,
     "Cessna152/fuelCapacity": 29,
     "Cessna152/fuelBurnRate": 6.1,
     "Cessna152/groundDistance": 3,
     "Cessna152/liftForce": 50,
     "Cessna152/maxSpeed": 15,
     "Cessna152/throttleSpeed": 0.1,
     "Cessna152/brakePower": 500,
     "Cessna152/steerAngle": 20,
     "F4UCorsair/cameraHeight": 25,
     "F4UCorsair/cameraDistance": 25,
     "F4UCorsair/minHeaightFromGround": 20,
     "F4UCorsair/maxMPH": 360,
     "F4UCorsair/rbLerpSpeed": 0.1,
     "F4UCorsair/maxLiftPower": 200,
     "F4UCorsair/flapLiftPower": 1000,
     "F4UCorsair/dragFactor": 0.01,
     "F4UCorsair/flapDragFactor": 0.0005,
     "F4UCorsair/pitchSpeed": 25000,
     "F4UCorsair/rollSpeed": 25000,
     "F4UCorsair/yawSpeed": 25000,
     "F4UCorsair/airplaneWeight": 2500,
     "F4UCorsair/smoothSpeed": 4,
     "F4UCorsair/maxAngle": 30,
     "F4UCorsair/maxRPM": 6500,
     "F4UCorsair/shutOffSpeed": 2.5,
     "F4UCorsair/propellerSpan": 2.2,
     "F4UCorsair/fuelCapacity": 75,
     "F4UCorsair/fuelBurnRate": 12,
     "F4UCorsair/groundDistance": 6,
     "F4UCorsair/liftForce": 150,
     "F4UCorsair/maxSpeed": 30,
     "F4UCorsair/throttleSpeed": 0.06,
     "F4UCorsair/brakePower": 500,
     "F4UCorsair/steerAngle": 25
   }

For each airplane the properties are pre-defined as default for each
airplane. These properties exist inside the compiled build as default
configuration. To override your configuration over the default, set the
key\`\ ``"General/priority"`` greater then 1. The default/current
priority is 1. if external json config sets the priority higehr then 1
then the external configuration will be loaded.

There is one more key that can be used for the airplane selection.
Currently only two aiplane are supported. The airplane to be loaded can
be one of ``"Cessna" or "F4UCorsair"``. The airplane can be defined with
``General/activeAirplane`` key.

The meaning for the each key are as given below.
------------------------------------------------

**General:** \| Key \| Default \| Details \| \|:—:|:—:|:—:\|
\|docversion“] \| CommonFunctions.GET_VERSION();\| \|priority”] \| 1 \|
if priority of json is higher json will override default setting\|
\|activeAirplane“] \|”Cessna152"; \| if priority of json is higher json
will override default setting\|

**Camera** \| Key \| Default \| Details \| \|:—:|:—:|:—:\|
\|cameraHeight“] \| 6\| Height of camera\| \|cameraDistance”] \| 12\|
Camera distance\| \|minHeaightFromGround"] \| 4;\| Min height of camera
from ground when aiplane lands\|

**Characteristics** \| Key \| Default \| Details \| \|:—:|:—:|:—:\|
\|maxMPH“] \| 150\| Max speed of the airplane\| \|rbLerpSpeed”] \| 0.1
\| Controls accelearation\| \|maxLiftPower“] \| 100\| Control takeoff
power of the airplane\| \|flapLiftPower”] \| 500\| Lift force by flap\|
\|dragFactor“] \| 0.01\| Drag by air pressure\| \|flapDragFactor”] \|
0.001\| Drag when flap is used\| \|pitchSpeed“] \| 5000\| Elevator
sufarce effectivness\| \|rollSpeed”] \| 4000\| Aleron surface
effectivness\| \|yawSpeed"] \| 5000\| Rudder lever effectivness\|

**Airplane Controller** \| Key \| Default \| Details \| \|:—:|:—:|:—:\|
\|airplaneWeight“] \| 900\| Mass of the Airplane in pounds\| \|control
surface\| \|smoothSpeed”] \| 4 \| Control surface speed\| \|maxAngle"]
\| 30 \| Max angle of the control surface\|

**Engine** \| Key \| Default \| Details \| \|:—:|:—:|:—:\| \|maxRPM“] \|
4500\| RPM of the engine force will be calculated by equarion
:math:`F_i | C_t\rho\omega^2_{max}D^4`\ \| \|shutOffSpeed”] \| 2 \|
shutdown rate when the engien is shutoff\| \|propellerSpan"] \| 1.6 \|
span of the propeller in meters\|

**Fuel** \| Key \| Default \| Details \| \|:—:|:—:|:—:\|
\|fuelCapacity“] \| 29 \| fuel capacity of the Airplane in galloons\|
\|fuelBurnRate”] \| 6.1 \| consumpltion rate Gallons/hr \|

**Ground Effect** \| Key \| Default \| Details \| \|:—:|:—:|:—:\|
\|groundDistance“] \| 3 \| Distance from ground when the ground effect
starts, generally its equal to wingspan\| \|liftForce”] \| 50 \| lift
force generated by the ground effect\| \|maxSpeed"] \| 15 \| max pseed
for ground effect\|

**Input** \| Key \| Default \| Details \| \|:—:|:—:|:—:\|
\|throttleSpeed"] \| 0.1\| Throttle Speed \|

**Wheels** \| Key \| Default \| Details \| \|:—:|:—:|:—:\|
\|brakePower“] \| 500 \| Brake Power\| \|steerAngle”] \| 20 \| steer
angle of the wheel\|

The default values in above tables are for airplane ``Cessna152``.
Properties for different airplane will be different and that defines
unique flying experience for each airplane.


