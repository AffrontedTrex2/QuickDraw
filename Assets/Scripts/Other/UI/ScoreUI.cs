using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
	public Text scoreText;
	public Text nextGameText;
	public Image loadingBar;

	public float timer = 5f;

	private MinigameInfo minigameInfo;

	void Start ()
	{
		minigameInfo = GetComponent <MinigameInfo> ();
		UpdateScore ();
	}

	void Update ()
	{
		if (timer < 0) {
			return;
		}

		timer -= Time.deltaTime;

		UpdateLoadingBar ();
	}

	public void UpdateNextGameText (string minigame)
	{
		//Find the actual name of the minigame
		string name = minigame;
		foreach (Minigame game in minigameInfo.minigames) {
			if (game.Equals (name)) {
				name = game.name;
				break;
			}
		}

		nextGameText.text = "Next:\n" + name;
	}

	//Fills the loading bar according to how much time is left
	void UpdateLoadingBar ()
	{
		if (timer < 0) {
			loadingBar.fillAmount = 0;
			return;
		}

		loadingBar.fillAmount = timer / 5f;
	}

	void UpdateScore ()
	{
		//Get scores from gamecontrol and update text
		string text = "";

		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {
			text += "Player " + (i + 1) + ": " + GameControl.instance.playerScores [i] + "\n";
		}

		scoreText.text = text;
	}


}
