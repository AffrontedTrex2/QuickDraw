using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetLaser : MonoBehaviour
{
	public LayerMask layermask;
	public float laserWidth = .1f;
	public float noise = 0f;
	public float maxLength = 50f;
	public Color color = Color.red;

	private LineRenderer lineRenderer;
	private int length;
	private Vector3[] position;

	private ParticleSystem ps;

	void Start ()
	{
		lineRenderer = GetComponent <LineRenderer> ();
		lineRenderer.startWidth = laserWidth;

		ps = GetComponentInChildren <ParticleSystem> ();

		//Change player to a new layer
		transform.parent.gameObject.layer = 2;
	}

	void Update ()
	{
		RenderLaser ();
	}

	void RenderLaser ()
	{
		//Shoot laser beam forwards
		UpdateLength ();

		lineRenderer.startColor = color;
		lineRenderer.endColor = color;

		//.2 is the offset so the laser comes out of the bottom instead of the center
		Vector3 startPos = transform.parent.transform.position;
		startPos.y -= .2f;
		lineRenderer.SetPosition (0, startPos);

		//Set particle system position
		ps.transform.position = lineRenderer.GetPosition (lineRenderer.positionCount - 1);

		//Move through array
		/*for (int i = 0; i < length; i++) {
			//Project the laser in the forward direction of its parent
			offset.x = transform.position.x + i * transform.forward.x + Random.Range (-noise, noise);
			//offset.z = i * transform.forward.z + Random.Range (-noise, noise) + transform.position.z;
			position [i] = offset;
			position [0] = transform.position;

			lineRenderer.SetPosition (i, position [i]);
		}*/
	}

	void UpdateLength ()
	{
		//Raycast from the location of the object forwards
		/*RaycastHit2D[] hit;
		hit = Physics2D.RaycastAll (transform.position, Vector3.down);
		int i = 0;
		while (i < hit.Length) {
			//Make sure what the laser is hitting isn't a trigger
			if (!hit [i].collider.isTrigger) {
				length = (int)Mathf.Round (hit [i].distance) + 2;
				position = new Vector3[length];
				//Move particle system to hit point
				if (endEffect) {
					endEffectTransform.position = hit [i].point;
				}
				if (!endEffect.isPlaying) {
					endEffect.Play ();
				}
				lineRenderer.positionCount = length;
				return;
			}
			i++;
		}*/

		//1 << LayerMask.NameToLayer ("PlayerUsingLaser")

		//If the player is standing on the ground, don't create the laser
		if (transform.parent.gameObject.GetComponent<PlayerPlatformerController> ().IsGrounded ()) {
			lineRenderer.enabled = false;

			if (ps.isPlaying) {
				ps.Stop ();
			}

			return;
		}
			
		lineRenderer.enabled = true;

		if (!ps.isPlaying) {
			ps.Play ();
		}

		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector3.down, maxLength, layermask);
		lineRenderer.SetPosition (lineRenderer.positionCount - 1, hit.point);

		//hit = Physics2D.Raycast (transform.position, Vector3.down, maxLength, LayerMask.NameToLayer ("Player"));

		//Check collision with player
		if (hit && (hit.transform.CompareTag (GameInformation.playerTag) ||
		    hit.transform.CompareTag ("FruitBasket"))) {

			if (GameControl.instance.minigameOver) {
				return;
			}

			//Get player object absed on if you hit the basket or not
			GameObject player;
			if (hit.transform.CompareTag (GameInformation.playerTag)) {
				player = hit.transform.gameObject;
			} else {
				player = hit.transform.parent.gameObject;
			}
				
			GameControl.instance.KillPlayer (player);
		}

		/*if (hit) {
			endEffectTransform.position = hit.point;
			endEffect.Play ();
		}*/

		//length = (int)maxLength;
		//position = new Vector3[length];
		//lineRenderer.positionCount = length;
	}
}
