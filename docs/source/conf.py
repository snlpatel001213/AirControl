# Configuration file for the Sphinx documentation builder.
#
# This file only contains a selection of the most common options. For a full
# list see the documentation:
# https://www.sphinx-doc.org/en/master/usage/configuration.html

# -- Path setup --------------------------------------------------------------

# If extensions (or modules to document with autodoc) are in another directory,
# add these directories to sys.path here. If the directory is relative to the
# documentation root, use os.path.abspath to make it absolute, like shown here.
#
import os
import sys

sys.path.insert(0, os.path.abspath('.'))


# -- Project information -----------------------------------------------------

project = 'Aircontrol'
copyright = '2022, Sunil Patel'
author = 'Sunil Patel'

# The full version, including alpha/beta/rc tags
release = '0.0.5'


# -- General configuration ---------------------------------------------------

# Add any Sphinx extension module names here, as strings. They can be
# extensions coming with Sphinx (named 'sphinx.ext.*') or your custom
# ones.
extensions = [
    'sphinx.ext.napoleon',
    'sphinx.ext.autodoc',
    'sphinx.ext.viewcode',
    'sphinx.ext.coverage',
]

# Add any paths that contain templates here, relative to this directory.
templates_path = ['_templates']

# List of patterns, relative to source directory, that match files and
# directories to ignore when looking for source files.
# This pattern also affects html_static_path and html_extra_path.
exclude_patterns = []


# -- Options for HTML output -------------------------------------------------

# The theme to use for HTML and HTML Help pages.  See the documentation for
# a list of builtin themes.
#
html_theme = 'press'

# Add any paths that contain custom static files (such as style sheets) here,
# relative to this directory. They are copied after the builtin static files,
# so a file named "default.css" will overwrite the builtin "default.css".
html_static_path = ['_static']

# If defined shows an image instead of project name on page top-left (link to index page).
html_logo = '../images/logo/logo_original_large-128_x_128.png'

# If provided, creates external links (e.g. Github) in the top right corner:

html_theme_options = {
  "external_links": [
      ("Github", "https://github.com/snlpatel001213/AirControl/" ),
      ("Project Dashboard", "https://github.com/snlpatel001213/AirControl/projects/1")
  ]
}
