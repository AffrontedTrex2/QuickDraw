using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chainsaw : MonoBehaviour
{
	SpriteRenderer renderer;
	SpriteRenderer parentRenderer;

	float yPos;
	float xPos;

	void Start ()
	{
		renderer = GetComponent <SpriteRenderer> ();
		parentRenderer = transform.parent.GetComponent <SpriteRenderer> ();

		xPos = transform.localPosition.x;
		yPos = transform.localPosition.y;
	}

	void Update ()
	{
		//If the x of the parent is flipped, then flip the chainsaw
		FlipChainsaw ();

		transform.localPosition = new Vector2 (transform.localPosition.x, yPos);
	}

	void FlipChainsaw ()
	{
		renderer.flipX = parentRenderer.flipX;

		//Also change position
		Vector3 pos = transform.localPosition;
		if (parentRenderer.flipX) {
			pos.x = xPos * -1f;
		} else {
			pos.x = xPos;
		}
		transform.localPosition = pos;
	}

	void OnTriggerEnter2D (Collider2D collision)
	{
		if (GameControl.instance.minigameOver) {
			return;
		}

		if (collision.gameObject.CompareTag (GameInformation.playerTag) && !collision.gameObject.name.Equals (transform.parent.name)) {
			//If you stabbed someone in the back, kill them
			if (StabbedBack (collision.gameObject)) {
				GameControl.instance.KillPlayer (collision.gameObject);
			}
		}
	}

	bool StabbedBack (GameObject player)
	{
		bool leftOfPlayer = transform.parent.position.x < player.transform.position.x;
		bool facingRight = !player.GetComponent<SpriteRenderer> ().flipX;

		return (leftOfPlayer && facingRight) || (!leftOfPlayer && !facingRight);
	}
}
