using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

	public GameObject fireball;

	public bool isAttacking = false;

	private Animator animator;

	void Start ()
	{
		animator = GetComponent<Animator> ();
	}

	void Update ()
	{
		if (Input.GetButtonDown ("Fire1")) {
			//Fire ();

			animator.SetTrigger ("attack");
			isAttacking = true;
		} else {
			isAttacking = false;
		}
	}

	void Fire ()
	{
		//TODO change position
		GameObject fireballObj = Instantiate (fireball, transform.position, Quaternion.identity);

		Vector3 direction;
		if (Input.GetAxis ("Horizontal") > 0.01f) {
			direction = Vector3.right;
		} else {
			direction = Vector3.left;
		}

		fireballObj.GetComponent<Fireball> ().SetTarget (direction);
	}
}
