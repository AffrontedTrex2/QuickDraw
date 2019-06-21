using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
	//Used for slippery powerup
	/*public bool slippery = false;
	public PhysicsMaterial2D slipperyMaterial;*/

	//Used for jumpshot powerup
	public bool jumpShot = false;
	public GameObject bullet;

	//Used for sugar rush minigame
	public bool hasCandy = false;

	//Used for ghostmode powerup
	public bool ghostMode = false;

	//Used to create double jump and jetpack
	public int maxJumpsInAir = 0;
	private int jumpsInAir = 0;

	//Used to created "drunk" powerup
	public float movementModifier = 1;

	public bool canMove = true;
	public bool isAlive = true;

	public float maxSpeed = 7;
	public float jumpTakeOffSpeed = 7;

	private SpriteRenderer spriteRenderer;
	private Animator animator;

	public int playerNum;

	//Which direction you're pressing
	public Vector2 move;
	//Directino you're being pushed by another player
	public float pushDirection;

	// Use this for initialization
	void Awake ()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();

		/*if (name.Equals ("Player")) {
			player = 1;
		}
		if (name.Equals ("Player2")) {
			player = 2;
		}*/	
	}

	void Start ()
	{
		SetPlayerNum ();
	}

	protected override void FixedUpdate ()
	{
		if (Time.timeScale == 0) {
			return;
		}

		if (ghostMode) {
			GhostMode ();
			return;
		}

		//If you're slippery, use the slippery movement calc instead
		/*if (slippery) {
			Ice ();
			return;
		}*/

		base.FixedUpdate ();
	}

	protected override void Update ()
	{
		if (Time.timeScale == 0) {
			return;
		}

		if (ghostMode) {
			return;
		}

		base.Update ();

		//Reset the number of extra jumps you have
		if (grounded) {
			jumpsInAir = maxJumpsInAir;
		}
	}

	/*void Ice ()
	{
		//Set rb2d to dynamic and add slipperiness
		rb2d.bodyType = RigidbodyType2D.Dynamic;
		rb2d.sharedMaterial = slipperyMaterial;

		string horizontal = "Horizontal" + playerNum;

		//if using a joystick, change to the joystick horizontal input
		if (GameControl.instance.usingJoysticks) {
			horizontal += "Joystick";
		}

		float h = Input.GetAxis (horizontal);

		if (h * rb2d.velocity.x < maxSpeed - 2) {
			rb2d.AddForce (Vector2.right * h * 320f);
		}

		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) {
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * (maxSpeed - 2), rb2d.velocity.y);
		}

		//TODO need to calculate if you're grounded

		string jump = "Jump" + playerNum;

		//If the jump button is pressed and the player is grounded, change y velocity
		if (Input.GetButtonDown (jump)) {
			Jump ();
		}

		if (h != 0) {
			spriteRenderer.flipX = h < 0;
		}

		//animator.SetBool ("grounded", grounded);
		animator.SetBool ("grounded", true);
		animator.SetFloat ("velocityX", Mathf.Abs (h));
	}*/

	public IEnumerator Stunned (float time)
	{
		canMove = false;
		animator.SetBool ("stunned", true);

		yield return new WaitForSeconds (time);

		canMove = true;
		animator.SetBool ("stunned", false);
	}

	public void Die ()
	{
		//Player becomes ghost
		Color temp = GetComponent <SpriteRenderer> ().color;
		temp.a = .3f;
		GetComponent <SpriteRenderer> ().color = temp;

		//Also disable collider
		//GetComponent <BoxCollider2D> ().isTrigger = true;

		isAlive = false;
	}

	public void Live ()
	{
		//Undo ghost state
		Color temp = GetComponent <SpriteRenderer> ().color;
		temp.a = 1;
		GetComponent <SpriteRenderer> ().color = temp;

		//GetComponent <BoxCollider2D> ().isTrigger = false;

		isAlive = true;
	}

	public void SetPlayerNum ()
	{
		for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {
			if (gameObject == GameControl.instance.players [i]) {
				playerNum = i + 1;
				return;
			}
		}
	}

	//Causes player to jump if grounded
	//Used by Hiccup powerup and by this script
	public void Jump ()
	{
		if (!canMove) {
			return;
		}

		if (grounded) {
			/*if (slippery) {
				rb2d.AddForce (new Vector2 (0f, 300));
			} else {
				velocity.y = jumpTakeOffSpeed;
			}*/
			velocity.y = jumpTakeOffSpeed;

			//If you have jumpshot, also shoot a bullet
			if (jumpShot) {
				Shoot ();
			}

			return;
		}

		//If you have double jump/jetpack, jump
		if (jumpsInAir > 0) {
			jumpsInAir--;

			/*if (slippery) {
				rb2d.AddForce (new Vector2 (0f, 300));
			} else {
				velocity.y = jumpTakeOffSpeed;
			}*/
			velocity.y = jumpTakeOffSpeed;

			if (jumpShot) {
				Shoot ();
			}
		}
	}

	public bool IsGrounded ()
	{
		return grounded;
	}

	protected override void ComputeVelocity ()
	{
		move = Vector2.zero;

		string horizontal = "Horizontal" + playerNum;

		//if using a joystick, change to the joystick horizontal input
		if (GameControl.instance.usingJoysticks) {
			horizontal += "Joystick";
		}

		/*if (player == 1) {
			move.x = Input.GetAxis ("Horizontal");
		}
		if (player == 2) {
			move.x = Input.GetAxis ("Horizontal2");
		}*/

		move.x = Input.GetAxis (horizontal);

		string jump = "Jump" + playerNum;
		/*if (player == 2) {
			jump = "Jump2";
		}*/

		//If the jump button is pressed and the player is grounded, change y velocity
		if (Input.GetButtonDown (jump)) {
			//velocity.y = jumpTakeOffSpeed;
			Jump ();
		} else if (!grounded && Input.GetButtonUp (jump)) {
			//If the player released the jump button, decrease velocity
			if (velocity.y > 0) {
				velocity.y = velocity.y * .5f;
			}
		}

		if (move.x != 0) {
			spriteRenderer.flipX = move.x < 0;
		}

		/*bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
		if (flipSprite) {
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}*/

		//animator.SetBool ("grounded", grounded);
		animator.SetBool ("grounded", true);
		animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);

		//Set new velocity
		if (canMove && move != Vector2.zero) {
			targetVelocity = move * maxSpeed * movementModifier;
		} else {
			targetVelocity = new Vector2 (pushDirection, 0) * maxSpeed * .3f;
		}
	}

	/*	void OnCollisionEnter2D (Collision2D collision)
	{
		//If you touched another player
		if (collision.gameObject.CompareTag (GameInformation.playerTag)) {

			Debug.Log ("touching");

			//And one of you is dead
			//Or both are dead
			if ((collision.gameObject.GetComponent<PlayerPlatformerController> ().isAlive != isAlive)
			    || (!collision.gameObject.GetComponent<PlayerPlatformerController> ().isAlive && !isAlive)) {
				Debug.Log ("touching dead player");

				//Ignore the collision
				Physics2D.IgnoreCollision (collision.collider, collision.otherCollider);
			}
		}
	}*/

	//Fires a bullet
	void Shoot ()
	{
		GameObject bulletObj = Instantiate (bullet, transform.position, Quaternion.identity);

		bulletObj.GetComponent<Bullet> ().parent = gameObject;
	}

	//Movement is all four directions, no gravity
	//Need to disable collision with walls only
	void GhostMode ()
	{
		float x = Input.GetAxisRaw ("Horizontal" + playerNum);
		float y = Input.GetAxisRaw ("Vertical" + playerNum);

		float speed = .07f;

		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (x * speed, y * speed);

		//Cast a raycast to check if there's an object at the position you wanna move to
		/*RaycastHit2D hit;
		hit = Physics2D.Raycast (transform.position, new Vector2 (x, y).normalized, speed);*/

		if (!WillCollideWithPlayer (new Vector2 (x, y).normalized, speed)) {
			transform.position = end;
		}

		//If you're not colliding with a player, move
		/*if (hit.collider.gameObject.CompareTag (GameInformation.playerTag) && hit.collider.gameObject != gameObject) {
			Debug.Log ("hit player");
		} else {
			transform.position = end;
		}*/

		//rb2d.MovePosition (end);

		//Flip sprite
		if (x != 0) {
			spriteRenderer.flipX = x < 0;
		}

		//Stay on screen
		var pos = Camera.main.WorldToViewportPoint (transform.position);
		pos.x = Mathf.Clamp (pos.x, 0, 1);
		pos.y = Mathf.Clamp (pos.y, 0, 1);

		transform.position = Camera.main.ViewportToWorldPoint (pos);
	}

	bool WillCollideWithPlayer (Vector2 direction, float distance)
	{
		RaycastHit2D[] hit;
		hit = Physics2D.RaycastAll (transform.position, direction, distance);

		//If you hit a player
		for (int i = 0; i < hit.Length; i++) {
			if (hit [i].transform.CompareTag (GameInformation.playerTag) && hit [i].collider.gameObject != gameObject) {
				return true;
			}
		}

		return false;
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		//If you stomped on a player, stun them
		if (collision.gameObject.CompareTag (GameInformation.playerTag)) {
			if (AbovePlayer (collision.gameObject)) {
				StartCoroutine (collision.gameObject.GetComponent<PlayerPlatformerController> ().Stunned (.5f));
			}
		}
	}

	void OnCollisionStay2D (Collision2D collision)
	{
		if (collision.gameObject.CompareTag (GameInformation.playerTag)) {
			if (CanGetPushed (collision.gameObject)) {
				//Decide how to push them depending on your position
				if (collision.gameObject.transform.position.x > transform.position.x) {
					pushDirection = -1;
				} else {
					pushDirection = 1;
				}
			}
		}
	}

	//Reset pushDir is the other player stops pushing you
	void OnCollisionExit2D (Collision2D collision)
	{
		if (collision.gameObject.CompareTag (GameInformation.playerTag)) {
			pushDirection = 0;
		}
	}

	//Returns if the other player is stationary and at the same level as you
	bool CanGetPushed (GameObject player)
	{
		bool sameY = Mathf.Abs (transform.position.y - player.transform.position.y) < .1f;
		bool pushing = player.GetComponent<PlayerPlatformerController> ().move != Vector2.zero;
		bool notMoving = move == Vector2.zero;
		return sameY && notMoving && pushing;
	}

	bool AbovePlayer (GameObject player)
	{
		BoxCollider2D collider = GetComponent <BoxCollider2D> ();

		//Gets if you're on top of the other player
		float minYDist = collider.size.y - .1f;
		bool onTop = transform.position.y - player.transform.position.y > minYDist;

		//Make sure x pos isn't too far off
		float minXDist = collider.size.x;
		bool correctXPos = Mathf.Abs (transform.position.x - player.transform.position.x) < minXDist;

		return onTop && correctXPos;
	}

	public IEnumerator BecomeTall ()
	{
		//If you're in the air, wait to land before becoming tall
		//Doesn't work, will fix when the game breaks
		if (!grounded) {
			yield return null;
		}

		//Stretch scale y value
		Vector3 scale = transform.localScale;
		scale.y *= 2;
		transform.localScale = scale;

		//Increase box collider size
		/*BoxCollider2D boxCollider = GetComponent <BoxCollider2D> ();
		Vector2 size = boxCollider.size;
		size.y = 1;
		boxCollider.size = size;

		//Offset boxcollider
		Vector2 offset = boxCollider.offset;
		offset.y = .25f;
		boxCollider.offset = offset;*/
	}

	public void UndoTall ()
	{
		//Undo everything

		//Stretch scale y value
		Vector3 scale = transform.localScale;
		scale.y /= 2;
		transform.localScale = scale;

		//Increase box collider size
		/*BoxCollider2D boxCollider = GetComponent <BoxCollider2D> ();
		Vector2 size = boxCollider.size;
		size.y = .5f;
		boxCollider.size = size;

		//Offset boxcollider
		Vector2 offset = boxCollider.offset;
		offset.y = 0;
		boxCollider.offset = offset;*/
	}
}
