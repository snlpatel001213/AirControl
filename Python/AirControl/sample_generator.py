import random

# ## Limits
# | Control          | type  | Limit                                   |
# |------------------|-------|-----------------------------------------|
# | Pitch            | float | Between -1.0 and 1.0                    |
# | Yaw              | float | Between -1.0 and 1.0                    |
# | Roll             | float | Between -1.0 and 1.0                    |
# | Sticky Throttle  | float | Between 0.0 and 1.0                     |
# | Brake            | int   | 0 : Dis-engaged, 1 : Engaged            |
# | Flaps            | int   | 0 : None, 1 : 15 Degree, 2 : 30 Degree  |
# | bankAngleRadian  | float |Between 0.0 and 1.0                      |
# | pitchAngleRadian | float |Between 0.0 and 1.0                      |
# | Flaps            | int   | 0 : None, 1 : 15 Degree, 2 : 30 Degree  |
# | Brake            | int   | 0 : Dis-engaged, 1 : Engaged            |
# | Flaps            | int   | 0 : None, 1 : 15 Degree, 2 : 30 Degree  |
class samples:
    def get_random_pitch(self):
        return random.uniform(-1,1)
    def get_random_yaw(self):
        return random.uniform(-1,1)
    def get_random_roll(self):
        return random.uniform(-1,1)
    def get_random_stickythrottle(self):
        return random.uniform(0,1)
    def get_random_pitchangle(self):
        return random.uniform(0,1)
    def get_random_bankangle(self):
        return random.uniform(0,1)