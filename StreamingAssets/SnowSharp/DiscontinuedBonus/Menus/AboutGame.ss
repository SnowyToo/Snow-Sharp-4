//If you are reading this, congrats! You found a file that was cut from the game!
//I decided to leave it in because SnowSharp files are laughably small. I suppose it rewards players looking into how it works.

$SetSpeed(d)
$SetTheme(Default)
TALES OF THE FORGOTTEN LANDS is primarily designed to be an interactive medium of expanding the Forgotten Lands. \n \n
Adding to that, it is also an homage to the old text-adventure games, albeit slightly modernized in its approach. \n
Sadly, because of how Snow# is designed (Snow# is the "language" that powers this game), I can't do them justice entirely in the sense that I can't give you freedom of typing in your own actions. \n \n
As a counterbalance, I aim to make your choices truly matter in this game, giving you the opportunity to leave a permanent footprint on the history (and future) of the Lands.

$SetKey(b)
Anything else you want information on?
* Canon / Choices / Snow# / Narratives / Back to main menu
{
	A valid question you might ask yourself relating to the canon is: "In a game that builds a story around player choice, and presumably has multiple ending timelines, does the story count as canon?" \n
	\n
	I'll make the statement here and now, clearly, that all events are written out to fit inside a coherent canon universe. This holds true regardless of which branch you end up on. \n
	That might sound like a contradiction in terms. How can two vastly different courses of history be canon in the same universe? \n \n
	The answer is that these histories relate to FORGOTTEN Lands. Forgotten by name and Forgotten by design. \n
	What I mean by this is that all different branches of the story in ToTFL are in essense telling the same story, but each time told by someone else. \n
	So in that sense, the entire game is canon to the universe.
	
	If you happen to find that answer unsatisfactory; you don't even have to take these narratives as canon. It works both ways. \n
	What I will say, however, is that all information found in THE LIBRARY OF INFINTE KNOWLEDGE are absolute canon. \n
	No matter how much or how little you happen to agree with it.
}
{
	The choices in ToTFL are impactful by design (but that comes with an asterisk). \n
	\n
	All choices that FEEL like they should impact the flow and direction of the storyline, WILL impact it appropriately. \n 
	Either in a small (different character interaction, extra dialogue,...) or big (entirely new branches of story, different "combat" behaviour,...) way.

	There are, however, also choices that DON'T impact anything (and I'm pointing that out now, so you can't blame me for lying about "choices mattering" later). \n
	These choices are mostly reserved for dialogue options (e.g. asking explanatory questions) and exploration options (e.g. exploring ruins that are not on the main narrative path) that have the sole purpose of expanding the world. \n
	In choices like those is the opportunity for you to explore the story and environment of the Lands outside of the main narrative, which is something I'd gladly drop the facade of "your choices matter" for. \n
	\n
	I suppose that, in a way, you could see the latter type of choices more as "side-quests" (or "side-narratives") to the main narrative. They are there for whoever shows interest in them, but they are definitely not obligatory.
}
{
	Snow# is a project I started in August 2017 when working on a very meta, break-the-fourth-wall game called "Kate!". \n
	It was originally designed to be a "dialogue language", that is, a kind of "scripting language" that could be interpreted to make dynamic dialogue trees and manage set variables that were native to Kate!'s design (reputation, interest, friendliness, doneWithYouPercentage). \n
	I quit work on the original Snow# in October that same year, when I quit the Kate! project. \n
	\n
	In late 2018, however, I picked up Snow# again and decided to rewrite it from scratch under the name "Snow# 4" (Snow# had undergone three "rewrites" during my work on Kate! already). \n
	This rewrite had the goal in mind to universalize Snow# to more than just the Kate! project, and to greatly expand its functionality and customizability. \n
	This effectively turned it more into more of a "narrative environment" than a "dialogue language" as it can be used for way more than just creating branching dialogue trees nowadays.
}
{
	ToTFL is a bundle of different medium-to-long narratives, split up in chapters. \n
	As of the release of the game, there are plans for 4 full narratives, all of which set in the Second or Third Era (though The First Era will not go unexplored in this game). \n
	This means all of them take place BEFORE Myths of the Forgotten Lands (the novel). \n 
	\n
	The amount of chapters between narratives may vary, but I intend on setting a minimum of 4 relatively short ones (between 15 and 60 minutes). \n
	The way they are released will be going per CHAPTER (not narrative). This means the game will see frequent updates (every other week if time allows me) for as long as it's in active development. \n
	\n
	For updates on the chapters and narratives as they are being developed, you can follow me on Twitter (@SnowGamesDev).
}
{
	$Load(Menus, MainMenu)
}
$Goto(b)