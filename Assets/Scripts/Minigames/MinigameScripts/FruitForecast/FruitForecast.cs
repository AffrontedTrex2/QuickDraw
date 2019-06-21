using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitForecast : MonoBehaviour
{
	public GameObject scoreBar;
	private ScoreBarUI scoreBarUI;

	//What will spawn from the top
	public GameObject fruit;
	public GameObject spikeball;

	//How long the minigame will be
	public float time = 30f;

	//Storing player scores
	public int[] scores;

	float spawnSpeed = .5f;
	float spawnTimer = .5f;

	void Start ()
	{
		scores = new int[GameControl.instance.numOfPlayers];

		scoreBarUI = scoreBar.GetComponent<ScoreBarUI> ();
		scoreBarUI.ShowBars ();
	}

	void Update ()
	{
		if (GameControl.instance.minigameOver) {
			return;
		}

		//Timer for ending the game
		if (time <= 3) {
			SetTimerText ();
		}

		//End the game if time is over
		if (time < 0) {
			GameControl.instance.EndMinigame (scores);
		}

		time -= Time.deltaTime;

		//If spawn timer is zero, spawn fruit
		if (spawnTimer > 0) {
			spawnTimer -= Time.deltaTime;
		} else {
			Spawn ();
			spawnTimer = spawnSpeed;
		}
	}

	void SetTimerText ()
	{
		int setTime = 0;

		if (time <= 3) {
			setTime = 3;
		}
		if (time <= 2) {
			setTime = 2;
		}
		if (time <= 1) {
			setTime = 1;
		}
		if (time <= 0.05f) {
			setTime = 0;
		}

		GameControl.instance.SetTimerText (setTime);
	}

	void Spawn ()
	{
		GameObject spawnedObj;
		//1/4 change to spawn spikeball
		if (Random.Range (0, 3) == 0) {
			spawnedObj = Instantiate (spikeball);

			//Set the object's reference to this script
			spawnedObj.GetComponent<FallingSpikeball> ().fruitForecast = this;
		} else {
			spawnedObj = Instantiate (fruit);
			spawnedObj.GetComponent<Fruit> ().fruitForecast = this;
		}

		//Randomize block x position
		float x = Random.Range (.05f, .95f);

		//Set block position
		spawnedObj.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (x, 1f, 0f));

		//Set z to 0
		Vector3 pos = spawnedObj.transform.position;
		pos.z = 1;
		spawnedObj.transform.position = pos;
	}

	//Called by fruit to increase score
	public void AddPoint (int player)
	{
		scores [player - 1]++;

		//Also update bar
		scoreBarUI.UpdateBars (scores);
	}
}
