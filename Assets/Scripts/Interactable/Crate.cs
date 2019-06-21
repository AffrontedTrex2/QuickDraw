using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : PhysicsObject
{

	public float moveSpeed = .8f;

	private float moveDirection = 0;
	private float castDist = 2f;

	protected override void ComputeVelocity ()
	{
		Vector2 move = Vector2.zero;
		move.x = moveDirection;

		targetVelocity = move * moveSpeed;

		//Debug.Log ("moveDir: " + moveDirection + " velo: " + targetVelocity);
	}

	private void OnCollisionEnter2D (Collision2D other)
	{

		if (other.gameObject.CompareTag (GameInformation.playerTag)) {
			//Check if the player is above
			//TODO this isn't as precise as it should be
			RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.up, castDist);

			if (!hit) {
				//moveDirection = other.gameObject.GetComponent<Rigidbody2D> ().velocity.x;
				if (other.gameObject.transform.position.x > transform.position.x) {
					moveDirection = -1;
				} else {
					moveDirection = 1;
				}
			}

			//Debug.Log ("Player: " + other.gameObject.transform.position.y + " Crate: " + transform.position.y);
		}
	}

	private void OnCollisionExit2D (Collision2D other)
	{
		moveDirection = 0;
	}
}
