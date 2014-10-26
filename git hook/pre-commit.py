#!/usr/bin/env python

__author__ = "Safder"
__url__ = "http://drunkchecker.azurewebsites.net"  # API endpoint

import os
import json
import urllib2
import re
import sys


def get_global_git_username():
    return re.search("name = ([\w\s]*)\n", __git_config__).groups(0)[0]


def get_global_git_email():
    return re.search("email = (.*)", __git_config__).groups(0)[0]


def consume_request(req, request_data):
    request = urllib2.Request(__url__ + req)
    request.add_header('Content-Type', 'application/json')
    response = urllib2.urlopen(request, json.dumps(request_data), 60)  # Set the timeout to a minute

    return response.read()

__git_config__ = open(os.environ['USERPROFILE'] + '\.gitconfig').read()

data = {
    'email': get_global_git_email()
}

result = json.loads(consume_request('/ReadForUser', data))

print result

if result['drunkLevel'] >= 3:  # drunk or higher
    if not result['user']['OverrideEnabled']:  # without the override enabled
        sys.exit(1)  # not allowed
sys.exit(0)  # allowed