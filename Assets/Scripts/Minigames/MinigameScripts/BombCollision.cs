using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollision : MonoBehaviour
{
	bool onCooldown = false;

	//OnCollisionStay
	void OnCollisionStay2D (Collision2D collision)
	{
		if (collision == null) {
			return;
		}

		if (onCooldown) {
			return;
		}

		//Also return if you don't have the bomb
		if (transform.childCount == 0) {
			return;
		} 

		if (GetComponentInChildren <Bomb> ().gameObject == null) {
			return;
		}

		//If you hit another player
		if (collision.gameObject.CompareTag (GameInformation.playerTag)) {

			//If the player you collided with is already dead, return
			if (!collision.gameObject.GetComponent<PlayerPlatformerController> ().isAlive) {
				return;
			}

			//Start cooldown
			StartCoroutine (TransferCooldown ());
			//Start cooldown of other player too
			StartCoroutine (collision.gameObject.GetComponent<BombCollision> ().TransferCooldown ());

			//Transfer the bomb
			TransferBomb (collision.gameObject);
		}
	}

	void TransferBomb (GameObject player)
	{
		//Save who is holding the bomb right now
		//GameObject prevPlayer = transform.parent.gameObject;

		GameObject bomb = GetComponentInChildren <Bomb> ().gameObject;

		bomb.transform.SetParent (player.transform);
		bomb.transform.position = player.transform.position;

		//Increase player speed
		IncreasePlayerSpeed (player);

		//Decrease prev player's speed
		DecreasePlayerSpeed (gameObject);

		//Enable the script of the new bomb holder
		bomb.GetComponentInParent <BombCollision> ().enabled = true;

		//Disable this cript
		this.enabled = false;
	}

	public IEnumerator TransferCooldown ()
	{
		//Disable hitbox
		//GetComponent <BoxCollider2D> ().enabled = false;
		onCooldown = true;

		yield return new WaitForSeconds (.5f);

		//GetComponent <BoxCollider2D> ().enabled = true;
		onCooldown = false;
	}

	void IncreasePlayerSpeed (GameObject player)
	{
		player.GetComponent<PlayerPlatformerController> ().maxSpeed *= 1.3f;
	}

	void DecreasePlayerSpeed (GameObject player)
	{
		player.GetComponent<PlayerPlatformerController> ().maxSpeed *= (10f / 13f);
	}

}
