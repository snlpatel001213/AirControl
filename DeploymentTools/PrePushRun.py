import re
import nbformat
from nbconvert import PythonExporter
import glob, os
import shutil
from pathlib import Path
from pip import main
cwd = os.getcwd()
skipped = 0

def convertNotebook(nb, destfn):
    os.system("jupyter nbconvert --to script '{0}' --output-dir {1}".format(nb, destfn))

def run_pipreqs():
    print("pipreqs {0}  --savepath {1} --force ".format(os.path.join(cwd), os.path.join(cwd,"Python","requirements.txt")))
    os.system("pipreqs {0}  --savepath {1} --force  ".format(os.path.join(cwd), os.path.join(cwd,"Python","requirements.txt")))
    # sphinx_rtd_theme==0.4.3
    # sphinx-press-theme
    requiements = open(os.path.join(cwd,"Python","requirements.txt"),"a")
    for i in ("sphinx_rtd_theme==0.4.3", "sphinx-press-theme", "pipreqs==0.4.11"):
        requiements.write(i+"\n")

def generate_requirements_file():
    # remove existing dir
    temp_dir = os.path.join(cwd,"DeploymentTools","temp")
    dirpath = os.path.join(temp_dir)
    if os.path.exists(temp_dir) and os.path.isdir(temp_dir):
        shutil.rmtree(temp_dir)
    os.makedirs(temp_dir)
    #search for ipynb files
    for path in Path(os.path.join(cwd)).rglob('*.ipynb'):
        try:
            print("\t",path.name)
            convertNotebook(path.absolute(), temp_dir)
        except:
            skipped =+1
    run_pipreqs()
    # shutil.rmtree(temp_dir)

def generate_doxygen_documentation():
    os.chdir(os.path.join(cwd,"docs"))
    os.system("{0} {1}".format("doxygen",os.path.join(cwd,"docs","doxygen.conf")))

if __name__ == '__main__':
#   generate_requirements_file()
  generate_doxygen_documentation()
  print ("$$$$$$$$$$$$$$$$$$$$$$$$$$$$")
  print("File Skipped due to error : ", skipped)
  print("#############################")