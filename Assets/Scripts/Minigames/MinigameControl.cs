using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameControl : MonoBehaviour
{
	MinigameUI ui;
	MinigameInfo info;

	PlayerReady playerReadyScript;

	bool uiClosed = false;

	void Start ()
	{
		//Don't allow pause
		GameControl.instance.pause_infoscreen = true;

		info = GetComponent <MinigameInfo> ();
		ui = GetComponent <MinigameUI> ();

		playerReadyScript = ui.minigameUI.GetComponentInChildren <PlayerReady> ();

		//Find players first to avoid error
		GameControl.instance.FindPlayers ();

		//activate the ui on startup
		//with the correct game being played
		ui.UpdateMinigameUI (GetCurrentMinigame ());
		ui.ShowMinigameUI ();
	}

	void Update ()
	{
		if (!ui.gameObject.activeSelf || uiClosed) {
			return;
		}

		//Wait for player to press before closing the ui
		/*string interact = "Interact1";

		if (Input.GetButtonDown (interact)) {
			ui.ClearAndHideMinigameUI ();

			//Start minigame after
			GameControl.instance.StartMinigame ();

			uiClosed = true;
		}*/

		//If playerReadyScript shows that each player is ready, start the minigame
		if (playerReadyScript.PlayersReady ()) {
			StartCoroutine (StartMinigame ());
		}
	}

	//Starts the minigame after pausing one second
	IEnumerator StartMinigame ()
	{
		uiClosed = true;

		yield return new WaitForSecondsRealtime (.5f);

		ui.ClearAndHideMinigameUI ();

		//Start minigame
		GameControl.instance.StartMinigame ();

		//Allow pausing
		GameControl.instance.pause_infoscreen = false;
	}

	//Returns a minigame object based on which scene is active
	Minigame GetCurrentMinigame ()
	{
		string scene = SceneManager.GetActiveScene ().name;

		foreach (Minigame game in info.minigames) {
			if (game.Equals (scene)) {
				return game;
			}
		}

		return null;
	}
}
