using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScare : MonoBehaviour
{
	//Prefab for particle system
	public GameObject ghostParticles;

	ParticleSystem ps;
	SpriteRenderer sprite;
	Animator animator;

	PlayerPlatformerController controller;

	//How far away you can scare others
	float scareDist = 2f;

	bool stunned = false;

	Vector3 prevPos;

	void Start ()
	{
		animator = GetComponent <Animator> ();
		controller = GetComponent <PlayerPlatformerController> ();

		//Hide player
		sprite = GetComponent <SpriteRenderer> ();
		sprite.enabled = false;

		//Create ghostParticles as a child
		GameObject particles = Instantiate (ghostParticles, transform);

		ps = particles.GetComponent<ParticleSystem> ();
		DisableParticles ();

		prevPos = transform.position;
	}

	void Update ()
	{
		if (stunned || !controller.isAlive || GameControl.instance.minigameOver) {
			return;
		}

		if (Moved ()) {
			EnableParticles ();

			prevPos = transform.position;
		} else {
			DisableParticles ();
		}


		//If down is pressed and you're on the ground
		string vertical = "Vertical" + controller.playerNum;

		//Add check for joystick users
		if (GameControl.instance.usingJoysticks) {
			vertical += "Joystick";
		}

		if (Input.GetAxis (vertical) < 0 && controller.IsGrounded ()) {
			//Scare opponents
			Scare ();
		} else {
			sprite.enabled = false;
		}
	}

	//Called by other players when they scare you
	public void GetScared ()
	{
		//Enable sprite
		sprite.enabled = true;

		//If game is over, don't allow anyone to get scared
		if (GameControl.instance.minigameOver) {
			return;
		}

		//Die
		/*controller.Die ();

		GameControl.instance.CheckAndEndMinigame ();*/
		GameControl.instance.KillPlayer (gameObject);
	}

	//Called if you failed to scare anyone
	IEnumerator FailScare ()
	{
		stunned = true;

		//Stun player
		controller.canMove = false;

		//Also play stunned animation
		animator.SetBool ("stunned", true);

		yield return new WaitForSeconds (1f);

		controller.canMove = true;
		animator.SetBool ("stunned", false);

		stunned = false;
	}

	void Scare ()
	{
		sprite.enabled = true;

		int playersKilled = 0;

		//Check if any other players are in the area
		//And if so, kill them
		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {

			GameObject player = GameControl.instance.players [i];

			//Don't calculate dist for yourself
			if (player == gameObject) {
				continue;
			}

			//If they're in range
			/*if ((player.transform.position - transform.position).sqrMagnitude > scareDist * scareDist) {
				//Kill them
				//player.GetComponent<GhostScare> ().GetScared ();
				Debug.Log ("scared player " + player.GetComponent<PlayerPlatformerController> ().playerNum);
			}*/

			//If they're in range
			//And if they're still alive
			/*if (Vector3.Distance (player.transform.position, transform.position) < scareDist
			    && player.GetComponent<PlayerPlatformerController> ().isAlive) {
				playersKilled++;

				//Kill them
				//player.GetComponent<GhostScare> ().GetScared ();
			}*/

			if (InRange (player)) {
				playersKilled++;

				player.GetComponent<GhostScare> ().GetScared ();
			}
		}

		//If you didn't kill anyone, you failed
		if (playersKilled == 0) {
			StartCoroutine (FailScare ());
		} else {

			//If the player is flipped, player scaringflipped instead
			if (GetComponent <SpriteRenderer> ().flipX) {
				animator.SetTrigger ("scaringFlipped");
			} else {
				animator.SetTrigger ("scaring");
			}

		}
	}

	//Returns if the other player is in scare distance
	bool InRange (GameObject player)
	{
		//If player is dead, don't scare them
		if (!player.GetComponent<PlayerPlatformerController> ().isAlive) {
			return false;
		}

		//If you're in range
		if (Vector3.Distance (player.transform.position, transform.position) < scareDist) {
			//Raycast to the other player
			Vector3 direction = player.transform.position - transform.position;

			RaycastHit2D[] hit;
			hit = Physics2D.RaycastAll (transform.position, direction, scareDist);

			//If you hit a wall
			for (int i = 0; i < hit.Length; i++) {
				if (hit [i].transform.CompareTag (GameInformation.wallTag)) {
					return false;
				}
			}

			//Else if there's no wall between you, return true
			return true;
		}

		return false;
	}

	bool Moved ()
	{
		//Returns if the player moved
		Vector3 diff = transform.position - prevPos;
		bool moveX = Mathf.Abs (diff.x) > .03f;
		bool moveY = Mathf.Abs (diff.y) > .03f;

		return moveX || moveY;
	}

	void DisableParticles ()
	{
		var main = ps.main;
		main.startLifetime = 0;
	}

	void EnableParticles ()
	{
		var main = ps.main;
		main.startLifetime = .3f;
	}
}
