$SetSpeed(5)
$ReqChar(false)
$LoadTheme(Menu)
$RAD(0.5, else)
{
	Welcome to TAILS OF THE FORGOTTEN LANDS. \n
}
{
	Welcome to TALES OF THE FORGOTTEN LANDS. \n
}
$RAD(0.5, else)
{
	This is the rudimentary main menu. \n
	... \n
	Huzzah!
}
{
	This is the rudimentary main menu.
}
$SetSpeed(d)
* Enter The Lands / Options / Patch notes / Quit Game
{
	$Load(Prologue, Prestart)
}
{
	$Load(Menus, Options)
}
{
	$Load(Menus, PatchNotes)
}
{
	Are you sure you want to quit the game?
	* Yes, quit the game. / No, return to the main menu.
	{
		$Quit()
	}
	{
		$Load(Menus, MainMenu)
	}
}