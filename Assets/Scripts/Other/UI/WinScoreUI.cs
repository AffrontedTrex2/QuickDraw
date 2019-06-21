using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScoreUI : MonoBehaviour
{
	public Text winnerText;
	public Text[] scoreTexts;
	public GameObject[] images;

	private Color[] playerColors;

	private SceneManagement sceneManager;

	void Start ()
	{
		sceneManager = GetComponentInParent <SceneManagement> ();

		playerColors = new Color[]{ Color.white, Color.red, Color.green, Color.yellow };

		Dictionary<int, int> ordereredScores = GetScores ();
		UpdateScores (ordereredScores);
	}

	void Update ()
	{
		//If player 1 presses A, go back to the menu
		string interact = "Interact1";
		if (Input.GetButtonDown (interact)) {
			sceneManager.MainMenu ();
		}
	}

	void UpdateScores (Dictionary<int, int> ordereredScores)
	{
		int pos = 0;
		foreach (KeyValuePair<int, int> entry in ordereredScores) {
			//Set winner text first
			if (pos == 0) {
				winnerText.text = "PLAYER " + (entry.Key + 1) + " WINS!";
			}

			//Set score texts
			scoreTexts [pos].text = "Player " + (entry.Key + 1) + "\n" + entry.Value;

			//Set image color
			UpdateImage (pos, entry.Key);

			pos++;
		}

		//Set the rest of the score text boxes to empty
		//And hide the rest of the player spaces
		for (int i = pos; i < scoreTexts.Length; i++) {
			scoreTexts [i].text = "";

			images [i].SetActive (false);
		}
	}

	void UpdateImage (int pos, int player)
	{
		//Array of the actual image components (player + podium)
		Image[] imageArr = images [pos].GetComponentsInChildren <Image> ();

		//Set it to the color of the player
		foreach (Image image in imageArr) {
			image.color = playerColors [player];
		}
	}

	Dictionary<int, int> GetScores ()
	{
		//Get scores from gamecontrol and update text
		string text = "";

		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {
			text += "Player " + (i + 1) + ": " + GameControl.instance.playerScores [i] + "\n";
		}

		//First int = player num
		//Second int = player score
		//Ordered from highest to lowest
		Dictionary<int, int> orderedScores = new Dictionary<int, int> ();
		int[] tempScores = GameControl.instance.playerScores;

		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {
			int highest = -1;
			int position = -1;

			for (int j = 0; j < GameControl.instance.numOfPlayers; j++) {
				if (tempScores [j] > highest) {
					highest = tempScores [j];
					position = j;
				}
			}
			orderedScores.Add (position, highest);

			tempScores [position] = -1;
		}

		return orderedScores;
	}
}
