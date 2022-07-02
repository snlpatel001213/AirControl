from gym.envs.registration import register

register(
    id='gym-v150',
    entry_point='airctrl.envs:Env',
)