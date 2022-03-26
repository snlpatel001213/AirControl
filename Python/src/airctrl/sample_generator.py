import random
import matplotlib.pyplot as plt
import numpy as np

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
        """
        This function returns a random pitch value between -1 and 1
        :return: A random pitch value.
        """
        return random.random()*(random.choices([-1,1])[0])
    
    def get_random_yaw(self):
        """
        This function returns a random value from a normal distribution with a mean of 0 and a standard
        deviation of 0.25
        :return: A random yaw value.
        """
        return random.normalvariate(0,0.25)
    
    def get_random_roll(self):
        """
        This function returns a random number between -1 and 1
        :return: A random number between -1 and 1
        """
        return random.normalvariate(0,0.25)
    
    def get_random_stickythrottle(self, distribution="uniform"):
        """
        Get a random number from a uniform distribution between 0 and 1
        
        :param distribution: uniform or beta, defaults to uniform (optional)
        :return: A random number between 0 and 1.
        """
        if(distribution=="uniform"):
            return random.random()
        if(distribution=="beta"):
            a, b = 1.0, 0.5
            return np.random.beta(a, b)
    
    def get_random_pitchangle(self):
        """
        This function returns a random number between -1 and 1
        :return: A random pitch angle.
        """
        return random.random()
    
    def get_random_bankangle(self):
        """
        This function returns a random number between -1 and 1
        :return: A random float between -1 and 1.
        """
        return random.random()
    
    def beta_dist(self):
        """
        plot beta distribution
        """
        a, b = 1.0, 0.5
        s = np.random.beta(a, b, 1000)
        
        # Create the bins and histogram
        count, bins, ignored = plt.hist(s, 20, density=True)
        
        # Plot the distribution curve
        plt.plot(bins, 1/(a * np.sqrt(2 * np.pi)) *
            np.exp( - (bins - a)**2 / (2 * b**2) ),       linewidth=3, color='y')
        plt.show()