using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	//Time before bomb explodes
	float timer;

	//First the time bomb is passed (intial time when random player is given bomb)
	bool firstTime = true;

	Animator animator;

	void Start ()
	{
		animator = GetComponent <Animator> ();

		/*GiveBombToRandomPlayer ();

		//Set timer to random number
		SetTimer ();*/
	}

	void Update ()
	{
		//if It's the first time, give bomb to random player and set timer
		if (firstTime) {
			GiveBombToRandomPlayer ();

			SetTimer ();
		}

		//Don't update if game is over
		if (GameControl.instance.minigameOver) {
			return;
		}

		timer -= Time.deltaTime;

		if (timer < 3) {
			animator.SetBool ("Flash", true);
		}

		if (timer < 0) {
			//Bomb explodes, kills player
			Explode ();
		}
	}

	void Explode ()
	{
		//GetComponentInParent <PlayerPlatformerController> ().Die ();

		animator.SetTrigger ("Explode");
		animator.SetBool ("Flash", false);

		//GameControl.instance.CheckAndEndMinigame ();

		GameControl.instance.KillPlayer (transform.parent.gameObject);

		//Check if game is over
		if (!GameControl.instance.minigameOver) {
			//Reset timer
			SetTimer ();

			//Give bomb to random player
			GiveBombToRandomPlayer ();
		}
	}

	void SetTimer ()
	{
		timer = Random.Range (10, 15);
	}

	void GiveBombToRandomPlayer ()
	{
		//Get random player
		int player = Random.Range (0, GameControl.instance.numOfPlayers);

		//Find a player who is still alive
		GameObject target = GameControl.instance.players [player];

		while (!target.GetComponent<PlayerPlatformerController> ().isAlive) {
			player = Random.Range (0, GameControl.instance.numOfPlayers);
			target = GameControl.instance.players [player];
		}

		//Give target the bomb
		TransferBomb (target);
	}

	void TransferBomb (GameObject player)
	{
		//Save who is holding the bomb right now
		GameObject prevPlayer = transform.parent.gameObject;

		//Remove bombcollision from prevPlayer
		prevPlayer.GetComponent<BombCollision> ().enabled = false;

		//Move bomb to other player
		transform.SetParent (player.transform);
		transform.position = player.transform.position;

		//Enable bombcollision on currentPlayer
		player.GetComponent<BombCollision> ().enabled = true;

		//Increase player speed
		IncreasePlayerSpeed (player);

		if (firstTime) {
			firstTime = false;
			return;
		}

		//Decrease prev player's speed
		DecreasePlayerSpeed (prevPlayer);
	}

	void IncreasePlayerSpeed (GameObject player)
	{
		player.GetComponent<PlayerPlatformerController> ().maxSpeed *= 1.3f;
	}

	void DecreasePlayerSpeed (GameObject player)
	{
		player.GetComponent<PlayerPlatformerController> ().maxSpeed *= (10f / 13f);
	}
}
