using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
	public Text gameText;
	public Text timerText;

	public void UpdateGameText (string text)
	{
		gameText.text = text;
	}

	//Start counting down timer text
	public void SetTimerText (int num)
	{
		timerText.text = num.ToString ();

		if (num == 0) {
			timerText.text = "";
		}
	}

	/*public void changeHealth (int health)
	{
		healthText.text = "Health: " + health;
	}

	public void changeScore (int score)
	{
		scoreText.text = "Score: " + score;
	}

	public void changeExp (int exp)
	{
		expText.text = "EXP: " + exp;
	}*/
}
