from AirControl.communicator import Communicator
from AirControl import schemaDef
import json

connection = Communicator()

uiaudio_schema = schemaDef.set_uiaudio(EnableAudio=False)
connection.send_data(uiaudio_schema)
print(connection.receive_data())

camera_schema = schemaDef.set_camera(ActiveCamera=1, IsCapture=True)
connection.send_data(camera_schema)
print(connection.receive_data())

control_schema = schemaDef.set_control(StickyThrottle=0.5)
connection.send_data(control_schema)
print(connection.receive_data())