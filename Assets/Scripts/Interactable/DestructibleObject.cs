using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
	protected int health;

	private Animator animator;

	protected virtual void Start ()
	{
		animator = GetComponent <Animator> ();
	}

	protected virtual void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject.CompareTag (GameInformation.playerTag)) {

			//Check if player is attacking
			bool isAttacking = other.gameObject.GetComponent<PlayerInteraction> ().isAttacking;

			if (isAttacking) {
				Debug.Log ("remaining health: " + health);
				bool isAlive = DecreaseHealth (GameInformation.playerDamage);



				if (!isAlive) {
					DestroySelf ();
				}
			}
		}
	}

	protected virtual void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag (GameInformation.playerTag)) {

			//Check if player is attacking
			bool isAttacking = other.gameObject.GetComponent<PlayerInteraction> ().isAttacking;

			if (isAttacking) {
				Debug.Log ("remaining health: " + health);
				bool isAlive = DecreaseHealth (GameInformation.playerDamage);

				if (!isAlive) {
					DestroySelf ();
				}
			}
		}
	}

	protected virtual void DestroySelf ()
	{
		animator.SetTrigger ("die");
	}

	//TODO make the hit animation only run once

	//Returns if the object still has health
	protected virtual bool DecreaseHealth (int amount)
	{
		animator.SetTrigger ("hit");

		health -= amount;

		return health > 0;
	}

	protected void DestroyObject ()
	{
		Destroy (gameObject);
	}
}
