using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Encompasses everything on the KeyboardSelect scene
public class KeyboardSelect : MonoBehaviour
{
	public GameObject[] controlInfo;
	public GameObject chooseTypeUI;
	public GameObject chooseNumberUI;

	private SceneManagement sceneManager;

	private bool showingControls = false;
	private bool[] playersReady;

	void Start ()
	{
		sceneManager = GetComponent <SceneManagement> ();
	}

	void Update ()
	{
		//Only run this if the controlUI is showing
		if (showingControls) {
			//First check if everyone is ready
			if (PlayersReady ()) {
				LoadNextMinigame ();
			}

			//For every player. check for an input
			for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {
				string interact = "Interact" + (i + 1);

				//If you confirm, then the panel background becomes green
				if (Input.GetButtonDown (interact)) {
					controlInfo [i].GetComponent<Image> ().color = Color.green;

					//That player is ready to play
					playersReady [i] = true;
				}
			}
		}

        //If you pressed the back button, go back
        if (Input.GetButtonDown("Cancel")) {
            Back();
        }
	}

	//Called by back button
	public void Back ()
	{
		if (!showingControls) {
			//If type selection is open, load main menu
			if (chooseTypeUI.activeSelf) {
				sceneManager.MainMenu ();
			} else {
				//Else open up type selection
				chooseTypeUI.SetActive (true);
			}
		} else {
			//If you are showing controls, reopen number select ui
			chooseNumberUI.SetActive (true);

			//and set showing to false
			showingControls = false;

			//Change the colors of the panels back to what they originally were
			foreach (GameObject panel in controlInfo) {
				panel.GetComponent<Image> ().color = Color.white;
			}
		}
	}

	bool PlayersReady ()
	{
		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {
			if (!playersReady [i]) {
				return false;
			}
		}

		return true;
	}

	void LoadNextMinigame ()
	{
		//Get random minigame
		string minigame = ChooseRandomMinigame ();

		//Load it
		sceneManager.Invoke (minigame, 0f);

        //Play new music
        SoundManager.instance.PlayBackground();
	}

	string ChooseRandomMinigame ()
	{
		int index;

		if (GameControl.instance.numOfPlayers > 2) {
			index = Random.Range (0, GameInformation.minigames.Count);
			return GameInformation.minigames [index];
		}

		index = Random.Range (0, GameInformation.minigamesTwoPlayer.Count);
		return GameInformation.minigamesTwoPlayer [index];
	}

	void ShowControls ()
	{
		//Reset playersReady
		playersReady = new bool[4];

		showingControls = true;
		chooseNumberUI.SetActive (false);

		//Only enable the info panels corresponding with the number of players
		for (int i = (GameControl.instance.numOfPlayers - 1); i < controlInfo.Length; i++) {
			
			//Players not playing will have their CI discolored
			Color color = controlInfo [i].GetComponent<Image> ().color;
			color.a = .2f;
			controlInfo [i].GetComponent<Image> ().color = color;
		}

		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {

			//Players playing will have their CI opaque
			Color color = controlInfo [i].GetComponent<Image> ().color;
			color.a = 1f;
			controlInfo [i].GetComponent<Image> ().color = color;
		}
	}

	//----------------------------------------
	//Button calls for chooseTypeUI
	//----------------------------------------

	public void ChooseController ()
	{
		sceneManager.PlayerSelect ();
	}

	public void ChooseKeyboard ()
	{
		chooseTypeUI.SetActive (false);
	}

	//----------------------------------------
	//Button calls for chooseNumberUI
	//----------------------------------------

	public void TwoPlayer ()
	{
		GameControl.instance.numOfPlayers = 2;
		ShowControls ();
	}

	public void ThreePlayer ()
	{
		GameControl.instance.numOfPlayers = 3;
		ShowControls ();
	}

	public void FourPlayer ()
	{
		GameControl.instance.numOfPlayers = 4;
		ShowControls ();
	}

}
