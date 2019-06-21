using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
	public GameObject[] playerSelectSpaceObj;
	public GameObject confirmUI;

	private PlayerSelectSpace[] playerSelectSpace;

	private bool confirmUIActive = false;
	private bool coroutineStarted = false;
	private float timer;

    private int numOfPlayers = 0;

	private SceneManagement sceneManager;

	void Start ()
	{
		sceneManager = GetComponent <SceneManagement> ();

		confirmUI.SetActive (false);

		//Put all the select space scripts into array
		playerSelectSpace = new PlayerSelectSpace[4];
		for (int i = 0; i < playerSelectSpaceObj.Length; i++) {
			playerSelectSpace [i] = playerSelectSpaceObj [i].GetComponent<PlayerSelectSpace> ();
		}

		timer = 1f;
		CheckForNewConnection ();

		//Run check function every two seconds
		//InvokeRepeating ("CheckForNewConnection", 0, 2f);
	}

	void Update ()
	{
		//If the UI is active
		//Wait for player 1 to enter input
		if (confirmUIActive) {

			if (!coroutineStarted) {
				StartCoroutine (WaitForInput ());

				//So that you don't run this coroutine many times
				coroutineStarted = true;
			}
		}

		//Check for new connection every two seconds
		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			CheckForNewConnection ();
			timer = 1f;
		}

		//If player1 presses A and there are at least two players
		if (Input.GetButtonDown ("Interact1") && GetNumOfPlayers () >= 2) {
			confirmUIActive = true;

			//Open up confirm UI
			confirmUI.SetActive (true);
		}

        //If you press cancel while there aren't two players, go back to selection screen
        if (Input.GetButtonDown("Cancel") && GetNumOfPlayers() < 2) {
            sceneManager.KeyboardSelect();
        }
	}

	IEnumerator WaitForInput ()
	{
		//Wait for player to press interact
		while (true) {
			//Use input here instead of InputBroker so that closing this ui can't be forced
			if (Input.GetButtonDown ("Interact1")) {

				//Set number of players in gamecontrol
				GameControl.instance.numOfPlayers = GetNumOfPlayers ();

				//Also set usingJoysticks for gamecontrol to true
				GameControl.instance.usingJoysticks = true;

				//Play next game
				LoadNextMinigame ();

				break;
			}

			//If you press B, go back to player selection
			if (Input.GetButtonDown ("Cancel")) {
				confirmUIActive = false;
				confirmUI.SetActive (false);

				coroutineStarted = false;

				break;
			}

			yield return null;
		}
	}

	void LoadNextMinigame ()
	{
		//Get random minigame
		string minigame = ChooseRandomMinigame ();

		//Load it
		sceneManager.Invoke (minigame, 0f);

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

	//Returns number of controllers attached
	int GetNumOfPlayers ()
	{
		int numOfPlayers = 0;

		string[] temp = Input.GetJoystickNames ();

		for (int i = 0; i < playerSelectSpaceObj.Length; i++) {

			//If there's a controller attached
			if (i <= (temp.Length - 1) && !string.IsNullOrEmpty (temp [i])) {

				numOfPlayers++;
			}
		}

		return numOfPlayers;
	}

    //Checks for a new joycon connection
    void CheckForNewConnection() {
        bool allPlayersFound = false;
        //numOfPlayers = 0;

        string[] temp = Input.GetJoystickNames();

		for (int i = 0; i < playerSelectSpaceObj.Length; i++) {

			//If there's a controller attached
			if (i <= (temp.Length - 1) && !string.IsNullOrEmpty (temp [i]) && !allPlayersFound) {

                //Update the text of the corresponding select space object
                playerSelectSpace [i].Connect (i + 1);
                //numOfPlayers++;
			} else {

				//Not connected
				playerSelectSpace [i].Disconnect ();

				allPlayersFound = true;
			}
		}

        //Connect();
	}

    void Connect() {
        for (int i = 0; i < playerSelectSpace.Length; i++) {
            if (i < numOfPlayers) {
                playerSelectSpace[i].Connect(i + 1);
            } else {
                playerSelectSpace[i].Disconnect();
            }
        }
    }
}
