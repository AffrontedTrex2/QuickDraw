using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameInfo : MonoBehaviour
{
	public List<Minigame> minigames;

	void Start ()
	{
		minigames = new List<Minigame> ();

		Init ();
	}

	//Create minigames, add them all to the list
	void Init ()
	{
		Minigame colorCraze = new Minigame ("Color Craze", "Run around to paint the ground. Player with the most colored tiles wins!", "ColorCraze");
		minigames.Add (colorCraze);

		Minigame stomp = new Minigame ("Stomp", "Stomp on other players' heads to kill them!", "Stomp");
		minigames.Add (stomp);

		Minigame bombTag = new Minigame ("Bomb Tag", "Don't be the one holding the bomb when it explodes!", "BombTag");
		minigames.Add (bombTag);

		Minigame ghostScare = new Minigame ("Ghost Scare", "Press down to scare your opponents! Be careful; if there's nobody in your scare radius, you'll be stunned!", "GhostScare");
		minigames.Add (ghostScare);

		Minigame spikeBall = new Minigame ("Spike Ball", "Avoid the spiked ball! It'll get faster and wilder as time passes...", "SpikeBall");
		minigames.Add (spikeBall);

		Minigame fallingBlocks = new Minigame ("Falling Blocks", "Avoid the falling blocks!", "FallingBlocks");
		minigames.Add (fallingBlocks);

		Minigame sugarRush = new Minigame ("Sugar Rush", "It's like musical chairs, but you're grabbing candy instead of smushing your butt onto chairs.", "SugarRush");
		minigames.Add (sugarRush);

		Minigame chainsawBackstab = new Minigame ("Chainsaw Backstab", "As the name suggests, you're equipped with a high calibre chainsaw. Unfortunately, it only cuts through backs...not fronts.", "ChainsawBackstab");
		minigames.Add (chainsawBackstab);

		Minigame fruitForecast = new Minigame ("Fruit Forecast", "Gather the falling fruit with your baskets. Careful of the spikeballs though, they'll stun you for a few moments.", "FruitForecast");
		minigames.Add (fruitForecast);

		//Vertigo doesn't work because:
		//Some bug where you clip through the platforms (maybe caused by player jumping up + platform moving down)
		//Sometimes you can't jump because the platform is moving downwards, and the player briefly is in the air
		/*Minigame vertigo = new Minigame ("Vertigo", "Don't touch the red blocks!", vertigoDemo, "Vertigo");
		minigames.Add (vertigo);*/
	}
}
