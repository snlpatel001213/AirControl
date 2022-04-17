import random
import traceback
from collections import deque, namedtuple
from airctrl import sample_generator

import numpy as np
import torch
import torch.nn.functional as F
import torch.optim as optim
import wandb
from model import QNetwork
import json
from torch.utils.data import Dataset, DataLoader
import threading
from tqdm import tqdm

sample = sample_generator.samples()


BUFFER_SIZE = int(1e7)  # replay buffer size
# BATCH_SIZE = 64         # minibatch size
# GAMMA = 0.99            # discount factor
# TAU = 1e-2              # for soft update of target parameters
# LR = 1e-2            # learning rate 
UPDATE_EVERY = 4        # how often to update the network

device = torch.device("cuda:0" if torch.cuda.is_available() else "cpu")
# device = torch.device("cpu")


class Agent():
    """Interacts with and learns from the environment."""

    def __init__(self, state_size, action_size, seed=0, batch_size = 4 ,gamma = 0.94 ,tau = 0.006 ,lr =  0.001 ):
        """Initialize an Agent object.
        
        Params
        ======
            state_size (int): dimension of each state
            action_size (int): dimension of each action
            seed (int): random seed
        """
        self.state_size = state_size
        self.action_size = action_size
        self.batch_size = batch_size
        self.gamma = gamma
        self.tau = tau
        self.lr = lr
        self.seed = random.seed(seed)

        # Q-Network
        self.qnetwork_local = QNetwork(state_size, action_size, seed).to(device)
        self.qnetwork_target = QNetwork(state_size, action_size, seed).to(device)
        self.optimizer = optim.Adam(self.qnetwork_local.parameters(), lr=self.lr)
        wandb.watch(self.qnetwork_target)
        wandb.watch(self.qnetwork_local)

        # Replay memory
        self.memory = ReplayBuffer(action_size, BUFFER_SIZE, self.batch_size, seed)
        # Initialize time step (for updating every UPDATE_EVERY steps)
        self.t_step = 0
    
    def step(self, state, action, reward, next_state, done):
        # Save experience in replay memory
        self.memory.add(state, action, reward, next_state, done)
        
        # Learn every UPDATE_EVERY time steps.
        self.t_step = (self.t_step + 1) % UPDATE_EVERY
        if self.t_step == 0:
            # If enough samples are available in memory, get random subset and learn
            if len(self.memory) > self.batch_size:
                wandb.log({"memory Size": len(self.memory)})
                experiences = self.memory.sample()
    #           print("Training with samples : ", len(self.memory))
                # self.learn(experiences, self.gamma)
                t1 = threading.Thread(target=self.learn, args=(experiences, self.gamma,))
                t1.start()
                t1.join()

    def act(self, state, eps=0.):
        """Returns actions for given state as per current policy.
        
        Params
        ======
            state (array_like): current state
            eps (float): epsilon, for epsilon-greedy action selection
        """
        state = torch.from_numpy(state).float().unsqueeze(0).to(device)
        # Epsilon-greedy action selection
        if random.random() > eps:
#             print("From Model")
            self.qnetwork_local.eval()
            with torch.no_grad():
                action_values = self.qnetwork_local(state)
            self.qnetwork_local.train()
            trigger_type = "M"
            return trigger_type, action_values.cpu().tolist()[0]
        else:
#             print("From Random")
            trigger_type = "S"
            return trigger_type, [sample.get_random_pitch(), sample.get_random_yaw(), sample.get_random_roll(), sample.get_random_stickythrottle()]

    def learn(self, experiences, gamma, training_flag = ""):
        """Update value parameters using given batch of experience tuples.
        Params
        ======
            experiences (Tuple[torch.Tensor]): tuple of (s, a, r, s', done) tuples 
            gamma (float): discount factor
        """
        states, actions, rewards, next_states, dones = experiences
        # print("state ",  states.shape)
        # print("actions ",  actions.shape)
        # print("rewards ",  rewards.shape)
        # print("next_states ",  next_states.shape)
        # print("dones ",  dones.shape)

        # Get max predicted Q values (for next states) from target model
        # Q_targets_next = self.qnetwork_target(next_states).detach()
        # Compute Q targets for current states 
        # Computing the expected reward for the next state.

        # Get expected Q values from local model
        # Q_expected = self.qnetwork_local(states)
        # Q_expected = self.qnetwork_local(states).gather(1, actions)
        # # Compute loss
        # loss = F.mse_loss(Q_expected, Q_targets)
        # print("Q_targets_next", Q_targets_next.shape)
        # print("Q_expected", Q_expected.shape)
        # print("Q_targets", Q_targets.shape)
        # print("loss",loss.shape)
        # print("dones",dones.shape)
        # print("rewards",rewards.shape)
        # Minimize the loss
        ####################Original#################
        Q_local_argmax = self.qnetwork_local(next_states).detach().max(1)[1].unsqueeze(1)
        Q_targets_next = self.qnetwork_target(next_states).gather(1, Q_local_argmax)
        Q_targets = rewards + (gamma * Q_targets_next * (1 - dones))
        Q_expected = self.qnetwork_local(states).gather(1, actions)
        loss = F.mse_loss(Q_expected, Q_targets)
        # Minimize the loss
        self.optimizer.zero_grad()
        loss.backward()
        self.optimizer.step()
        wandb.log({training_flag+"loss": loss})

        # ------------------- update target network ------------------- #
        self.soft_update(self.qnetwork_local, self.qnetwork_target, self.tau)      

    def soft_update(self, local_model, target_model, tau):
        """Soft update model parameters.
        θ_target = τ*θ_local + (1 - τ)*θ_target
        Params
        ======
            local_model (PyTorch model): weights will be copied from
            target_model (PyTorch model): weights will be copied to
            tau (float): interpolation parameter 
        """
        for target_param, local_param in zip(target_model.parameters(), local_model.parameters()):
            target_param.data.copy_(tau*local_param.data + (1.0-tau)*target_param.data)
    
    def data_loader(self):
        ""
        
        # for each_experience in tqdm(crux_lines):
           
        
    def pretrain(self,iterations=10000):
        self.crux_lines =  open('crux.json', 'r').readlines()
        for each_experience  in tqdm(self.crux_lines):
            try:
                each_experience = json.loads(each_experience)
                state = torch.tensor(each_experience["state"])
                action = torch.tensor(each_experience["action"])
                reward = each_experience["reward"]
                next_state = torch.tensor(each_experience["next_state"])
                done = each_experience["done"]
                done = 1.0 if(done) else 0.0
                self.memory.add(state,action,reward,next_state,done,write_to_file=False)
            except:
                pass
        print("########### Inserting to memory completed ###########")
        print("########### Memory Size: {0} ###########".format(len(self.memory)))
        # for i in range(0.epochs):
        for i in tqdm(range(0,iterations)):
            if len(self.memory) > self.batch_size:
                experiences = self.memory.sample()
    #           print("Training with samples : ", len(self.memory))
                self.learn(experiences, self.gamma,training_flag="pretraining_")
            else:
                print("Memory size is not enough")
                    
class CustomTextDataset(Dataset):
    def __init__(self):
        self.crux_lines =  open('crux.json', 'r').readlines()
    def __len__(self):
            return len(self.crux_lines)
    def __getitem__(self, idx):
        each_experience = self.crux_lines[idx]
        each_experience = json.loads(each_experience)
        state = torch.tensor(each_experience["state"])
        action = torch.tensor(each_experience["action"])
        reward = each_experience["reward"]
        next_state = torch.tensor(each_experience["next_state"])
        done = each_experience["done"]
        done = 1.0 if(done) else 0.0
        experiences = [state,action,reward,next_state,done]
        return experiences

class ReplayBuffer:
    """Fixed-size buffer to store experience tuples."""

    def __init__(self, action_size, buffer_size, batch_size, seed):
        """Initialize a ReplayBuffer object.
        Params
        ======
            action_size (int): dimension of each action
            buffer_size (int): maximum size of buffer
            batch_size (int): size of each training batch
            seed (int): random seed
        """
        self.action_size = action_size
        self.memory = deque(maxlen=buffer_size)  
        self.batch_size = batch_size
        self.experience = namedtuple("Experience", field_names=["state", "action", "reward", "next_state", "done"])
        self.seed = random.seed(seed)
        self.crux_file =  open('crux.json', 'a')

    def add(self, state, action, reward, next_state, done,write_to_file=True):
        """Add a new experience to memory."""
        e = self.experience(state, action, reward, next_state, done)
        if write_to_file:
            crux = {"state":state.tolist(), "action":action, "reward":reward, "next_state":next_state.tolist(), "done":done}
            self.crux_file.write(json.dumps(crux))
            self.crux_file.write('\n')
        self.memory.append(e)
    
    def sample(self):
        """Randomly sample a batch of experiences from memory."""
        experiences = random.sample(self.memory, k=self.batch_size)

        states = torch.from_numpy(np.vstack([e.state for e in experiences if e is not None])).float().to(device)
        actions = torch.from_numpy(np.vstack([e.action for e in experiences if e is not None])).long().to(device)
        rewards = torch.from_numpy(np.vstack([e.reward for e in experiences if e is not None])).float().to(device)
        next_states = torch.from_numpy(np.vstack([e.next_state for e in experiences if e is not None])).float().to(device)
        dones = torch.from_numpy(np.vstack([e.done for e in experiences if e is not None]).astype(np.uint8)).float().to(device)
  
        return (states, actions, rewards, next_states, dones)

    def __len__(self):
        """Return the current size of internal memory."""
        return len(self.memory)
