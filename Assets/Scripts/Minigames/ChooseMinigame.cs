using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMinigame : MonoBehaviour
{
	private string nextMinigame;

	SceneManagement sceneManager;
	ScoreUI scoreUI;

	void Start ()
	{
		sceneManager = GetComponent <SceneManagement> ();
		scoreUI = GetComponentInChildren <ScoreUI> ();

		//Get random minigame
		ChooseRandomMinigame ();

		//And update UI with the minigame
		scoreUI.UpdateNextGameText (nextMinigame);
	}

	void Update ()
	{
		//Wait for player1 to confirm before going to next minigame
		/*if (Input.GetButtonDown ("Interact1")) {
			LoadNextMinigame ();
		}*/

		if (scoreUI.timer < 0) {
			LoadNextMinigame ();
		}
	}

	void LoadNextMinigame ()
	{
        //Load it
        sceneManager.Invoke (nextMinigame, 0f);
	}

	void ChooseRandomMinigame ()
	{
		//If the minigame rotation list is empty, reset it
		if (GameInformation.minigameRotation.Count == 0) {
			FillMinigameRotation ();
		}

		//Get random minigame
		int index = Random.Range (0, GameInformation.minigameRotation.Count);
		nextMinigame = GameInformation.minigameRotation [index];

		//Remove that minigame from the list
		GameInformation.minigameRotation.RemoveAt (index);
	}


	//For testing purposes
	void PrintMinigameRotation ()
	{
		Debug.Log (GameInformation.minigameRotation.Count);
		foreach (string minigame in GameInformation.minigameRotation) {
			Debug.Log (minigame);
		}
	}

	void FillMinigameRotation ()
	{
		List<string> minigames;

        /*if (GameControl.instance.numOfPlayers > 2) {
			minigames = GameInformation.minigames;
		} else {
			minigames = GameInformation.minigamesTwoPlayer;
		}*/

        minigames = GameInformation.minigamesTwoPlayer;

        foreach (string game in minigames) {
			GameInformation.minigameRotation.Add (game);
		}
	}
}
