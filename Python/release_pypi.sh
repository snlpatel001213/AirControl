rm -rf dist
cp  ../README.md .
cp ../LICENSE .
cp ../VERSION .
 ~/miniconda3/bin/pip install twine
 ~/miniconda3/bin/python setup.py sdist
twine upload dist/* --verbose