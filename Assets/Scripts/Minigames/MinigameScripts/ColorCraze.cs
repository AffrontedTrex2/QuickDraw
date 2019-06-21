using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ColorCraze : MonoBehaviour
{
	public Tilemap tilemap;
	public GameObject scoreBar;

	private ScoreBarUI scoreBarUI;

	//The colored tiles the players have
	TileBase[] playerTiles;
	int[] points;

	public float time = 10f;

	void Start ()
	{
		
	}

    //Placed in init because playerTiles[i] line has to be called AFTER GC has found the players
    public void Init() {
        //Enable player changetilecolor script
        /*for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {
			GameControl.instance.players [i].GetComponent<ChangeTileColor> ().enabled = true;
		}*/

        //Create array of player Tiles
        playerTiles = new TileBase[GameControl.instance.numOfPlayers];

        //For each player, find the object and get their tile object
        for (int i = 0; i < playerTiles.Length; i++) {
            playerTiles[i] = GameControl.instance.players[i].GetComponent<ChangeTileColor>().changedTile;
        }

        //Set up points
        points = new int[playerTiles.Length];

        scoreBarUI = scoreBar.GetComponent<ScoreBarUI>();

        //scoreBarUI.ShowBars ();
    }

    void Update ()
	{
		if (GameControl.instance.minigameOver) {
			return;
		}

		UpdateScore ();

		if (time <= 3) {
			SetTimerText ();
		}

		if (time < 0) {
			GameControl.instance.EndMinigame (points);
		}

		time -= Time.deltaTime;
	}

	void SetTimerText ()
	{
		int setTime = 0;

		if (time <= 3) {
			setTime = 3;
		}
		if (time <= 2) {
			setTime = 2;
		}
		if (time <= 1) {
			setTime = 1;
		}
		if (time <= 0.05f) {
			setTime = 0;
		}

		GameControl.instance.SetTimerText (setTime);
	}

	void UpdateScore ()
	{
		//Update score
		CountColorBlocks ();

		//Then update UI score bars
		scoreBarUI.UpdateBars (points);
	}

	//Counts the number of blocks each player has
	void CountColorBlocks ()
	{
		int[] tempPoints = new int[playerTiles.Length];

		BoundsInt bounds = tilemap.cellBounds;
		TileBase[] allTiles = tilemap.GetTilesBlock (bounds);

		for (int x = 0; x < bounds.size.x; x++) {
			for (int y = 0; y < bounds.size.y; y++) {
				TileBase tile = allTiles [x + y * bounds.size.x];

				//Check whose tile it is and give them a point
				if (tile != null) {
					//Cycle through and check if the tile matches any player's tile
					//If it does, add a point to the point array
					for (int i = 0; i < playerTiles.Length; i++) {
						if (tile == playerTiles [i]) {
							tempPoints [i]++;
							continue;
						}
					}
				}
			}
		}

		points = tempPoints;
	}

}
