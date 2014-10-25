#!/usr/bin/env python

__author__ = "Safder"
__url__ = "http://drunkchecker.azurewebsites.net/"  # API endpoint

import os
import json
import urllib2
import re
import sys
import random


def get_global_git_username():
    return re.search("name = ([\w\s]*)\n", __git_config__).groups(0)[0]


def get_global_git_email():
    return re.search("email = (.*)", __git_config__).groups(0)[0]


def consume_request(request, request_data):
    request = urllib2.Request(__url__ + request)
    request.add_header('Content-Type', 'application/json')
    response = urllib2.urlopen(request, json.dumps(request_data), 60)  # Set the timeout to a minute

    return response.read()

__git_config__ = open(os.environ['USERPROFILE'] + '\.gitconfig').read()

data = {
    'username': get_global_git_username()
}

result = json.loads(consume_request('/ReadForUser?', data))


## Temporary stuff beyond here, before the API is fleshed out
randomValue = random.random() * 400

if result['value'] > randomValue:
    print 'reading was greater'
    # call back to API to send for supervisor request
    sys.exit(1)
else:
    print 'random was greater'
    sys.exit(0)