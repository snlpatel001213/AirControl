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
    def get_sample(self, distribution="uniform", rng=None):
        """
        General function to return random values for pitch, yaw, roll and, stickyThrottle
        :param distribution: uniform or beta, defaults to uniform (optional)
        :return: Numpy array with all random values.
        """
        pitch =  self.get_random_pitch(distribution=distribution,rng=rng)
        yaw = self.get_random_yaw(rng=rng)
        roll= self.get_random_roll(rng=rng)
        stickyThrottle= self.get_random_stickythrottle(distribution=distribution,rng=rng)
        return np.array([pitch, yaw,roll,stickyThrottle])

    def get_random_pitch(self, distribution="uniform",rng=None):
        """
        This function returns a random pitch value between -1 and 1
        :return: A random pitch value.
        """
        if rng !=  None:
            if(distribution=="uniform"):
                return rng.random()*(random.choices([-1,1])[0])
            if(distribution=="beta"):
                a, b = 10, 0.9 # emsure shift toward -1 side,  that forces the airplane up
                return rng.beta(a, b)*-1
        else:
            if(distribution=="uniform"):
                return random.random()*(random.choices([-1,1])[0])
            if(distribution=="beta"):
                a, b = 10, 0.9 # emsure shift toward -1 side, that forces the airplane up
                return np.random.beta(a, b)*-1
    
    def get_random_yaw(self, rng):
        """
        This function returns a random value from a normal distribution with a mean of 0 and a standard
        deviation of 0.25
        :return: A random yaw value.
        """
        if rng !=  None:
             return rng.normal(0,0.1)
        return np.random.normal(0,0.1)
    
    def get_random_roll(self, rng):
        """
        This function returns a random number between -1 and 1
        :return: A random number between -1 and 1
        """
        if rng !=  None:
             return rng.normal(0,0.1)
        return np.random.normal(0,0.1)
    
    def get_random_stickythrottle(self, distribution="uniform", rng=None):
        """
        Get a random number from a uniform distribution between 0 and 1
        
        :param distribution: uniform or beta, defaults to uniform (optional)
        :return: A random number between 0 and 1.
        """
        if rng !=  None:
            if(distribution=="uniform"):
                return rng.random()
            if(distribution=="beta"):
                a, b = 1.0, 0.5
                return rng.beta(a, b)
        else:
            if(distribution=="uniform"):
                return random.random()
            if(distribution=="beta"):
                a, b = 1.0, 0.5
                return np.random.beta(a, b)
    
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