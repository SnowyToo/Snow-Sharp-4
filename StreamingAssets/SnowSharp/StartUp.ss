//Theme and Variables
$SetTheme(CenterText)
$TypeRate(d)
$ReqChar(false)

//"Logo"
This game was brought to you by \n
\n
\n
\n
SNOW GAMES DEVELOPMENT \n
\n
\n
\n
\n
$Wait(0.3)
\n
\n
... \n
$Wait(0.2)
(And powered by Snow# 4.)


Checking game version... \n
$SetKey(versionZero)
... \n
$Wait(0.5)
|version=0, else|
{
	$GoTo(versionZero)
}
{
	Game version verified. \n
}
$TypeRate(d)
\n

//Appropriate game version
|version > buildV, version < buildV, else|
{
	IMPORTANT NOTE! \n
	You are still running an outdated version of the game. You might want to consider updating. \n
	Updating is not done automatically. Choosing to update will open the GameJolt Download page.
	* Update / Remind me later
	{
		[openGameJolt=1]
		Please close down the game while updating.
	}
	{
		As you wish. \n
		The game will proceed to the main menu.
	}
}
{
	If you can see this, I have forgotten to update the version number on the server. \n 
	Please let me know so you won't be bothered by this message anymore. \n
	The game will proceed to the main menu.
}
{
	|hasStarted=0, else|
	{
		You are up-to-date! \n
		The game will proceed to the main menu.
	}
	{
		You are up-to-date!
	}
}

$Load(Menus, MainMenu)