using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	//Who shot the bullet
	public GameObject parent;
	private Vector3 target;

	private float speed = 6f;

	void Start ()
	{
		//Set bullet direction to the way you're facing
		float xDir = 1;
		if (parent.GetComponent<SpriteRenderer> ().flipX) {
			xDir = -1;

			//Also flip x of this sprite
			GetComponent <SpriteRenderer> ().flipX = true;
		}

		target = parent.gameObject.transform.position;
		target.x = xDir * 100;
	}

	void Update ()
	{
		/*Vector3 pos = transform.position;
		pos.x += xDir * speed;
		transform.position = pos;*/

		transform.position = Vector2.MoveTowards (transform.position, target, speed * Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D collision)
	{
		if (GameControl.instance.minigameOver) {
			Destroy (gameObject);
			return;
		}

		//Don't kill the player who shot the bullet
		if (collision.gameObject.Equals (parent)) {
			return;
		}

		if (collision.gameObject.CompareTag (GameInformation.playerTag)) {
			GameControl.instance.KillPlayer (collision.gameObject);
			Destroy (gameObject);
		}

		if (collision.gameObject.CompareTag (GameInformation.wallTag)) {
			Destroy (gameObject);
		}
	}
}
