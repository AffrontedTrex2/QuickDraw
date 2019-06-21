using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour
{
	PlayerPlatformerController playerScript;
	ParticleSystem ps;

	void Start ()
	{
		playerScript = GetComponentInParent <PlayerPlatformerController> ();
		ps = GetComponent <ParticleSystem> ();
	}

	void Update ()
	{
		//If you're on the ground, disable the ps
		//Else, enable it
		if (playerScript.IsGrounded ()) {
			if (ps.isPlaying) {
				ps.Stop ();
			}
		} else {
			if (!ps.isPlaying) {
				ps.Play ();
			}
		}
	}
}
