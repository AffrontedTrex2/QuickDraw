﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontGoThroughThings : MonoBehaviour
{
	public bool sendTriggerMessage = false;

	public LayerMask layerMask = -1;
	public float skinWidth = 0.1f;

	private float minimumExtent;
	private float partialExtent;
	private float sqrMinimumExtent;
	private Vector3 previousPosition;
	private Rigidbody2D myRigidbody;
	private Collider2D myCollider;

	void Start ()
	{
		myRigidbody = GetComponent <Rigidbody2D> ();
		myCollider = GetComponent <BoxCollider2D> ();
		previousPosition = myRigidbody.position;
		minimumExtent = Mathf.Min (Mathf.Min (myCollider.bounds.extents.x, myCollider.bounds.extents.y), myCollider.bounds.extents.z);
		partialExtent = minimumExtent * (1.0f - skinWidth);
		sqrMinimumExtent = minimumExtent * minimumExtent;
	}

	void FixedUpdate ()
	{
		Vector3 movementThisStep = (Vector3)myRigidbody.position - previousPosition;
		float movementSqrMagnitude = movementThisStep.sqrMagnitude;

		if (movementSqrMagnitude > sqrMinimumExtent) {
			float movementMagnitude = Mathf.Sqrt (movementSqrMagnitude);
			RaycastHit hitInfo;

			if (Physics.Raycast (previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value)) {
				if (!hitInfo.collider) {
					return;
				}

				if (hitInfo.collider.isTrigger) {
					hitInfo.collider.SendMessage ("OnTriggerEnter2D", myCollider);
				} else {
					myRigidbody.position = hitInfo.point - (movementThisStep * movementMagnitude) * partialExtent;
				}
			}
		}

		previousPosition = myRigidbody.position;
	}
}
