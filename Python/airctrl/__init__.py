from gym.envs.registration import register

register(
    id='AirControl150',
    entry_point='airctrl.envs:AirControl150',
)