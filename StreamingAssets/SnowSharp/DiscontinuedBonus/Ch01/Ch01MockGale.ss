//If you are reading this, congrats! You found a file that was cut from the game!
//I decided to leave it in because SnowSharp files are laughably small. I suppose it rewards players looking into how it works.

//Set standard values
$LoadTheme(Default)
$ReqChar(false)
$TypeSpeed(d)

//TEXT STARTS HERE
|goFirst=0|
//IF YOU DIDN'T GET ATTACKED ALREADY, YOU GET ATTACKED HERE.
{
    It doesn't even take a blink of an eye for the beast to see where you are, and without hesitation it charges at you. \n
    \n
    |earthUsed=1, else|
    //MADE WALL
    {
        Whether due to the amount of dust floating around, the pent-up rage of the Mocker's Gale or because it's been starving for who knows how long, you don't know. \n
        But as it charges towards you, it doesn't seem to notice the walls you summoned earlier, and smashes face-first into one of them. \n
        The wall breaks, but doesn't crumble yet, possibly being able to take another hit from the animal. \n
        \n
        The Mocker's Gale quickly steps backwards, shaking its head roughly around. \n
        It appears to be stunned.
        [stunned=1]
    }
    //DIDN'T MAKE WALL; DODGE ROLL
    {
        $RAD(85 - 25*fireUsed, else)
        //SUCCESSFUL DODGE
        {
            Despite the trade goods you're carrying, you manage to jump out of the way of his charge just barely. \n
        }
        //UNSUCCESSFUL DODGE
        {
            [damaged=1]
            Despite your best efforts to get out of the way in time, your robe gets caught on one of its tusks. \n
            A piece tears off it, and you get pushed to the side. You do manage to maintain your balance, however. \n
        }
        The beast turns around, to face you and prepares itself to charge again.
        It is time you take action.
    }
}
[cbTurn=0]

//YOUR MOVE
$SetKey(b)
[cbTurn=cbTurn+1]
* Try to run / Check surroundings / Essentia Manipulation 
//TRY TO RUN
{
    //RUNNING WILL ONLY WORK WHEN BEAST IS STUNNED OR SNARED.
    |stunned=0 & snared=0, else|
    {
        //FAILED RUN ATTEMPT
        You try to run away past the Mocker's Gale. \n
        But despite the rocks weighing it down, and your best efforts, it still seems to have a vast speed advantage over you, blocking your path as fast as the winds with every step you try to take.
    }
    {
        //SUCCEEDED RUN ATTEMPT
        |stunned>0, else|
        {
            With the Mocker's Gale stunned due to the impact, this seems like the perfect time to make a run for it. \n
        }
        {
            With the Mocker's Gale locked in place due to his feet being held captive by the stone, this seems like the perfect time to make a run for it. \n
        }
    }
}
//CHECK
{
    |areaCheck=1, areaCheck=2, else|
    //CHECKED BEFORE
    {
        For a reason beyond my own explanation, you find it necessary to check your surroundings again. \n
        Even though literally JUST DID before being charged at. \n
        \n
        Needless to say, nothing has changed, except now you're obviously being attacked by a hungry Mocker's Gale. \n
        \n
        ... \n
        I don't know what you expected...
    }
    //CHECKED ALREADY DRUING COMBAT
    {
        Nothing has changed since last time you checked. \n
        Still very much being attacked by a Mocker's Gale, by the way.
    }
    //FIRST CHECK
    {
		The area is covered in a thick cloud of dust, presumably from the branches of the Dustleaves surrounding the area. \n
		It's difficult to see anything; the only things you can see clearly, are the flickering, flaming pieces of the Scorchbarks' bark. \n
		\n
        You're being attacked by a Mocker's Gale, but not an ordinary one. \n
        As opposed to the normal, wind-like creatures, this one seems to be embedded with a rock-like armor in its "skin", making it vastly easier to see it move around. \n
        You suspect it being a cross-breed of sorts, or a victim to experiments of Bestial Manipulation.
    }
    [areaCheck=2]
}
//ESSENTIA MANIPULATION
{
    //TAKE OUT CRYSTALS FIRST
    |essentia=0|
    {
        You reach for your Essentia Crystals in your pocket and throw them in the air. They stay afloat close to your body, just above your back.
    }

    * Lower rock walls (|earthUsed=1|) / Launch rock wall (|earthUsed=1 & wallDead=0|) / Enlarge Bonfire (|fireUsed>0|) / Extinguish Bonfire (|fireUsed>0|) / Enstone its feet / Fireball / Fire Circle
    //REGATHER EARTH ESSENTIA
    {
        [N1E=N1E+7-1*wallDead]
        You lower the rock walls around you, regaining the Essentia you spent on three of them in the process. \n
        |wallDead=0, else|
        //WALL CRUMBLED, NOT DEAD
        {
            Due to the fourth wall being cracked, the amount of Essentia you can regain from it is drastically decreased. \n
        }
        //WALL DEAD
        {
            Since the fourth wall is obliterated, however, you cannot reclaim its Essentia. \n
        }
        \n
        You've made yourself vulnerable to the Mocker's Gale attacks once more.
    }
    //LAUNCH ROCK WALL
    {
        [N1E=N1E-3]
        Using your Earth Essentia once again, you put a seismic force behind the cracked wall, causing it to launch itself at the already stunned Mocker's Gale. \n
        The green glow in your Essentia Crystal seems to lessen slightly as you manipulate the wall. You estimate about 20% of the crystal to be drained. \n
        [wallDead=1]
        \n
        The wall hits the beast with such force (staight in the face, at that!), that it causes it to fly about a feet backwards and fall to the ground. \n
        It still seems to be breathing fine, but it definitely looks battered. \n
        \n
        It's not going to put up much of a fight anymore.
        [wornOut=1]
    }
    //REDIRECT BONFIRE FLAMES
    {
        [N1F=N1F-3]
        |fireUsed=1, else|
        {
            Using your Fire Essentia once again, you cause the bonfire to grow larger. It's grown to fill about a third of the walking space you have. \n
            The red glow in your Essentia Crystal seems to lessen slightly as you manipulate the fire. You estimate a little over 20% of the crystal to be drained. \n
            \n
            As the fire grows larger, its presence grows more threatening and ominous. You can see the beast's glowing eyes fill with dismay. \n
            It appears to be paralyzed in place by its fear.
            [fireUsed=2]
        }
        {
            Using your Fire Essentia a third time, you cause the bonfire to grow larger. It growns to fill about half the walking space you have. \n
            \n
            The discomfort in the eyes of the Mocker's Gale grow in the same way the bonfire grows, until you see it disappear behind the wall of flames you just created. \n
            A high-pitched, hog-like sound can be heard from behind the fire, followed by four anxious feet trying their best to get out of there as fast as possible.
            [fireUsed=3]
        }
    }
    //REGATHER FIRE ESSENTIA
    {
        [N1F=N1F+5+2*fireUsed]
        You extingish the bonfire entirely, regaining the Fire Essentia you used while creating it. \n
        \n
        Your vision decreases again.
    }
    //FEET PRISON
    {

    }
    //FIREBALL
    {

    }
    //FIRE CIRCLE
    {

    }
}

//ITS MOVE

//CHECK FOR WORN OUT
|wornOut=1|
{

}

//CHECK FOR STUNS OR SNARES
|stunned>0, snared=1|
{
    [stunned=stunned-1]
    
}
{
    $RAD(70, else)
    //FAILED ATTEMPT TO BREAK FREE
    {

    }
    //BREAKS FREE
    {

    }
}

//TURN
|cbTurn>2|
//MARK THE BEAST; BUTTERFLY EFFECT.
{

}


//Return to menu or other file
$Load(FenchorEntrance)