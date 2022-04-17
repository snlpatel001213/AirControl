import torch
import torch.nn as nn
import torch.nn.functional as F
import torchvision.models as models

class QNetwork(nn.Module):
    """Actor (Policy) Model."""

    def __init__(self, state_size, action_size, seed, fc1_units=384, fc2_units=192, fc3_units=96):
        """Initialize parameters and build model.
        Params
        ======
            state_size (int): Dimension of each state
            action_size (int): Dimension of each action
            seed (int): Random seed
            fc1_units (int): Number of nodes in first hidden layer
            fc2_units (int): Number of nodes in second hidden layer
        """
        super(QNetwork, self).__init__()
        self.seed = torch.manual_seed(seed)
        self.fc1 = nn.Linear(state_size, fc1_units)
        self.fc2 = nn.Linear(fc1_units, fc2_units)
        self.fc3 = nn.Linear(fc2_units, fc3_units)
        self.fc4_a = nn.Linear(fc3_units, action_size-1)
        self.fc4_b = nn.Linear(fc3_units, 1)
        self.drop = nn.Dropout(p=0.1)
        # resnet = models.resnet18(pretrained=True)
        # resnet(torch.randn(1,3,224,224)).shape
        

    def forward(self, state):
        """Build a network that maps state -> action values."""
        x = F.relu(self.fc1(state))
        x = self.drop(F.relu(self.fc2(x)))
        x = self.drop(F.relu(self.fc3(x)))
        fc4_a_out = self.fc4_a(x)
        fc4_b_out = self.fc4_b(x)
        tuple_of_activated_parts = (
            F.tanh(fc4_a_out),
            F.relu(fc4_b_out)
        )
        out = torch.cat(tuple_of_activated_parts, dim=1)
        return out

class LSTMQNetwork(nn.Module):
    """
    Actor (Policy) Model.
    
    usage:
    l = LSTMQNetwork()
    inp = torch.randn(b, 1, 384) # (b.timestep,seqlen)
    out, hstate = l(inp)  # out [b, action_space]
    """

    def __init__(self, state_size=384, action_size=4, hidden_size=128, fc1_units=64, seed=0):
        super(LSTMQNetwork, self).__init__()
        self.seed = torch.manual_seed(seed)
        self.rnn = torch.nn.LSTM(input_size=384, hidden_size=hidden_size, batch_first=True)
        self.fc1 = nn.Linear(hidden_size, fc1_units)
        self.fc2 = nn.Linear(fc1_units, action_size)
        self.drop = nn.Dropout(p=0.1)
    def forward(self,inp,hstates=None):
        if (hstates==None):
            rnn_out,hstates = self.rnn(inp)
        else:
            rnn_out,hstates = self.rnn(inp, hstates)
        batch, timestemp,hidden = rnn_out.shape
        rnn_out = rnn_out.reshape(batch,hidden)
        fc1_out = self.drop(F.relu(self.fc1(rnn_out)))
        fc2_out = self.fc2(fc1_out)
        first_slice = fc2_out[:,0:3]
        second_slice = fc2_out[:,3:]
        tuple_of_activated_parts = (
        F.tanh(first_slice),
        F.sigmoid(second_slice)
        )
        out = torch.cat(tuple_of_activated_parts, dim=1)
        return out, hstates
