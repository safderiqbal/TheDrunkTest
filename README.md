TheDrunkTest
============

#TODO
* Get OneDiary API key
* Get Clockwork SMS API key
* Get Barclays pingit? (don't know if it's necessary for the hack)

Git hook
	To use the git hook



==========================================================================================================
=URL - http://drunkchecker.azurewebsites.net/

DrunkCheckResponse exposes success, value, user.

/Read 														Will return a DrunkCheckResponse object

/ReadForUser?id=[something]									Will return a DrunkCheckResponse object
/ReadForUser?email=[something]

/User/CreateUser?name=[something]&email=[something]			Will return a User object.

/User/GetUser?userId=[something]							Will return a User object.
/User/GetUser?email=[something]								Will return a User object.

/User/GetResultsForUser?userId=[something]					Will return a Json array of Reading objects

/User/UpdateUser?userId=[something]							Allow you to update the user. 3 optional params
							&email=[something]
							&mobile=[something]
							&supervisorId=[something]

============================================================================================================

public enum DrunkLevel
    {
        Sober,
        Tipsy,
        GettingThere,
        Drunk,
        Fooked,
        JonDrunk
    }

============================================================================================================