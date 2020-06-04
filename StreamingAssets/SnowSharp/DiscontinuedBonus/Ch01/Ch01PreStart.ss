//If you are reading this, congrats! You found a file that was cut from the game!
//I decided to leave it in because SnowSharp files are laughably small. I suppose it rewards players looking into how it works.

//Set standard values
$LoadTheme(Default)
$ReqChar(false)
$TypeSpeed(d)

//Pre-Chapter Introduction.
Before you start this adventure, would you like some background on the character you're playing and the situation in which you'll start?
* Yes / Yes, but give me a TL;DR / No
{
	The year is 661 of the SECOND ERA, well over a year before THE ELDER JUDGEMENT would take place. \n
	\n
	You are AESMYR, daughter of GENERAL MAEDROCH, of THE DWARVES. Merchant by profession, and member of FENCHOR's City Council. \n
	Your mother was a PURE MAGUS by education, but retired from that task in favor of raising her child. She did, however, still entertain herself (and you) by leisurely practicing ESSENTIA MANIPULATION. \n
	It was only after her death that you became determined to pursue the same level of mastery of Essentia as her - both in knowledge and Manipulation. \n
	By now, you could call yourself an amateur Pure Magus, as well an amateur researcher on the topic.

	You are currently on your way back to you home city of FENCHOR, returning from a two-day trip to GHELINDOR, CAPITAL UNDER THE MOUNTAIN. \n
	Your business there was primarily to stock up on trade goods, as well as recharge your FIRE and EARTH ESSENTIA CRYSTALS. \n
	You inherited an ANULUS PRAECLUSUM - a ring that can store an indefinite amount of Essentia - from your mother. Hers was attuned to MAGICA. \n
	\n
	This is where your story begins. \n
}
{
	The year is 661 of the SECOND ERA, before THE ELDER JUDGEMENT. \n
	\n
	You are AESMYR of the Dwarves, a well-known and well-respected member of FENCHOR's City Council. \n
	You are returning to your home city after a trip to THE CAPITAL UNDER THE MOUNTAIN. \n
	You currently have FIRE and EARTH ESSENTIA CRYSTALS in your possesion. \n
	Your MAGICA ESSENTIA is stored in a ring you're wearing.
	\n
	...\n
	That's about all relevant information in a nutshell. \n
}
{
	Well then... \n
}
\n
Good luck, Aesmyr!
//Return to menu or other file
$Load(Nar01/Ch01, Ch01Start)