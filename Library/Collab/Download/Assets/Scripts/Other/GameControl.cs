using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
	//Number of points you need to win
	private int winPoints = 5;

	public bool minigameOver = false;
	public bool usingJoysticks = false;

	//Variables for calculating if you are able to pause
	[Header ("Pausing")]
	public bool isPaused = false;
	public bool pause_powerup = false;
	public bool pause_infoscreen = false;
	public GameObject pauseUI;

	public GameObject[] players;
	public int numOfPlayers = 2;

	public int[] playerScores;
	public Color[] playerColors;

	public static GameControl instance = null;

	/*	public GameObject uiObj;
	public GameObject powerupControlObj;*/

	UI ui;
	PowerupControl powerupControl;

	SceneManagement sceneManagement;

	//Used to keep track of who was powerup between minigames
	//0-3
	[HideInInspector] public int powerupTargetNum;
	[HideInInspector] public Powerup activePowerup;
	[HideInInspector] public int gamesSinceLastPowerup = 1;

	/*public int health = 10;
	public int score = 0;
	public int exp = 0;*/

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (this);
	}

	void Start ()
	{
		Init ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape) ||
		    Input.GetButtonDown ("Start1Joystick") ||
		    Input.GetButtonDown ("Start2Joystick") ||
		    Input.GetButtonDown ("Start3Joystick") ||
		    Input.GetButtonDown ("Start4Joystick")) {
			TryTogglePause ();
		}

        //If you press 6, automatically end the minigame with player 1 as the winner
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            EndMinigame(0);
        }
	}

	public void Init ()
	{
		sceneManagement = GetComponent <SceneManagement> ();

		playerScores = new int[4];

		playerColors = new Color[]{ Color.white, Color.red, Color.green, Color.yellow };

		powerupTargetNum = -1;
		activePowerup = null;
		gamesSinceLastPowerup = 1;

		minigameOver = false;
		usingJoysticks = false;

		isPaused = false;
		pause_powerup = false;
		pause_infoscreen = false;
	}

	//Called by pauseUI, returns you to main menu
	public void MainMenu ()
	{
		Init ();
		sceneManagement.MainMenu ();
	}

	//Makes sure you aren't pausing/resuming during an info screen or something
	void TryTogglePause ()
	{
		bool canToggle;
		canToggle = !pause_powerup && !pause_infoscreen;

		//Make sure current screen is not one of the screens that you can't pause in
		canToggle = canToggle && !GameInformation.cannotPauseScreens.Contains (SceneManager.GetActiveScene ().name);

		if (canToggle) {
			if (isPaused) {
				Resume ();
			} else {
				Pause ();
			}
		}
	}

	//Called when minigames begin
	public void StartMinigame ()
	{
		minigameOver = false;

		//Find and attach players
		//FindPlayers ();

		//Also find ui and powerupcontrol
		ui = GameObject.Find ("UI").GetComponent<UI> ();
		//pauseUI = GameObject.Find ("PauseUI");
		powerupControl = GameObject.Find ("PowerupControl").GetComponent<PowerupControl> ();

        //Find scoreBar
        ScoreBarUI scoreBar = ui.GetComponentInChildren<ScoreBarUI>();
        //If the minigame isn't fruit forecast or color craze, disable it
        if (!SceneManager.GetActiveScene().name.Equals(GameInformation.fruitForecast) &&
            !SceneManager.GetActiveScene().name.Equals(GameInformation.colorCraze)) {
            scoreBar.HideBars();
        }

		//Apply previous powerup
		if (activePowerup != null) {
			powerupControl.ActivatePowerup ();
		}
	}

	//Find players in scene and attach them
	public void FindPlayers ()
	{
		for (int i = 0; i < players.Length; i++) {
			string name = "Player" + (i + 1);
			GameObject player = GameObject.Find (name);

			//If this current player number is more than the number of players playing
			//Destroy that player object
			if (i + 1 > numOfPlayers) {
				Destroy (player);
				continue;
			}

			//If this player should be playing
			//Add it to the array
			if (player != null) {
				players [i] = player;

				//Also set player num
				player.GetComponent<PlayerPlatformerController> ().SetPlayerNum ();
			}
		}

        //If the minigame is Color craze, init the color craze script
        if (SceneManager.GetActiveScene().name.Equals(GameInformation.colorCraze)) {
            GameObject.Find("MinigameControl").GetComponent<ColorCraze>().Init();
        }
	}

	public void KillPlayer (GameObject player)
	{
		player.GetComponent <PlayerPlatformerController> ().Die ();

		CheckAndEndMinigame ();
	}

	//Called by powerupcontrol
	//Updates who's the target of the powerup
	public void UpdatePowerupTarget (GameObject winner)
	{
		//If there's no winner, return -1
		if (winner == null) {
			powerupTargetNum = -1;
			return;
		}

		for (int i = 0; i < numOfPlayers; i++) {
			if (winner == players [i]) {
				powerupTargetNum = i;
				return;
			}
		}
	}

	//Called by powerupinfo
	//To get who the target is
	public GameObject GetTarget ()
	{
		return players [powerupTargetNum];
	}

	public void DestroyObject (GameObject obj)
	{
		Destroy (obj);
	}

	public void UpdateGameText (string text)
	{
		ui.UpdateGameText (text);
	}

	public void SetTimerText (int num)
	{
		ui.SetTimerText (num);
	}

	public void Pause ()
	{
		Time.timeScale = 0;
		isPaused = true;

		pauseUI.SetActive (true);
	}

	public void Resume ()
	{
		Time.timeScale = 1;
		isPaused = false;

		pauseUI.SetActive (false);
	}

	public void CheckAndEndMinigame ()
	{
		if (IsGameOver ()) {
			EndMinigame (GetAlivePlayer ());
		}
	}

	int GetAlivePlayer ()
	{
		//Finds and returns player number of who is still alive
		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {
			if (GameControl.instance.players [i].GetComponent<PlayerPlatformerController> ().isAlive) {
				return i;
			}
		}

		return 0;
	}

	public bool IsGameOver ()
	{
		int alive = 0;

		//Checks if everyone is still alive
		//Returns true if everyone except one is dead
		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {
			if (GameControl.instance.players [i].GetComponent<PlayerPlatformerController> ().isAlive) {
				alive++;
			}
		}

		return alive == 1;
	}

	//Called when any minigame ends
	//Takes player num of winner, 0-3
	public void EndMinigame (int winner)
	{
		minigameOver = true;

		//Update winner score
		playerScores [winner]++;

		//Update UI
		string text = "Winner: Player " + (winner + 1);
		UpdateGameText (text);

		//Add one to games elapsed
		//powerupControl.gamesSinceLastPowerup++;
		gamesSinceLastPowerup++;

		//Undo powerups on target
		/*if (activePowerup != null) {
			powerupControl.DeactivatePowerup ();
		}*/

		//Fade for a few seconds, then call scenemanager
		StartCoroutine (FadeToScoreScreen ());
	}

	//Finds winner
	//Prints points
	public void EndMinigame (int[] points)
	{
		minigameOver = true;

		string text = "";

		//Cycle through and print points
		for (int i = 0; i < points.Length; i++) {
			text += "Player " + (i + 1) + ": " + points [i] + "\n";
		}

		List<int> winner = FindWinner (points);

		foreach (int player in winner) {
			//Update winner score
			playerScores [player]++;
		}

		//Update UI
		text += "Winner:";
		foreach (int player in winner) {
			text += "\nPlayer " + (player + 1);
		}

		UpdateGameText (text);

		//Add one to games elapsed
		//powerupControl.gamesSinceLastPowerup++;
		gamesSinceLastPowerup++;

		//Fade for a few seconds, then call scenemanager
		StartCoroutine (FadeToScoreScreen ());
	}

	//TODO add fade to black
	IEnumerator FadeToScoreScreen ()
	{
		//Fade for 3 seconds
		yield return new WaitForSeconds (3f);

		bool gameOver = false;
		for (int i = 0; i < numOfPlayers; i++) {
			//If someone has >= the amount of points to win, go to win screen
			if (playerScores [i] >= winPoints) {
				gameOver = true;
				sceneManagement.WinScreen ();
			}
		}

		if (!gameOver) {
			//Change scene to score screen if nobody has won yet
			sceneManagement.ScoreScreen ();
		}
	}

	List<int> FindWinner (int[] points)
	{
		int most = -1;

		//Find what number is the largest
		for (int i = 0; i < points.Length; i++) {
			if (points [i] > most) {
				most = points [i];
			}
		}

		//If that posiition contains the largest, add the position (and therefore player num) to the list
		List<int> winner = new List<int> ();
		for (int i = 0; i < points.Length; i++) {
			if (points [i] == most) {
				winner.Add (i);
			}
		}

		//Update score
		//UpdateScore (winner);

		return winner;
	}

	//Used by sugar rush to find out how many candies to summon
	public int GetNumberOfPlayersAlive ()
	{
		int num = 0;

		for (int i = 0; i < numOfPlayers; i++) {
			if (players [i].GetComponent<PlayerPlatformerController> ().isAlive) {
				num++;
			}
		}

		return num;
	}

	/*public void changeHealth (int change)
	{
		health += change;
		UIScript.changeHealth (health);
	}

	public void changeScore (int change)
	{
		score += change;
		UIScript.changeScore (score);
	}

	public void changeExp (int change)
	{
		exp += change;
		UIScript.changeExp (exp);
	}*/
}
