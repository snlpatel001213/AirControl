# Console scripts

import sys
from colorama import Fore, Back, Style
from .__init__ import __version__  # all imports require a dot when building package: '.__init__', etc.


# def main_with_argparse():
#     parser = argparse.ArgumentParser()
#     parser.add_argument('--login', nargs='?', const=True, default=False, help='ultralytics login API_KEY')
#     parser.add_argument('--hello', action='store_true', help='debug command')
#     opt = parser.parse_args()
#
#     if opt.login:
#         key = opt.login if isinstance(opt.login, str) else get_api_key()  # specified or most recent path
#         print(f'{prefix}Logging in with API key {key}')
#
#         # Login user with API key
#         # TODO: login operations here
#
#     elif opt.hello:  # alternate command
#         print(f'{prefix}Hello user!')


def main():
    if len(sys.argv) == 1 or sys.argv[1] == 'version':  # usage: $ aircontrol version
        print(Fore.Blue + 'Version {__version__}')
        print(Fore.Blue + 'For more info see https://github.com/snlpatel001213/AirControl/')
        print(Style.RESET_ALL)
    else:
        print(Fore.Blue + 'Arguments not recognized')
        print(Style.RESET_ALL)
        exit()


if __name__ == '__main__':
    main()
