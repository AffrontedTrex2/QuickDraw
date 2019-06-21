using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBlock : MonoBehaviour
{
	float speed = .05f;

	void Update ()
	{
		if (Time.timeScale == 0) {
			return;
		}

		transform.position += Vector3.down * speed;

		//If it goes off screen, destroy the block
		if (Camera.main.WorldToViewportPoint (transform.position).y < -.2f) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D collision)
	{
		if (GameControl.instance.minigameOver) {
			return;
		}

		if (collision.gameObject.CompareTag (GameInformation.playerTag)) {
			GameControl.instance.KillPlayer (collision.gameObject);

			Destroy (gameObject);
		}
	}
}
