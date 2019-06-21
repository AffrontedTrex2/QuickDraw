using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.CompareTag (GameInformation.playerTag)) {
			GetCandy (collision.gameObject);
		}
	}

	void GetCandy (GameObject player)
	{
		PlayerPlatformerController playerScript = player.GetComponent<PlayerPlatformerController> ();

		//If you're already safe, don't grab the candy
		//Or if you're dead
		if (playerScript.hasCandy || !playerScript.isAlive) {
			return;
		}

		//Give player candy and destroy self
		playerScript.hasCandy = true;
		Destroy (gameObject);
	}
}
