#!/bin/bash  

update_client_example_doc{
    for i in client_examples/*.ipynb; do
    echo "######### Processing - $i ##############"; 
    filename=$(basename -- "$i")
    extension="${filename##*.}"
    filename="${filename%.*}"
    echo "$basename , $extension $filename"
    ~/miniconda3/bin/python -m nbconvert  --to rst   $i 
    htmlfile=client_examples/$filename.rst
    rstfile=../docs/source/$filename.rst
    echo "$htmlfile --> $rstfile"
    mv "$htmlfile" "$rstfile"
    cp -r client_examples/"$filename"_files ../docs/source/
    rm -rf client_examples/"$filename"_files 
    # pandoc -f html -t rst "$htmlfile" -o "$rstfile"
    done
}
# client documentation generation
# ipython to ipynb to RST
# update_client_example_doc()
# Upload to Pypi
rm -rf dist
cp  ../README.md .
cp ../LICENSE .
cp ../VERSION .
 ~/miniconda3/bin/pip install twine
 ~/miniconda3/bin/python setup.py sdist
 ~/miniconda3/bin/twine upload dist/* --verbose