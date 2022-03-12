#!/usr/bin/env python3

#####################################\
# cd Python
# python setup.py sdist
# twine upload dist/* --verbose
# pip install twine
######################################

import os

from setuptools import find_packages, setup


# load the README file and use it as the long_description for PyPI
with open("README.md", "r") as f:
    readme = f.read()


# package configuration - for reference see:
# https://setuptools.readthedocs.io/en/latest/setuptools.html#id9
setup(
    name="airctrl",
    description="AirControl is an Open Source, Modular, Cross-Platform, and Extensible Flight Simulator For Deep Learning Research.",
    long_description=readme,
    long_description_content_type="text/markdown",
    version=open("VERSION", "r").read().strip(),
    author="Sunil Patel",
    author_email="snlpatel001213@hotmail.com",
    packages=find_packages('airctrl'),
    package_dir={'': 'airctrl'},
    url="https://aircontrol.readthedocs.io",
    python_requires=">=3.7.*",
    install_requires=["numpy", "requests"],
    license="MIT",
    include_package_data=True,
    zip_safe=True,
    entry_points={"console_scripts": ["py-package-template=py_pkg.entry_points:main"]},
    classifiers=[
        "License :: OSI Approved :: MIT License",
        "Programming Language :: Python :: 3",
        "Programming Language :: Python :: 3.6",
        "Programming Language :: Python :: 3.5",
        "Programming Language :: Python :: 3.4",
        "Environment :: Console",
        "Intended Audience :: Science/Research",
        "License :: OSI Approved :: GNU General Public License v3 or later (GPLv3+)",
        "Operating System :: OS Independent",
        "Topic :: Software Development :: Libraries",
        "Topic :: Software Development :: Libraries :: Python Modules",
        "Topic :: Scientific/Engineering :: Artificial Intelligence"
    ],
    keywords="Airplane Simulation, Unity, C#, Python",
    data_files=[
        ('version',['VERSION']),
    ]
)
