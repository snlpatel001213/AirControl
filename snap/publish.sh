#!/bin/bash

###### referecne ####
# https://snapcraft.io/blog/publish-your-unity-games-in-the-snap-store

VERSION=$(<../VERSION)
#####################
sudo rm -rf parts
sudo rm -rf prime
sudo rm -rf stage
# remove unity build files
sudo rm -rf  snap/local/v*
sudo rm -rf  snap/local/*.debug
sudo rm -rf  snap/local/*.so
# remove snap build files
rm -rf *.snap
# copy build from the unity linux build
cp -r ../Build/"$VERSION"/Linux/*.* snap/local/
# build the snap
sudo snapcraft --destructive-mode
# sudo snap install aircontrol_"$VERSION"_amd64.snap --dangerous
# sudo snapcraft push aircontrol_"$VERSION"_amd64.snap
