rm -rf dist
cp  ../README.md .
cp ../LICENSE .
cp ../VERSION .
pip install twine
~/miniconda3/bin/python3 setup.py sdist
twine upload dist/* --verbose