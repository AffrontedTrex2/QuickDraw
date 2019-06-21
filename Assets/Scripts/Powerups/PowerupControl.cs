using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupControl : MonoBehaviour
{
	public GameObject quickdrawObj;

	//How many games have elapsed since the last time a powerup was given
	//public int gamesSinceLastPowerup = 1;
	//Amount of time to wait before giving a powerup
	private float timeBeforePowerup = -1;
	private bool givePowerup = false;

	public bool animationFinished = false;

	PowerupInfo info;

	//Reference to ui
	PowerupUI ui;

	//Reference to the four images of the players on the powerup ui screen after someone takes a powerup
	PowerupUIImage uiImage;

	//Reference to GetPowerup, which has script for choosing who gets powerup
	GetPowerup getPowerup;

	void Awake ()
	{
		//DontDestroyOnLoad (this);
	}

	void Start ()
	{
		info = GetComponent <PowerupInfo> ();
		ui = GetComponent <PowerupUI> ();
		getPowerup = GetComponent <GetPowerup> ();

		uiImage = ui.powerupUI.GetComponent<PowerupUIImage> ();

		quickdrawObj.SetActive (false);

		CheckForNextPowerup ();
	}

	void Update ()
	{
		if (givePowerup) {
			timeBeforePowerup -= Time.deltaTime;
		}

		//If timer is less than 0
		if (givePowerup && timeBeforePowerup < 0) {
			givePowerup = false;

			//First check if the game is over; if it is, don't give the powerup
			if (GameControl.instance.minigameOver) {
				return;
			}

			//Set it to 0 after youve gotten the powerup
			GameControl.instance.gamesSinceLastPowerup = 0;

			//Undo the previous powerup
			if (GameControl.instance.activePowerup != null) {
				UndoPowerup ();
			}

			ChoosePowerup ();
		}
	}

	//Undo previous powerup using gamecontrol info
	void UndoPowerup ()
	{
		//If nobody got the power up last time, there's nothing to undo
		if (GameControl.instance.powerupTargetNum == -1) {
			return;
		}

		string function = "Undo" + GameControl.instance.activePowerup.function;
		info.Invoke (function, 0f);
	}

	//Activate previous powerup after scene has changed, called by gamecontrol
	public void ActivatePowerup ()
	{
		if (info == null) {
			info = GetComponent <PowerupInfo> ();
		}

		//If nobody got the powerup last time, don't run this
		if (GameControl.instance.powerupTargetNum == -1) {
			return;
		}

		info.Invoke (GameControl.instance.activePowerup.function, 0f);
	}

	public void DeactivatePowerup ()
	{
		info.Invoke ("Undo" + GameControl.instance.activePowerup.function, 0f);
	}

	//Check how soon the next powerup should be given
	void CheckForNextPowerup ()
	{
		//If minigame is over, don't set next powerup
		/*if (GameControl.instance.minigameOver) {
			return;
		}*/

		//If there have been three or more games, immediately give powerup
		if (GameControl.instance.gamesSinceLastPowerup >= 2) {
			timeBeforePowerup = Random.Range (2, 4);

			givePowerup = true;
		}

		//If there have been two games, give powerup soon
		if (GameControl.instance.gamesSinceLastPowerup >= 1) {
			//50 percent change of getting powerup
			if (Random.Range (0, 1) == 0) {
				timeBeforePowerup = Random.Range (4, 10);

				Debug.Log ("time: " + timeBeforePowerup);

				givePowerup = true;
			}
		}

		//30 percent chance to get a new powerup
		if (GameControl.instance.gamesSinceLastPowerup == 0) {
			//If you get the fifty-fifty
			if (Random.Range (0, 3) == 0) {
				timeBeforePowerup = Random.Range (4, 16);

				Debug.Log ("time: " + timeBeforePowerup);

				givePowerup = true;
			}
		}
	}

	void ChoosePowerup ()
	{
		//Randomly choose powerup from list
		GameControl.instance.activePowerup = GetRandomPowerup ();

		//Run the animation
		quickdrawObj.SetActive (true);
		quickdrawObj.GetComponent<Animator> ().SetTrigger ("quickdraw");

		//Don't allow anyone to pause
		GameControl.instance.pause_powerup = true;

		//Wait for input/players to take the powerup
		//Also update powerup info and and effect
		StartCoroutine (StealAndUpdatePowerup ());
	}

	void ActivatePowerup (GameObject winner)
	{
		DisplayPowerupInfo (winner);

		//Active powerup
		info.Invoke (GameControl.instance.activePowerup.function, 0f);
	}

	void DisplayPowerupInfo (GameObject winner)
	{
		//After you yoink the powerup, display what it actually does
		//Wait for input before continuing
		ui.ShowPowerupUI ();
		ui.UpdateWinner (winner);
		ui.UpdateIcon (GameControl.instance.activePowerup.icon);
		ui.UpdateName (GameControl.instance.activePowerup.name);
		ui.UpdateDescription (GameControl.instance.activePowerup.description);
		ui.ShowButton ();

		//Set the winner images
		uiImage.SetImages (winner);

		StartCoroutine (WaitForWinnerInput (winner));
	}

	//Returns who got the powerup
	IEnumerator StealAndUpdatePowerup ()
	{
		//Wait for the animation to finish before starting the powerup getting process
		while (!animationFinished) {
			yield return null;
		}

		//Reset animationfinished
		animationFinished = false;

		//Randomly choose a way for players to get powerup
		GameObject winner;

		//winner = getPowerup.ChoosePowerupWinner ();
		CoroutineWithData cd = new CoroutineWithData (this, getPowerup.ChoosePowerupWinner (GameControl.instance.activePowerup));
		yield return cd.coroutine;

		winner = (GameObject)cd.result;

		//If the winner is null, because nobody chose a powerup, just close the uis
		if (winner != null) {
			//Set powerup target
			//powerup.target = winner;
			//info.target = winner;
			GameControl.instance.UpdatePowerupTarget (winner);

			//Active the power and display the powerup description
			ActivatePowerup (winner);
		} else {
			NobodyWantsPowerup ();
		}
	}

	//Is called if nobody takes the powerup
	void NobodyWantsPowerup ()
	{
		//Update powerup target will null
		GameControl.instance.UpdatePowerupTarget (null);

		ui.ClearAndHideGetPowerupUI ();
		ui.ClearAndHidePowerupUI ();
	}

	//Returns random powerup that's not the active powerup
	Powerup GetRandomPowerup ()
	{
		int num = Random.Range (0, info.powerups.Count);
		Powerup temp = info.powerups [num];

		//For testing purposes, if there's only one powerup, return that
		if (info.powerups.Count == 1) {
			return temp;
		}

		while (temp == GameControl.instance.activePowerup) {
			num = Random.Range (0, info.powerups.Count);
			temp = info.powerups [num];
		}

		return temp;
	}

	//Waits until the player who got the powerup presses A
	//And after they press A, the powerup info screen will close
	IEnumerator WaitForWinnerInput (GameObject winner)
	{
		string interact = "Interact" + winner.GetComponent<PlayerPlatformerController> ().playerNum;

		//Wait for player to press interact
		while (true) {
			//Use input here instead of InputBroker so that closing this ui can't be forced
			if (Input.GetButtonDown (interact)) {
				//Deactivate UI
				ui.ClearAndHidePowerupUI ();

				//Allow pausing again
				GameControl.instance.pause_powerup = false;

				break;
			}

			yield return null;
		}
	}
}
