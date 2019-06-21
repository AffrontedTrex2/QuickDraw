using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpikeBall : MonoBehaviour
{
	public GameObject wallTilemapObj;
	public GameObject floorTilemapObj;

	float speed;
	Vector3 direction;

	float timeBeforeIncreasingSpeed = 4f;
	float maxTime = 4f;

	//Amount to offset direction by when the ball bounces
	//Increases as time passes
	float offset = 0;

	void Start ()
	{
		speed = 3f;

		//direction = new Vector3 (100, 100);
		direction = new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f));
		direction *= 100;
		//rb2d.AddForce (direction * speed);
	}

	void Update ()
	{
		IncreaseSpeed ();
	}

	void FixedUpdate ()
	{
		//Move in direction
		transform.position = Vector3.MoveTowards (transform.position, direction, speed * Time.deltaTime);

		//Add force to rb2d
		/*if (Mathf.Abs (rb2d.velocity.x) < maxSpeed && Mathf.Abs (rb2d.velocity.y) < maxSpeed) {
			rb2d.AddForce (direction * moveForce);
		}*/

		/*if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) {
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		}*/
	}

	void IncreaseSpeed ()
	{
		timeBeforeIncreasingSpeed -= Time.deltaTime;

		if (timeBeforeIncreasingSpeed < 0) {

			timeBeforeIncreasingSpeed = maxTime;
			speed += .5f;

			offset += .05f;

			//Apply new speed to ball
			/*Vector3 direction = rb2d.velocity.normalized;
			rb2d.AddForce (direction * speed);*/
		}
		
		
	}

	//Reverse direction to create bouncing effect
	//Collision = location of object you collided with
	/*void SetDirection (Vector3 collision)
	{
		//If wall is above/below you, reverse y
		if (Mathf.Abs (collision.y - transform.position.y) < .1f) {
			direction.x *= -1;
		}

		//If wall is to the left/right, reverse x
		if (Mathf.Abs (collision.x - transform.position.x) < .1f) {
			direction.y *= -1;
		}

		if (collision.y != transform.position.y) {
			direction.x *= -1;
		}

		if (collision.x != transform.position.x) {
			direction.y *= -1;
		}

		//Add a little ??? to the mix
		direction.x += Random.Range (-.2f, .2f);
		direction.y += Random.Range (-.2f, .2f);
	}*/

	void SetDirection (GameObject collision)
	{
		if (collision == wallTilemapObj) {
			direction.x *= -1;
		}
		if (collision == floorTilemapObj) {
			direction.y *= -1;
		}

		//Add a little ??? to the mix
		direction.Normalize ();
		direction.x += Random.Range (-offset, offset);
		direction.y += Random.Range (-offset, offset);
		direction *= 100;
	}

	void OnCollisionEnter2D (Collision2D collision)
	{

		//If you collided with a player, kill them
		if (collision.gameObject.CompareTag (GameInformation.playerTag)) {

			if (GameControl.instance.minigameOver) {
				return;
			}

			GameControl.instance.KillPlayer (collision.gameObject);

			/*collision.gameObject.GetComponent <PlayerPlatformerController> ().Die ();

			//Check if the game is over
			GameControl.instance.CheckAndEndMinigame ();*/
		}

		//If you collided with a wall, change direction
		/*Vector3 hitPosition = Vector3.zero;

		if (collision.gameObject == wallTilemapObj || collision.gameObject == floorTilemapObj) {

			//Set new direction based on which wall you hit
			

			foreach (ContactPoint2D hit in collision.contacts) {
				hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
				hitPosition.y = hit.point.y - 0.01f * hit.normal.y;

				//set direction based on where you collided
				SetDirection (hitPosition);

				break;
			}

		}*/

		SetDirection (collision.gameObject);
	}
}
