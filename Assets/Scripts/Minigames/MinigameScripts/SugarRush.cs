using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarRush : MonoBehaviour
{
	public GameObject candyObj;
	public Transform[] spawnPositions;

	private List<Vector3> candyPositions;

	void Start ()
	{
		SummonCandies ();
	}

	void Update ()
	{
		if (GameControl.instance.minigameOver) {
			return;
		}

		//If all the candies are gone
		if (NoMoreCandies ()) {
			//First kill all the candyless players
			KillCandylessPlayer ();

			//Then check if the game is over
			if (GameControl.instance.minigameOver) {
				return;
			}

			//Then summon candies again
			SummonCandies ();
		}
	}

	void KillCandylessPlayer ()
	{
		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {

			GameObject player = GameControl.instance.players [i];

			//If a certain player doesn't have candy
			if (!player.GetComponent<PlayerPlatformerController> ().hasCandy) {
				//Kill them
				GameControl.instance.KillPlayer (player);

				return;
			}
		}
	}

	bool NoMoreCandies ()
	{
		return gameObject.GetComponentInChildren <Candy> () == null;
	}

	void SummonCandies ()
	{
		//Reset candy positions
		candyPositions = new List<Vector3> ();

		//Reset all players hasCandy status
		ResetHasCandy ();

		int numOfCandies = GameControl.instance.GetNumberOfPlayersAlive () - 1;

		for (int i = 0; i < numOfCandies; i++) {
			//Create a candy object as a child
			GameObject candy = Instantiate (candyObj, transform);

			//Randomize candy position
			SetPosition (candy);
		}
	}

	void ResetHasCandy ()
	{
		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {
			GameControl.instance.players [i].GetComponent<PlayerPlatformerController> ().hasCandy = false;
		}
	}

	void SetPosition (GameObject candy)
	{
		int index = Random.Range (0, spawnPositions.Length);
		Vector3 pos = spawnPositions [index].position;

		//Find a position that isn't already taken by another candy
		while (candyPositions.Contains (pos)) {
			index = Random.Range (0, spawnPositions.Length);
			pos = spawnPositions [index].position;
		}

		//Add the position to the list
		candyPositions.Add (pos);

		//Set new position
		candy.transform.position = pos;
	}
}
