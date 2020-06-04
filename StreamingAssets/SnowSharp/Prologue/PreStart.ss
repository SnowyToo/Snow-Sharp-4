//Set standard values
$LoadTheme(Default)
$ReqChar(false)
$TypeSpeed(d)

//Text goes here
Because the prologue quest is quite expositional, and without much player input, I'm giving you the ability to skip it. \n
You can skip straight to The Mountain to the North-East, with or without a short briefing, or you can play it.
$SetKey(b)
* Skip with TL;DR / Skip without TL;DR / Play the prologue.
{
	[skipYes=1]
}
{
	[skipNo=1]
}
{
	[play=1]
}
Are you absolutely sure? \n
|play=1, else|
{
	Once you start the prologue, you cannot quit out of it anymore.
}
{
	You cannot replay the opening as of yet without restarting the game entirely.
}
* Yes, I'm sure / No, I want to reconsider.
{
	As you wish.
}
{
	$Goto(b)
}
|skipNo=1, skipYes=1, play=1|
{
	$Load(POI, Mountain)
}
{
	//Briefing
}
{
	$Load(Prologue, Start)
}

//Return to menu or other file
$Load(Menus, MainMenu)