//Set standard values
$ReqChar(false)
$TypeSpeed(5)

//Text goes here
$SetKey(b)
$LoadTheme(CenterText)
Welcome to THE OPTIONS MENU. \n
Which options would you like to view?
$TypeRate(d)

* Controls / Audio / Video / Back to main menu
{
	$LoadTheme(Default)
	CONTROLS \n
	\n
	\n
	The only controls you need in this game are LEFT MOUSE CLICK and SPACEBAR. \n
	\n
	\n
	SPACE can be used to skip the text appearing letter by letter. Additionally, it is used to advance the game when all dialogue is printed (also indicated with "Press SPACE to continue" in the bottom-left corner). \n
	\n
	LEFT MOUSE CLICK is used to interact with the UI, that is, clicking the buttons that appear on-screen. \n
	\n
	That is literally all controls.
}
{
	$SetKey(c)
	AUDIO SETTINGS \n
	\n
	\n
	What would you like to change to the audio settings?
	* Disable background music / Enable background music / Disable text-appear sound / Enable text-appear sound / Nothing
	{
		<backgroundMusic=0>
		Background music disabled.
	}
	{
		<backgroundMusic=1>
		Background music enabled.
	}
	{
		<textSound=0>
		Letter-by-letter print no longer makes a sound.
	}
	{
		<textSound=1>
		Letter-by-letter print makes a sound again.
	}
	{
		$Goto(b)
	}
	$Goto(c)
}
{
	$SetKey(c)
	VIDEO SETTINGS \n
	\n
	\n
	What would you like to change to the video settings?
	* Disable letter-by-letter print / Enable letter-by-letter print / Disable fullscreen / Enable fullscreen / Nothing
	{
		<textAnim=0>
		Letter-by-letter print disabled.
	}
	{
		<textAnim=1>
		Letter-by-letter print enabled.
	}
	{
		<fullScreen=0>
		Fullscreen enabled.
	}
	{
		<fullScreen=1>
		Fullscreen disabled.
	}
	{
		$Goto(b)
	}
	$Goto(c)
}
{
	//Return to menu or other file
	$Load(Menus, MainMenu)
}
$Goto(b)