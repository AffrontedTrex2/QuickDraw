using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiccup : MonoBehaviour
{
	bool startHiccup = false;

	Animator animator;

	//Time until next hiccup
	float timer;

	void Start ()
	{
		SetTimer ();

		animator = GetComponent <Animator> ();
	}

	void Update ()
	{
		if (!startHiccup) {

			//Wait for player to press interact button before allowing hiccups to start
			//Interact = used to close powerup ui, so this ensures that hiccups only start after the ui is closed
			string interact = "Interact" + GetComponentInParent <PlayerPlatformerController> ().playerNum;
			if (Input.GetButtonDown (interact)) {
				startHiccup = true;
			}

			return;
		}

		//Lift interact
		ReleaseButton ();

		//TODO if the game is paused, return, and don't subtract time

		//Independent of timescale
		timer -= Time.unscaledDeltaTime;

		if (timer < 0) {
			SetTimer ();

			PressButton ();

			//Play hiccup animation
			animator.SetTrigger ("Hiccup");
		}
	}

	void ReleaseButton ()
	{
		string interact = "Interact" + GetComponentInParent <PlayerPlatformerController> ().playerNum;
		InputBroker.RemoveButtonDown (interact);
	}

	void PressButton ()
	{
		string interact = "Interact" + GetComponentInParent <PlayerPlatformerController> ().playerNum;
		InputBroker.SetButtonDown (interact);

		//Jump
		GetComponentInParent <PlayerPlatformerController> ().Jump ();
	}

	void SetTimer ()
	{
		timer = Random.Range (.3f, 2f);
	}
}
