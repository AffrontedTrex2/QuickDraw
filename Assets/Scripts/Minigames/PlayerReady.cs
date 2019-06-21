using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReady : MonoBehaviour
{
	public GameObject[] playerReadySpaces;

	private PlayerReadySpace[] readySpaceScripts;
	private bool[] playersReady;

	void Start ()
	{
		playersReady = new bool[GameControl.instance.numOfPlayers];

		//Init the script array
		readySpaceScripts = new PlayerReadySpace[playerReadySpaces.Length];

		for (int i = 0; i < playerReadySpaces.Length; i++) {
			readySpaceScripts [i] = playerReadySpaces [i].GetComponent<PlayerReadySpace> ();
		}

		//Hide the players that aren't playing
		for (int i = GameControl.instance.numOfPlayers; i < playerReadySpaces.Length; i++) {
			playerReadySpaces [i].SetActive (false);
		}
	}

	//Listen for input
	void Update ()
	{
		//For every player. check for an input
		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {

			string interact = "Interact" + (i + 1);

			//If you confirm, then the panel background becomes green
			if (Input.GetButtonDown (interact)) {
				readySpaceScripts [i].Ready ();

				//That player is ready to play
				playersReady [i] = true;
			}
		}
	}

	public bool PlayersReady ()
	{
		for (int i = 0; i < playersReady.Length; i++) {
			if (!playersReady [i]) {
				return false;
			}
		}

		return true;
	}
}
