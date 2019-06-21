using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages getting powerup
public class GetPowerup : MonoBehaviour
{
	//Ways to get the powerup
	public List<IEnumerator> getPowerupMethods;

	[HideInInspector]
	public GameObject winner;

	//Will wait until the quickdraw animation finished before continuing
	/*[HideInInspector]
	public GameObject animationFinished = false;*/

	private PowerupUI powerupUI;

	void Start ()
	{
		Init ();

		powerupUI = GetComponent <PowerupUI> ();
	}

	void Init ()
	{
		getPowerupMethods = new List<IEnumerator> ();

		getPowerupMethods.Add (PressButton ());
	}

	//Returns who gets the powerup
	public IEnumerator ChoosePowerupWinner (Powerup powerup)
	{
		//First set winner to null
		winner = null;

		//Choose method to use
		IEnumerator method = ChooseMethod ();

		//Update UI with icon and text of method
		UpdateGetUI (powerup, method);

		//Invoke method
		yield return StartCoroutine (method);

		//After there's a winner, hide the ui
		powerupUI.ClearAndHideGetPowerupUI ();

		//return winner
		yield return winner;
	}

	//Returns string of which method to use
	IEnumerator ChooseMethod ()
	{
		int index = Random.Range (0, getPowerupMethods.Count);
		return getPowerupMethods [index];
	}

	void UpdateGetUI (Powerup powerup, IEnumerator method)
	{
		//Set UI as active
		powerupUI.ShowGetPowerupUI ();

		powerupUI.UpdateGetIcon (powerup.icon);

		string text = "null";
		if (method.ToString ().Equals (PressButton ().ToString ())) {
            //text = "Player 1 press space, player 2 press /";
            text = "Player INTERACT to get the powerup!";
        }

		powerupUI.UpdateGetText (text);
	}

	//-------------------
	//DIFFERENT METHODS
	//-------------------

	IEnumerator PressButton ()
	{
		float timer = 3f;

		//Get winner
		while (winner == null) {
			if (InputBroker.GetButtonDown ("Interact1")) {
				winner = GameControl.instance.players [0];
			} else if (InputBroker.GetButtonDown ("Interact2")) {
				winner = GameControl.instance.players [1];
			} else if (InputBroker.GetButtonDown ("Interact3")) {
				winner = GameControl.instance.players [2];
			} else if (InputBroker.GetButtonDown ("Interact4")) {
				winner = GameControl.instance.players [3];
			}

			//Subtract from the timer
			timer -= Time.unscaledDeltaTime;

			//If the time has run down, nobody has won
			if (timer <= 0) {
				break;
			}

			yield return null;
		}
	}
}
