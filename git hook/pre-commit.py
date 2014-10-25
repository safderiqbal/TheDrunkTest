#!/usr/bin/env python

__author__ = "Safder"

import sys
import os
import json
import urllib2
import re


def find_global_git_settings():
    git_config = open(os.environ['USERPROFILE'] + '\.gitconfig').read()
    git_username = re.search("name = ([\w\s]*)\n", git_config).groups(0)[0]

    return git_username


def consume_request(url):
    request = urllib2.urlopen(url, None, 60)  # Set the timeout to a minute

    return request.read()

#print find_global_git_settings()
#
#print consume_request('https://developer.yahoo.com/')
#
#test_json = json.loads('["foo", {"bar":["baz", null, 1.0, 2]}]')
#print test_json
#print test_json[0]

#Exit with failure
sys.exit(1)

# contact the API with the commit user's name

# if too drunk error