import gym
from gym import error, spaces, utils
from gym.utils import seeding
import numpy as np
from torch import dtype
from airctrl.src import environment 
from airctrl.src import sample_generator
from airctrl.src.utils import unity
import os

# same as BasicEnv, with one difference: the reward for each action is a normal variable
# purpose is to see if we can use libraries

class AirControl150(gym.Env):
    metadata = {'render.modes': ['human']}
    def __init__(self,action_space=4, observation_space=385, server_port=8899, MAX_EULAR = 360, MAX_ENV_SIZE = 10000, MAX_REWARD_SIZE=1000, MAX_LIDAR_RANGE = 500):
        """_summary_

        Args:
            action_space (int, optional): _description_. Defaults to 4.
            observation_space (int, optional): _description_. Defaults to 385.
            server_port (int, optional): _description_. Defaults to 8899.
            MAX_EULAR (int, optional): _description_. Defaults to 360.
            MAX_ENV_SIZE (int, optional): _description_. Defaults to 10000.
            MAX_REWARD_SIZE (int, optional): _description_. Defaults to 1000.
            MAX_LIDAR_RANGE (int, optional): _description_. Defaults to 500.
        """
        self.MAX_EULAR = MAX_EULAR # boundary for eular
        self.MAX_ENV_SIZE = MAX_ENV_SIZE
        self.MAX_REWARD_SIZE=MAX_REWARD_SIZE
        self.MAX_LIDAR_RANGE = MAX_LIDAR_RANGE
        self.action_space = spaces.Box(low=-1.0, high=1.0, shape=(action_space,), dtype=np.float32)
        self.observation_space = spaces.Box(low=-1000.0, high=1000.0, shape=(observation_space,), dtype=np.float32)
        self.L = unity.Launch()
        self.process = self.L.launch_executable("/home/supatel/Games/AirControl_2021/Build/FORSIMULATION/Linux/vFORSIMULATION-AirControl.x86_64", server_port=server_port)
        self.x =  environment.Trigger()
        self.x.get_connected(server_port)

    def output_to_features(self, state):
        """_summary_

        Args:
            state (_type_): _description_

        Returns:
            _type_: _description_
        """
        MSL = state['MSL']/self.MAX_ENV_SIZE
        Latitude = state['Latitude']
        Longitude = state['Longitude']
        normalizedRPM =  state['CurrentRPM']/state['MaxRPM']
        normalizedPower =  state['CurrentPower']/state['MaxPower']
        normalizedSpeed = state['CurrentSpeed']/150 # Normlizing by max speed
        pitchAngle = state['PitchAngle']
        bankAngle = state['BankAngle']
        ifCollision = state['IfCollision']
        collisionObject = state['CollisionObject']
        Reward = state["Reward"] # Normalizing rewards
        IsGrounded = 1.0 if(state["IsGrounded"]) else 0.0
        IsFlying = 1.0 if(state["IsFlying"]) else 0.0
        IsTaxiing = 1.0 if(state["IsTaxiing"]) else 0.0
        PosXAbs = (state["PosXAbs"])/self.MAX_ENV_SIZE
        PosYAbs = (state["PosYAbs"])/self.MAX_ENV_SIZE
        PosZAbs = (state["PosZAbs"])/self.MAX_ENV_SIZE
        PosXRel = (state["PosXRel"])/self.MAX_ENV_SIZE
        PosYRel = (state["PosYRel"])/self.MAX_ENV_SIZE
        PosZRel = (state["PosZRel"])/self.MAX_ENV_SIZE
        RotXAbs = (state["RotXAbs"])/self.MAX_EULAR
        RotYAbs = (state["RotYAbs"])/self.MAX_EULAR
        RotZAbs = (state["RotZAbs"])/self.MAX_EULAR
        RotXRel = (state["RotXRel"])/self.MAX_EULAR
        RotYRel = (state["RotYRel"])/self.MAX_EULAR
        RotZRel = (state["RotZRel"])/self.MAX_EULAR
        LidarPointCloud = 1.0-np.asarray(state['LidarPointCloud'])/self.MAX_LIDAR_RANGE

        feature_vector = [MSL, Latitude, Longitude, normalizedRPM, normalizedPower, normalizedSpeed, pitchAngle, \
                        bankAngle, IsGrounded, IsFlying, IsTaxiing, ifCollision, \
                        PosXAbs, PosYAbs, PosZAbs, PosXRel, PosYRel, PosZRel,RotXAbs,RotYAbs,RotZAbs,RotXRel,RotYRel,RotZRel, ifCollision] + LidarPointCloud.tolist()

        return np.asarray(feature_vector), Reward, ifCollision, collisionObject

    def step(self, action):
        """_summary_

        Args:
            action (_type_): _description_

        Returns:
            _type_: _description_
        """
        pitch =  action[0]
        yaw = action[1]
        roll= action[2]
        stickyThrottle=action[3]
        state = self.x.step(Pitch=pitch, Yaw=yaw, Roll=roll, StickyThrottle=stickyThrottle) 
        state, reward, done, collisionObject = self.output_to_features(state)
        info = {"collison Object": collisionObject}
        return np.array(state,dtype="float32"), reward, done, info

    def reset(self, seed=777, return_info=False, options=None):
        """_summary_

        Args:
            seed (int, optional): _description_. Defaults to 777.
            return_info (bool, optional): _description_. Defaults to False.
            options (_type_, optional): _description_. Defaults to None.

        Returns:
            _type_: _description_
        """
        super().reset(seed=seed)
        state = self.x.reset(IsOutput=True)
        self.x.set_audio(IsActive=True, EnableAudio=False)
        self.x.set_ui(ShowUIElements=True, IsActive= True)
        self.x.set_camera(ActiveCamera=1, IsActive=True, IsCapture=False, CaptureCamera=1, CaptureType=0,CaptureHeight=540, CaptureWidth=960)
        self.x.set_lidar(IsActive=False)
        state, _, _, info = self.output_to_features(state)
        state = np.array(state,dtype="float32")
        return (state, info) if return_info else info
  
    def render(self, server_port=8899):
        """_summary_

        Args:
            server_port (int, optional): _description_. Defaults to 8899.
        """
        if(self.process.poll() == None):
            print("Already rendered at pid : ", self.process.pid)
        else:
            self.process.kill()
            self.__init__()

    def close(self):
        """_summary_
        """
        self.process.kill()
