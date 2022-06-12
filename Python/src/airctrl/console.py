# Console scripts

import sys
from colorama import Fore, Back, Style
from .__init__ import __version__  # all imports require a dot when building package: '.__init__', etc.

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
