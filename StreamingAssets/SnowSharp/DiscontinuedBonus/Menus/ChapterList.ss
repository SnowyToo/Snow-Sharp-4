//If you are reading this, congrats! You found a file that was cut from the game!
//I decided to leave it in because SnowSharp files are laughably small. I suppose it rewards players looking into how it works.

$SetTheme(ChapterSelect)
$TypeRate(d)
$ReqChar(false)

$SetKey(b)
Which narrative would you like to engulf yourself in?
* The Abyssal March / More coming soon / Return to main menu
{
	$SetKey(c)
	Which chapter will you play?
	* Chapter I \n "Fumes on the Horizon" / Chapter II \n "The Abyss Stares Back"(|Na1Ch2=1|) / Chapter II \n "Terrors never seen before"(|Na1Ch2=2|) / Chapter III \n "Claws from below"(|Na1Ch3=1|) / Chapter III \n "Declaration of Hostility"(|Na1Ch3=2|) / Chapter III \n "Unbroken Grounds"(|Na1Ch3=3|) / Chapter III \n "Smoke on the Water"(|Na1Ch3=4|) / Chapter IV \n "Abyssal Dimplomacy"(|Na1Ch4=1|) / Chapter IV \n "The Battle of The Abyss"(|Na1Ch4=2|) / Chapter IV \n "Dar'tox Alk-Har"(|Na1Ch4=3|) / Chapter IV \n "The Battle of Aqcaqorgalmurm"(|Na1Ch4=4|) / Chapter IV \n "Demonic Decimation"(|Na1Ch4=5|) / Chapter IV \n "War on Two Fronts"(|Na1Ch4=6|) / Back
	{
		$Load(Nar01/Ch01, CH01PreStart)
	}
	{
		$Load(Nar01/Ch02a, Ch02PreStart)
	}
	{
		$Load(Nar01/Ch02b, Ch02PreStart)
	}
	{
		I'm sorry, player. \n
		I'm afraid I can't let you do that (yet).
	}
	{
		I'm sorry, player. \n
		I'm afraid I can't let you do that (yet).
	}
	{
		I'm sorry, player. \n
		I'm afraid I can't let you do that (yet).
	}
	{
		I'm sorry, player. \n
		I'm afraid I can't let you do that (yet).
	}
	{
		I'm sorry, player. \n
		I'm afraid I can't let you do that (yet).
	}
	{
		I'm sorry, player. \n
		I'm afraid I can't let you do that (yet).
	}
	{
		I'm sorry, player. \n
		I'm afraid I can't let you do that (yet).
	}
	{
		I'm sorry, player. \n
		I'm afraid I can't let you do that (yet).
	}
	{
		I'm sorry, player. \n
		I'm afraid I can't let you do that (yet).
	}
	{
		I'm sorry, player. \n
		I'm afraid I can't let you do that (yet).
	}
	{
		$Goto(b)
	}
	$Goto(c)
}
{
	I'm sorry, player. \n
	I'm afraid I can't let you do that (yet).
}
{
	$Load(Menus, MainMenu)
}
$Goto(b)