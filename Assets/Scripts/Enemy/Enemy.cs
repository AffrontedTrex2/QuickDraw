using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{

	public float moveSpeed = 1f;

	private int moveDirection = 1;
	private int changeMoveDirCounter = 10;

	//	private float castDist = 1f;

	protected override void ComputeVelocity ()
	{
		Vector2 move = Vector2.zero;

		move.x = moveDirection;

		//Set new velocity
		targetVelocity = move * moveSpeed;

		//Change direction if the movecounter is 0 (to prevent wierd stuff) and if velocity is 0
		if (changeMoveDirCounter == 0 && velocity.x == 0) {
			changeMoveDirCounter = 10;
			moveDirection *= -1;
		}

		if (changeMoveDirCounter > 0) {
			changeMoveDirCounter--;
		}
	}

	//TODO set a bool isDead to prevent multiple "deaths" from occuring (thus increasing extra exp)

	private void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.CompareTag (GameInformation.playerTag)) {
			//Check if player is above
			//RaycastHit2D playerAbove = Physics2D.Raycast (transform.position, Vector2.up, castDist);

			//Check if player is attacking
			bool isAttacking = other.gameObject.GetComponent<PlayerInteraction> ().isAttacking;

			if (isAttacking) {
				//GameControl.instance.changeScore (GameInformation.enemyRegularScore);
				Destroy (gameObject);
			} else {
				//GameControl.instance.changeHealth (GameInformation.enemyRegularDamage);
			}
		}
	}

	private void OnCollisionStay2D (Collision2D other)
	{
		//TODO Also fix this collision (same issue as crate; not as precise)
		if (other.gameObject.CompareTag (GameInformation.playerTag)) {
			//Check if player is above
			//RaycastHit2D playerAbove = Physics2D.Raycast (transform.position, Vector2.up, castDist);

			//Check if player is attacking
			bool isAttacking = other.gameObject.GetComponent<PlayerInteraction> ().isAttacking;

			if (isAttacking) {
				//GameControl.instance.changeScore (GameInformation.enemyRegularScore);
				Destroy (gameObject);
			}
		}
	}
}
