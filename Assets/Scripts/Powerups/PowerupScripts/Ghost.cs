using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ghost : MonoBehaviour
{
	public Sprite following;
	public Sprite hiding;

	public GameObject target;

	private float speed = 0.01f;
	private SpriteRenderer renderer;

	void Start ()
	{
		renderer = GetComponent <SpriteRenderer> ();
	}

	void Update ()
	{
		if (Time.timeScale == 0) {
			return;
		}

		FacePlayer ();

		if (PlayerFacingAway ()) {
			FollowPlayer ();

			renderer.sprite = following;
		} else {
			//Else, replace sprite with hiding sprite
			renderer.sprite = hiding;
		}
	}

	void FacePlayer ()
	{
		//Flip sprite so that the ghost is looking at player
		renderer.flipX = target.transform.position.x > transform.position.x;
	}

	bool PlayerFacingAway ()
	{
		bool playerFacingLeft = target.GetComponent<SpriteRenderer> ().flipX;
		bool playerRightOfGhost = target.transform.position.x < transform.position.x;

		return (playerFacingLeft && playerRightOfGhost) || (!playerFacingLeft && !playerRightOfGhost);
	}

	void FollowPlayer ()
	{
		Vector3 direction = (target.transform.position - transform.position).normalized;

		transform.position += direction * speed;
	}

	void OnTriggerEnter2D (Collider2D collision)
	{
		if (GameControl.instance.minigameOver) {
			return;
		}

		//Kill players it touches
		if (collision.transform.CompareTag (GameInformation.playerTag)) {
			GameControl.instance.KillPlayer (collision.gameObject);
		}
	}

	/*	void KillPlayer (GameObject player)
	{
		player.GetComponent <PlayerPlatformerController> ().Die ();

		GameControl.instance.CheckAndEndMinigame ();
	}*/
}