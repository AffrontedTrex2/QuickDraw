using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikeball : MonoBehaviour
{
	public FruitForecast fruitForecast;

	float speed = .05f;
	bool playerStunned = false;

	void Update ()
	{
		if (Time.timeScale == 0) {
			return;
		}

		transform.position += Vector3.down * speed;

		//If it goes off screen, destroy the block
		if (Camera.main.WorldToViewportPoint (transform.position).y < -.2f && !playerStunned) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D collision)
	{
		if (GameControl.instance.minigameOver) {
			return;
		}

		if (collision.gameObject.CompareTag (GameInformation.playerTag)) {
			//GameControl.instance.KillPlayer (collision.gameObject);
			//Stun player for a moment
			StartCoroutine (Stunned (collision.gameObject));

			//Hide object to create illusion of being destroyed
			GetComponent <SpriteRenderer> ().enabled = false;
			GetComponent <CircleCollider2D> ().enabled = false;
		}
	}

	public IEnumerator Stunned (GameObject player)
	{
		PlayerPlatformerController playerScript = player.GetComponent<PlayerPlatformerController> ();
		Animator animator = player.GetComponent<Animator> ();

		playerStunned = true;

		playerScript.canMove = false;
		animator.SetBool ("stunned", true);

		yield return new WaitForSeconds (1f);

		playerScript.canMove = true;
		animator.SetBool ("stunned", false);

		playerStunned = false; 
	}
}
