using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupUI : MonoBehaviour
{
	[Header ("PowerupUI")]
	public GameObject powerupUI;

	public Image icon;
	public Text winnerText;
	public Text nameText;
	public Text descriptionText;
	private Button button;

	[Header ("GetPowerupUI")]
	public GameObject getPowerupUI;

	public Image getIcon;
	public Text getText;


	void Start ()
	{
		button = powerupUI.GetComponentInChildren <Button> ();

		ClearAndHidePowerupUI ();
		ClearAndHideGetPowerupUI ();

		//But pause right after
		Time.timeScale = 0;
	}
		
	//---------------
	//POWERUP UI
	//---------------

	public void ShowPowerupUI ()
	{
		powerupUI.SetActive (true);

		button.enabled = true;

		//Pause game
		Time.timeScale = 0;
	}

	//Clears icon, name, and description, and sets inactive
	public void ClearAndHidePowerupUI ()
	{
		icon.sprite = null;
		nameText.text = "";
		descriptionText.text = "";
		winnerText.text = "";

		button.enabled = false;

		powerupUI.SetActive (false);

		//Resume game
		Time.timeScale = 1;
	}

	public void UpdateIcon (Sprite sprite)
	{
		icon.sprite = sprite;
	}

	public void UpdateName (string name)
	{
		nameText.text = name;
	}

	public void UpdateDescription (string description)
	{
		descriptionText.text = description;
	}

	public void ShowButton ()
	{
		button.enabled = true;
	}

	public void UpdateWinner (GameObject winner)
	{
		string text = "null";
		int winnerNum = -1;

		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {
			if (winner == GameControl.instance.players [i]) {
				winnerNum = i;
				break;
			}
		}

		text = "Player " + (winnerNum + 1) + "'s upgrade";

		winnerText.text = text;
	}

	//-----------------
	//GET POWERUP UI
	//-----------------

	public void ShowGetPowerupUI ()
	{
		getPowerupUI.SetActive (true);

		//Pause game
		Time.timeScale = 0;
	}

	public void ClearAndHideGetPowerupUI ()
	{
		getIcon.sprite = null;
		getText.text = "";

		getPowerupUI.SetActive (false);

		//Resume game
		//GameControl.instance.Resume ();
	}

	public void UpdateGetText (string text)
	{
		getText.text = text;
	}

	public void UpdateGetIcon (Sprite sprite)
	{
		getIcon.sprite = sprite;
	}
}
