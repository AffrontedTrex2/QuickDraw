using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
	private Vector3 direction;
	private float moveSpeed = 7;

	public void SetTarget (Vector2 direction)
	{
		this.direction = direction;
	}

	void Update ()
	{
		transform.Translate (direction * Time.deltaTime * moveSpeed);
	}

	private void OnCollisionEnter2D (Collision2D other)
	{
		//TODO collision with wall not working

		if (GameInformation.canCollide.Contains (other.gameObject.tag)) {
			Destroy (gameObject);
		}

		if (other.gameObject.CompareTag (GameInformation.enemyTag)) {
			Destroy (other.gameObject);
			/*GameControl.instance.changeExp (GameInformation.enemyRegularExp);
			GameControl.instance.changeScore (GameInformation.enemyRegularScore);*/
			Destroy (gameObject);
		}
	}
}
