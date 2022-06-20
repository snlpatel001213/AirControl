from gym.envs.registration import register

register(
    id='GymAirctrl-v0',
    entry_point='airctrl.envs:Env',
)