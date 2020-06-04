$LoadTheme(Default)
$TypeRate(d)
$ReqChar(false)
|metFeldir=0, else|
{
	$SetCast(Librarian, You)
}
{
	$SetCast(Feldir, You)
}

//Message
|firstPass=0, else|
{
	$LoadTheme(Menu)
	$TypeRate(5)
	You enter the LIBRARY OF INFINITE KNOWLEDGE. \n
	|metFeldir=0, else|
	{
		|openedLib=0, else|
		{
			You see an an ancient Dwarf - presumably the librarian - at a desk. He greets you with a smile. \n
		}
		{
			The librarian greets you from his desk. Smiling, as always. \n
		}
	}
	{
		|openedLib=0, else|
		{
			You see Feldir sitting at a desk. He welcomes you, warmly smiling. \n
		}
		{
			Feldir greets you with a smile from his desk, as per usual. \n
		}
	}
	$SetChar(0)
	\n
	$ReqChar(true)
	|openedLib=0 & metFeldir=0, openedLib=0 & metFeldir=1, else|
	{
		<openedLib=1>
		Hello, newcommer! What information might you seek?
	}
	{
		<openedLib=1>
		Hello, friend! Tell me, what brings you here?
	}
	{
		Welcome back, friend! How can I assist you this time?
	}
}
{
	$ReqChar(true)
	-0
	Anything else?
}

//Information goes here.
[firstPass=1]
$SetKey(b)
$TypeRate(d)
-You
* The Library of Infinite Knowledge / Essentia / The Tree of Life / The Elder / More... / Return to Main Menu
{
	$Load(Menus/Library, Library)
}
{
	$Load(Menus/Library, Essentia)
}
{
	$Load(Menus/Library, Tree)
}
{
	$Load(Menus/Library, Elder)
}
{
	* The Forsaken / The Ethereal Temples / Races of The Lands / Forbidden Magiks / The Abyss / More...
	{
		$Load(Menus/Library, Forsaken)
	}
	{
		$Load(Menus/Library, Temples)
	}
	{
		//Races
		//TBI
	}
	{
		$Load(Menus/Library, Mortis)
	}
	{
		$Load(Menus/Library, Abyss)
	}
	{
		* Settlements / More...
		{
			//Settlements
			//TBI
		}
		{
			$Goto(b)
		}
	}
}
{
	$LoadTheme(Default)
	That was it, thanks!
	[firstPass=0]
	$Load(Menus, MainMenu)
}



I'm sorry, you're not supposed to see this. \n
If you can, something went severely wrong! \n
Please let the developer know you broke his game... (and preferably also how.) \n
\n
(Press SPACE to reload the menu)
$Goto(b)