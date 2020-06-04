//Set standard values
$LoadTheme(Default)
$ReqChar(true)
$TypeSpeed(d)
|metFeldir=0, else|
{
	$SetCast(Librarian, You)
}
{
	$SetCast(Feldir, You)
}


//Text goes here


//Return to menu or other file
$Load(Menus/Library, _LibLoop)