using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBarUI : MonoBehaviour
{
	public Image[] scoreBars;
	public Image background;

	private RectTransform[] scoreTransforms;
	private float maxWidth;

	void Start ()
	{
		//Disable extra bars
		/*for (int i = GameControl.instance.numOfPlayers; i < scoreBars.Length; i++) {
			scoreBars [i].enabled = false;
		}*/

		//Add enabled scorebars to scoreTransforms
		scoreTransforms = new RectTransform[GameControl.instance.numOfPlayers];
		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {
			scoreTransforms [i] = scoreBars [i].GetComponent <RectTransform> ();
		}

		//Set maxwidth to the full length of the bar
		maxWidth = scoreTransforms [0].rect.width;

		UpdateBars (new int[]{ 1, 1, 1, 1 });

        ShowBars();
	}

	public void HideBars ()
	{
		for (int i = 0; i < scoreBars.Length; i++) {
			scoreBars [i].enabled = false;
		}

		background.enabled = false;
	}

	public void ShowBars ()
	{
		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {
			scoreBars [i].enabled = true;
		}
        for (int i = GameControl.instance.numOfPlayers; i < 4; i++) {
            scoreBars[i].enabled = false;
        }

		background.enabled = true;
	}

	//Update the bars according to what score each player has
	public void UpdateBars (int[] score)
	{
		//Get total score for calculating percentages
		int totalScore = 0;
		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {
			totalScore += score [i];
		}

		//Make sure it starts with an equal split, and that TS isn't 0
		totalScore = Mathf.Max (totalScore, GameControl.instance.numOfPlayers);

		//For each player's score bar
		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {
			//If you have no points, still give you one
			if (score [i] == 0) {
				score [i] = 1;
			}

			//Set their score bar size according to their % score
			//scoreBars [i].fillAmount = score [i] * 1.0f / totalScore;
			float width = score [i] * 1.0f / totalScore * maxWidth;
			//scoreTransforms [i].rect.Set (scoreTransforms [i].rect.x, scoreTransforms [i].rect.y, width, scoreTransforms [i].rect.height);

			scoreTransforms [i].sizeDelta = new Vector2 (width, scoreTransforms [i].rect.height);

			if (i == 0) {
				continue;
			}

			//If this isn't player one's bar, adjust the x position of the bar according to the previous bar
			float x = scoreTransforms [i - 1].localPosition.x;
			x += scoreTransforms [i - 1].rect.width;

			scoreTransforms [i].localPosition = new Vector2 (x, scoreTransforms [i].localPosition.y);
		}
	}
}
