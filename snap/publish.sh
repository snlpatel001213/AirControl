#https://snapcraft.io/blog/publish-your-unity-games-in-the-snap-store
sudo snapcraft --destructive-mode
sudo snap install aircontrol_1.0.0_amd64.snap --dangerous
sudo snapcraft push aircontrol_1.0.0_amd64.snap

