using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : MonoBehaviour
{
	float burnTime = 1.5f;
	float timer;

	//Used to calculate if player moved
	Vector3 prevPos;

	Color normalColor = Color.gray;
	Color dangerColor = Color.red;

	ParticleSystem particleSystem;

	void Start ()
	{
		timer = burnTime;

		particleSystem = GetComponent <ParticleSystem> ();

		ChangeParticleColor (normalColor);

		prevPos = transform.parent.position;
	}

	void Update ()
	{
		//If game is over, don't do anything
		if (GameControl.instance.minigameOver) {
			return;
		}

		if (timer < 1.5f) {
			ChangeParticleColor (dangerColor);
		} else {
			ChangeParticleColor (normalColor);
		}

		if (timer < 0) {
			GameControl.instance.KillPlayer (transform.parent.gameObject);
		}

		//If you move, then return
		//If not, decrease timer and carry out consequences
		if (Moved ()) {
			timer = burnTime;

			//Update position
			prevPos = transform.parent.position;

			return;
		}

		timer -= Time.deltaTime;
	}

	bool Moved ()
	{
		//Returns if the player moved
		/*string horizontal = "Horizontal" + GetComponentInParent <PlayerPlatformerController> ().playerNum;
		float move = Input.GetAxis (horizontal);

		return move != 0;*/

		Vector3 diff = transform.parent.position - prevPos;
		bool moveX = Mathf.Abs (diff.x) > .03f;
		bool moveY = Mathf.Abs (diff.y) > .03f;

		return moveX || moveY;
	}

	/*void KillPlayer ()
	{
		GetComponentInParent <PlayerPlatformerController> ().Die ();

		//Check if game is over
		GameControl.instance.CheckAndEndMinigame ();
	}*/

	void ChangeParticleColor (Color color)
	{
		var main = particleSystem.main;
		main.startColor = color;
	 
	}
}
