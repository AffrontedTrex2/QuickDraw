using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
	public float minGroundNormalY = .65f;
	public float gravityModifier = 1f;

	protected Vector2 targetVelocity;
	protected bool grounded;
	protected Vector2 groundNormal;
	protected Rigidbody2D rb2d;
	protected Vector2 velocity;
	protected ContactFilter2D contactFilter;
	//Where the raycast will check
	protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
	//Stores results of raycast
	protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);
	//Stores results of raycasts that hit something

	protected const float minMoveDistance = 0.001f;
	//Will only check for collision if velocity is more than this value
	protected const float shellRadius = 0.01f;
	//Buffer to prevent overlapping during collision

	void OnEnable ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start ()
	{
		//ContactFilter2D will be set to the gameObject's current layer
		contactFilter.useTriggers = false;
		contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
		contactFilter.useLayerMask = true;	
	}
	
	// Update is called once per frame
	protected virtual void Update ()
	{
		targetVelocity = Vector2.zero;
		ComputeVelocity ();
	}

	protected virtual void ComputeVelocity ()
	{
	}

	protected virtual void FixedUpdate ()
	{
		//Calculates velocity based on gravity and the modifer

		velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
		velocity.x = targetVelocity.x;

		grounded = false;

		Vector2 deltaPosition = velocity * Time.deltaTime;

		Vector2 moveAlongGround = new Vector2 (groundNormal.y, -groundNormal.x);
		Vector2 move = moveAlongGround * deltaPosition.x;

		//Calculate x movement
		Movement (move, false);

		move = Vector2.up * deltaPosition.y;

		//Calculate y movement
		Movement (move, true);
	}

	protected virtual void Movement (Vector2 move, bool yMovement)
	{
		float distance = move.magnitude;

		//If the object is moving, check for collision
		if (distance > minMoveDistance) {
			int count = rb2d.Cast (move, contactFilter, hitBuffer, distance + shellRadius);
		
			//Add the raycasts that hit something to the list
			hitBufferList.Clear ();
			for (int i = 0; i < count; i++) {
				hitBufferList.Add (hitBuffer [i]);
			}

			for (int i = 0; i < hitBufferList.Count; i++) {
				Vector2 currentNormal = hitBufferList [i].normal;

				//If the angle of the collision shows that the it's colliding with the ground
				if (currentNormal.y > minGroundNormalY) {
					grounded = true;

					//If you're moving vertically, 
					if (yMovement) {
						groundNormal = currentNormal;
						currentNormal.x = 0;
					}
				}

				//Checks if a new collision from the movement could occur, and prevent colliders from overlapping
				float projection = Vector2.Dot (velocity, currentNormal);
				if (projection < 0) {
					velocity = velocity - projection * currentNormal;
				}

				float modifiedDistance = hitBufferList [i].distance - shellRadius;
				distance = modifiedDistance < distance ? modifiedDistance : distance;
			}
		}

		//Moves object
		rb2d.position = rb2d.position + move.normalized * distance;
	}

	public void moveX (Vector2 velocity)
	{
		this.velocity = velocity;
		Movement (this.velocity, false);
	}
}
