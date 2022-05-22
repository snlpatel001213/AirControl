import subprocess
import glob
import os
import time
from sys import platform
import socket

from click import command

class Launch:
    def get_client_info(self):
        """
        It returns the IP address of the client
        :return: The IP address of the client.
        """
        return socket.gethostbyname(socket.gethostname())

    def launch_executable(self, file_name: str,  server_port=8053, client_ip=None, sleeptime=5) -> subprocess.Popen:
        """
        The function takes in the name of the executable file, the server port, the client ip and the sleep
        time. 
        
        It then checks if the executable file is present in the environment folder. If it is, it launches
        the executable file with the server port, client ip and client port as arguments. 
        
        It then sleeps for the specified time to allow the environment to load.
        
        :param file_name: The name of the executable file to launch
        :type file_name: str
        :param server_port: The port that the server will listen on, defaults to 8053 (optional)
        :param client_ip: The IP address of the client machine
        :param sleeptime: The time to wait for the environment to load, defaults to 5 (optional)
        :return: The process object is being returned.
        """
        launch_string = self.validate_environment_path(file_name)
        if launch_string is None:
            raise (
                f"Couldn't launch the {file_name} environment. Provided filename does not match any environments."
            )
        else:
            if(client_ip==None):
                client_ip = self.get_client_info()
            subprocess_args = launch_string
            # std_out_option = DEVNULL means the outputs will not be displayed on terminal.
            # std_out_option = None is default behavior: the outputs are displayed on terminal.
            print("Loading environment from {0} at port {1} client ip {2} client port {3}".format(subprocess_args,str(server_port),str(client_ip), str(server_port)))
            
            try:
                command_line = [subprocess_args, "--serverPort",  str(server_port), "--clientIP" , str(client_ip), "--clientPort", str(server_port)]
                process  = subprocess.Popen(
                    args = command_line,
                    # start_new_session=True means that signals to the parent python process
                    # (e.g. SIGINT from keyboard interrupt) will not be sent to the new process on POSIX platforms.
                    # This is generally good since we want the environment to have a chance to shutdown,
                    # but may be undesirable in come cases; if so, we'll add a command-line toggle.
                    # Note that on Windows, the CTRL_C signal will still be sent.
                    start_new_session=True
                )
                print("Sleeping for {0} seconds to allow environment load".format(sleeptime))
                time.sleep(sleeptime)
                return process
            except PermissionError as perm:
                # This is likely due to missing read or execute permissions on file.
                raise (
                    f"Error when trying to launch environment - make sure "
                    f"permissions are set correctly. For example "
                    f'"chmod -R 755 {launch_string}"'
                ) from perm

    def get_platform(self):
        """
        returns the platform of the operating system : linux, darwin or win32
        """
        return platform
    
    def validate_environment_path(self,env_path: str):
        """
        Strip out executable extensions of the env_path
        :param env_path: The path to the executable
        """
        env_path = (
            env_path.strip()
            .replace(".app", "")
            .replace(".exe", "")
            .replace(".x86_64", "")
            .replace(".x86", "")
        )
        true_filename = os.path.basename(os.path.normpath(env_path))

        if not (glob.glob(env_path) or glob.glob(env_path + ".*")):
            return None

        cwd = os.getcwd()
        launch_string = None
        true_filename = os.path.basename(os.path.normpath(env_path))
        if self.get_platform() == "linux" or self.get_platform() == "linux2":
            candidates = glob.glob(os.path.join(cwd, env_path) + ".x86_64")
            if len(candidates) == 0:
                candidates = glob.glob(os.path.join(cwd, env_path) + ".x86")
            if len(candidates) == 0:
                candidates = glob.glob(env_path + ".x86_64")
            if len(candidates) == 0:
                candidates = glob.glob(env_path + ".x86")
            if len(candidates) == 0:
                if os.path.isfile(env_path):
                    candidates = [env_path]
            if len(candidates) > 0:
                launch_string = candidates[0]

        elif self.get_platform() == "darwin":
            candidates = glob.glob(
                os.path.join(cwd, env_path + ".app", "Contents", "MacOS", true_filename)
            )
            if len(candidates) == 0:
                candidates = glob.glob(
                    os.path.join(env_path + ".app", "Contents", "MacOS", true_filename)
                )
            if len(candidates) == 0:
                candidates = glob.glob(
                    os.path.join(cwd, env_path + ".app", "Contents", "MacOS", "*")
                )
            if len(candidates) == 0:
                candidates = glob.glob(
                    os.path.join(env_path + ".app", "Contents", "MacOS", "*")
                )
            if len(candidates) > 0:
                launch_string = candidates[0]
        elif self.get_platform() == "win32":
            candidates = glob.glob(os.path.join(cwd, env_path + ".exe"))
            if len(candidates) == 0:
                candidates = glob.glob(env_path + ".exe")
            if len(candidates) == 0:
                # Look for e.g. 3DBall\UnityEnvironment.exe
                crash_handlers = set(
                    glob.glob(os.path.join(cwd, env_path, "UnityCrashHandler*.exe"))
                )
                candidates = [
                    c
                    for c in glob.glob(os.path.join(cwd, env_path, "*.exe"))
                    if c not in crash_handlers
                ]
            if len(candidates) > 0:
                launch_string = candidates[0]
        return launch_string