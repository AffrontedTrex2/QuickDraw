using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
	//public LayerMask layer;

	void OnCollisionStay2D (Collision2D collision)
	{
		if (GameControl.instance.minigameOver) {
			return;
		}

		//Raycast down
		//If you stepped on a player's head, kill them

		//If you hit another player
		/*if (collision.gameObject.CompareTag (GameInformation.playerTag)) {
			Debug.DrawRay (transform.position, Vector2.down, Color.green);

			RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, .5f, LayerMask.NameToLayer ("Player"));
			if (hit != null) {
				Debug.Log ("TOP");
			}

			if (OnPlayer ()) {
				Debug.Log ("TOP");
			}
		}*/

		if (collision.gameObject.CompareTag (GameInformation.playerTag)) {
			if (AbovePlayer (collision.gameObject)) {
				GameControl.instance.KillPlayer (collision.gameObject);
			}
		}
	}

	/*void KillPlayer (GameObject player)
	{
		player.GetComponent <PlayerPlatformerController> ().Die ();

		GameControl.instance.CheckAndEndMinigame ();
	}*/

	bool AbovePlayer (GameObject player)
	{
		BoxCollider2D collider = GetComponent <BoxCollider2D> ();

		//Gets if you're on top of the other player
		float minYDist = collider.size.y - .1f;
		bool onTop = transform.position.y - player.transform.position.y > minYDist;

		//Make sure x pos isn't too far off
		float minXDist = collider.size.x;
		bool correctXPos = Mathf.Abs (transform.position.x - player.transform.position.x) < minXDist;

		return onTop && correctXPos;
	}
	/*
	bool OnPlayer ()
	{
		Debug.DrawRay (transform.position, Vector2.down * .5f, Color.green);

		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, .5f, layer);
		if (hit != null && hit.transform != transform) {
			return true;
		}

		return false;
	}*/
}
