============================================================================================================

-----------------------------------------What we have done--------------------------------------------------

============================================================================================================

Arduino 
	- Arduino has an MQ-3 sensor attached as well as an LED bar, button and buzzer.
	- Arduino connected to a c# console app which keeps a socket open with our public facing web service and
		accepts a request to make a reading and will pipe it back up.

Web Service
	- Public facing service available at : http://drunkchecker.azurewebsites.net/
	- Serves up JSON
	- Offers the following base functionality:
		- Create/Update/Get a user.
		- Return all readings for a user.
		- Read arduino alcohol sensor for user.
			- Option for notifying the user's supervisor if you blow over a certain value it will text them 
				to say you are drunk.
			- Option for sending a text to the user mobile number telling them to stop.
				- TODO : Hopefully will send abuse instead!!
			- Option for sending a text to the user's emergancy contact number.
	- Third party APIs
		- Ability to recieve a request for a donation to be made from a User. The charity is chosed randomally from
			those stored.
		- Ability to send an SMS message to a number (Used above for example)
		- Ability to recieve an SMS message (+447860033766)
			- "DONATE [EmailAddress]" will create a new donation which will allow you override one rejected service
			- "READ [EmailAddress]" will start a new read on the Arduino and will message you back the result.
			- TODO : Hopefully will accept an override for a user from their supervisor (To commit code for example)

Client Git Hook
	- Polls the web service for a reading for the user and if they blow over then it will stop them from 
		committing anything.
	- If they happen to get rejected for blowing over they have 2 options to be able to override the reading:
		- Donation via text to get their user unlocked for a commit.
		- Supervisor override via text will allow their next commit to go through.

Chrome Extention
	- Allows a user to "login" or create a new User
	- Can control the following sites
		- Online Banking sites
			- Stop you from clicking anything until you take a reading. (It will start a reading for you)
			- If over it will bring up an overlay telling you to go to bed.
		- Facebook/Twitter
			- Stop you from posting tweet/status until you take a reading. (It will start a reading for you)
			- If over it changes the status/tweet box into a youtube video of "nope.avi" and stops it.
	- If you have the override flag active (Via donation)
	- TODO : Add the "notifyIce" to the ReadForUser calls to notify their ICE mobile.


==========================================================================================================
=URL - http://drunkchecker.azurewebsites.net/

DrunkCheckResponse exposes success, value, user.

/Read 														Will return a DrunkCheckResponse object

/ReadForUser?id=[something]									Will return a DrunkCheckResponse object
/ReadForUser?email=[something]
	&notifySupervisor=[something]							Optional which will notify the users supervisor
	&textSelf=[something]									Optional which will text yourself abuse.
	&notifyIce=[something]									Optional whih will notify the emergacy contact

/Donate?id=[something]										Allows a donation
/Donate?email=[something]

/ClockWork/SendSMS?recipientNumber=[something]&message=[something]

/ClockWork/ReceiveSms 										This is the end point for clockwork service to hit.

/User/CreateUser?name=[something]&email=[something]			Will return a User object.

/User/GetUser?userId=[something]							Will return a User object.
/User/GetUser?email=[something]								Will return a User object.

/User/GetResultsForUser?userId=[something]					Will return a Json array of Reading objects

/User/UpdateUser?userId=[something]							Allow you to update the user. 3 optional params
							&email=[something]
							&mobile=[something]
							&supervisorId=[something]

============================================================================================================
Levels of drunk
    {
        Sober,
        Tipsy,
        GettingThere,
        Drunk,
        Fooked,
        JonDrunk
    }

============================================================================================================

+447860033766

============================================================================================================