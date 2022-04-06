#https://snapcraft.io/blog/publish-your-unity-games-in-the-snap-store
sudo rm -rf parts
sudo rm -rf prime
sudo rm -rf stage
sudo snapcraft --destructive-mode
sudo snap install aircontrol_1.2.0_amd64.snap --dangerous
sudo snapcraft push aircontrol_1.2.0_amd64.snap
