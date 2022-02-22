rm -rf dist
cp  ../README.md .
cp ../LICENSE .
cp ../VERSION .
pip install twine
python setup.py sdist
twine upload dist/* --verbose --skip-existing 