using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInformation
{
	public static float playerMaxSpeed = 5;
	public static float playerJumpTakeOffSpeed = 8;

	public static string mainMenu = "MainMenu";
	public static string scoreScreen = "ScoreScreen";
	public static string winScreen = "WinScreen";
	public static string playerSelect = "PlayerSelect";
	public static string keyboardSelect = "KeyboardSelect";
    public static string options = "Options";

	public static List<string> cannotPauseScreens = new List<string> () {
		mainMenu,
		scoreScreen,
		winScreen,
		playerSelect,
		keyboardSelect
	};

	public static string colorCraze = "ColorCraze";
	public static string bombTag = "BombTag";
	public static string ghostScare = "GhostScare";
	public static string spikeBall = "SpikeBall";
	public static string stomp = "Stomp";
	public static string fallingBlocks = "FallingBlocks";
	public static string vertigo = "Vertigo";
	public static string sugarRush = "SugarRush";
	public static string chainsawBackstab = "ChainsawBackstab";
	public static string fruitForecast = "FruitForecast";

	public static List<string> minigames = new List<string> () {
		colorCraze,
		bombTag,
		ghostScare,
		spikeBall,
		stomp,
		fallingBlocks,
		//vertigo,
		sugarRush,
		chainsawBackstab,
		fruitForecast
	};

	public static List<string> minigamesTwoPlayer = new List<string> () {
		colorCraze,
		bombTag,
		ghostScare,
		spikeBall,
		stomp,
		fallingBlocks,
		//vertigo,
		//sugarRush,
		chainsawBackstab,
		fruitForecast
	};

	//Used so that each minigame is played once before repeat minigames
	public static List<string> minigameRotation = new List<string> ();














	public static string playerTag = "Player";


	public static string enemyTag = "Enemy";
	public static string wallTag = "Wall";
	public static string crateTag = "Crate";

	//If projectiles hit this, they disappear
	public static ArrayList canCollide = new ArrayList { enemyTag, wallTag, crateTag };

	public static int enemyRegularDamage = -1;
	public static int enemyRegularScore = 20;
	public static int enemyRegularExp = 10;

	public static int playerDamage = 1;

	public static int coinScore = 10;

	public static int expPlant = 50;

}
