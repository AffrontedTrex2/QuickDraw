using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBlockTile : MonoBehaviour
{

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (GameControl.instance.minigameOver) {
			return;
		}

		if (collision.gameObject.CompareTag (GameInformation.playerTag)) {
			KillPlayer (collision.gameObject);
		}
	}

	void KillPlayer (GameObject player)
	{
		player.GetComponent <PlayerPlatformerController> ().Die ();

		GameControl.instance.CheckAndEndMinigame ();
	}
}
