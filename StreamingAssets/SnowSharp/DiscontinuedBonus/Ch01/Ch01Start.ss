//If you are reading this, congrats! You found a file that was cut from the game!
//I decided to leave it in because SnowSharp files are laughably small. I suppose it rewards players looking into how it works.

//Set standard values
$LoadTheme(Default)
$ReqChar(false)
$TypeSpeed(d)

//Set File Stats
$SetCast(You)
<N1F=60>
<N1E=60>

//Text goes here
You look up at the sky. The Sun's almost at her apex, almost noon, but strangely, she hasn't burdened you on your trip from the Capital yet. \n
\n
In fact, a strong summer wind has been keeping you rather chilly at the times you were in the open; you bless yourself lukcy most of the path has trees surrounding it, acting as extra cover against the winds. \n
\n
Then again, who could've forseen that for this time of year? Merchants never travel between Fenchor and Ghelindor because of how high the temperatures can reach... \n
This year decided to be different in that regard, apparently...

A soothing breeze brushes comfort against your face, and wraps you in a mild enjoyment you cannot hide. \n
You take a deep breath and take a moment to bathe in the soundless nature; something you sometimes forgot existed in Fenchor.
$SetSpeed(0.7)
[pitch=5]
EEEEEP
[pitch=1]
$SetSpeed(d)
A sharp shriek from behind some rockberry bushes closeby shatters your moment of silence. \n
It sounded very much like a young hare in trouble, but you're not certain. It could be a trap set out by a hungry Mockers' Gale.
* Ignore the call / Investigate the scene
//IGNORE (BORING PATH)
{
	You decide to keep your urge to check the situation at bay and carry on to your home city. \n
	As you make your way along the path, the screams seem to get louder and more distressed, sending a shiver of cold dispair down your spine. You can feel them haunting you in your wake. \n
	You pick up the pace to outrun the call for help. It doesn't take long afterwards for hare to give up, with its screeching turning into a deep, annoyed grumble.
	Your heart skips a beat in relief as you slow down again. Your ability to recognize a Mocker's Gale amazes you. \n
	\n
	... Or it was dumb luck... Probably just dumb luck...

	It takes you a moment to get your senses again, but when you finally do, you resume your trip, taking your luck as a good omen of The Forsaken. \n
	That in mind, you start marching to Fenchor again, whistling the melody of the city.
}
//INVESTIGATE (EVENTFUL PATH)
{
	You follow the sound, leading you to a small clearing in the forest, surrounded by several Scorchbarks and Dustleaves, making it fairly hard to see what is even going on. The screams abruptly stop. \n
	Though you can't see much, you can see enough to know there isn't any hare around, nor anything that could've caused it distress. \n
	\n
	The silence is broken once again by the rustling of leaves. You can't quite tell where it's coming from, but as it goes on, there's an occasional "TOCK". \n
	As if two rocks are colliding with each other.
	It starts to dawn on you that YOU might be the one in trouble now.
	* Run back / Essentia Manipulation / Check surroundings / Play dead
	//RUN BACK
	{
		You turn around as quickly as your heartrate skyrocketed, rushing for the pathway you came from, beyond those rockberry bushes.
		[pitch=0.7]
		$SetSpeed(0.6)
		ROOAARR
		$SetSpeed(d)
		[pitch=1]
		Before you can even reach the rockberries, you're stopped dead in your tracks by a Mocker's Gale leaping out at you. \n
		$RAD(85, else)
		{
			You manage to dodge the beasts' stone tusks, just barely. \n
		}
		{
			Due to the trade goods you're carrying, you find it difficult to dodge its tusks coming at you. \n
			You are pushed back and fall to the ground with a heavy blow, tearing your robe, while causing your Essentia Crystals to roll out of your opened pocket. They stay afloat close to you.
			[damaged=1]
		}
		The Mocker's Gale does not seem happy to see you.
		[goFirst=1]
		$Load(Nar01/Ch01, Ch01MockGale)
	}
	//ESSENTIA
	{
		[essentia=1]
		You reach for your Essentia Crystals in your pocket and throw them in the air. They stay afloat close to your body, just above your back.
		* Make bonfire / Blow dustcloud away / Extinguish Scorchbarks / Make stone walls around you
		{
			[N1F=N1F-8]
			[fireUsed=1]
			You reach out for your Fire Essentia Crystal, making it glow red hot as the Essentia syphons out of it. \n
			With the assistance of your Magica Essentia, you manage to light a bonfire in the middle of the clearing. \n
			The light it gives off makes it exponentially easier to see the surrounding area, effectively giving away that the rustling was coming from between the rockberry bushes.
		}
		{
			You think of blowing the dust away, but fear that's going to be quite difficult without any Air Essentia. \n
			If only you'd anticipated this very unlikely course of events would take place...
		}
		{
			You think of extinguishing the Scorchbarks, but fear that's going to be quite difficult without any Water Essentia. \n
			If only you'd anticipated this very unlikely course of events would take place...
		}
		{
			[N1E=N1E-9]
			[earthUsed=1]
			You reach out for your Earth Essentia Crystal, making it glow a poison green as the Essentia syphons out of it. \n
			With the assistance of your Magica Essentia, you manage to emerge four walls from around you, roughly reaching up to your waist. \n
			Even though you still can't really see anything, you can feel a little safer now.
		}
	}
	//CHECK SURROUNDINGS
	{
		[areaCheck=1]
		The area is covered in a thick cloud of dust, presumably from the branches of the Dustleaves surrounding the area. \n
		It's difficult to see anything; the only things you can see clearly, are the flickering, flaming pieces of the Scorchbarks' bark. \n
		\n
		As you listen closer, the sound of rustling seems to be coming from everywhere at once, but the rocks "TOCK"ing against each other is very clearly coming from just one way: the rockberry bushes you came through.
	}
	//PLAY DEAD
	{
		Instinctively, you drop to the floor and hold your breath. Making sure to appear as limp as possible. \n
		From between the rockberry bushes, a rock-covered Mocker's Gale lunges forward at seemingly nothing. \n
		\n
		It walks around the clearing, certain of the fact it heard something fall for its trap. \n
		You close your eyes, but can't decide whether you did that to make the act more believable, our out of genuine fear.
		
		Minutes pass, in which the Mocker's Gale is calmly walking around. \n
		Loud sniffing noises of its hog's nose fill the area as it's searching for something. Anything. Whatsoever. \n
		He is getting closer by the second, until his nose is virtually touching your own... \n
		\n
		Shivers run from down your spine to all over your body as the Mocker's Gale cold breath finds your face. \n
		The stench of the beast forces itself down your nostrils, being so strong that your eyes tear up. Not wincing due to it proves to be the biggest challenge of all this. \n
		\n
		And then...

		...he pulls away. \n
		\n
		Seemingly alerted by something, but not by something you heard. \n
		Disturbed - maybe even panicking - it runs away through the trees, deeper into the forest. \n
		\n
		Leaving you to guess what was the thing that caused him to leave... \n
		You slowly open your eyes again, relieved at the fact he wasn't just micking that sound.

		//BACK TO FENCHOR
		Whatever the cause may be, it's no longer of your concern. If anything, you take it as an omen that The Forsaken smile upon your fate. \n
		\n
		After a minute or two to parse what just happened, you get back up on your feet and brush the dirt off your clothes. \n
		It takes you another minute to get your barings again, but after you found those damned rockberry bushes that started this all, you're on your way to Fenchor again... \n
		... faster than before, admitedly.

		$Load(Nar01/Ch01, Fentrance)
	}	
	|fireUsed=0 & areaCheck=0, else|
	//YOU DON'T KNOW ITS POSITION HERE
	{
		From between the rockberry bushes, a beast lunges forward. \n
	}
	//YOU DO KNOW IT'S POSITION HERE
	{
		A rock-covered beast lunges forwards at you from between the bushes. \n
	}
	\n
	You recognize the creature underneath the stone armor as a Mocker's Gale. \n
	And you can tell from the look on its face... \n 
	It is NOT happy you're there.
	$Load(Nar01/Ch01, Ch01MockGale)
}

//Return to menu or other file
$Load(Nar01/Ch01, Ch01Fentrance)