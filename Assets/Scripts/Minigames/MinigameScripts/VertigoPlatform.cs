using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertigoPlatform : MonoBehaviour
{
	Rigidbody2D rb2d;
	float speed = .01f;

	void Start ()
	{
		rb2d = GetComponent <Rigidbody2D> ();
	}

	void FixedUpdate ()
	{
		Move ();
	}

	void Move ()
	{
		Vector2 pos = rb2d.position;
		pos.y += speed;
		rb2d.position = pos;
	}

	/*protected override void Update ()
	{
		if (Time.timeScale == 0) {
			return;
		}

		base.Update ();

		Vector2 pos = rb2d.position;
		pos += Vector2.down * speed;
		rb2d.position = pos;

		if (transform.position.y <= -3.3f) {
			Destroy (gameObject);
		}
	}*/
}
