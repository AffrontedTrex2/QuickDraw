using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketStay : MonoBehaviour
{
	private Vector2 position;

	void Start ()
	{
		position = transform.localPosition;
	}

	void Update ()
	{
		//Keep basket at the same position
		transform.localPosition = position;
	}
}
