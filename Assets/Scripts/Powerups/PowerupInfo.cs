using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupInfo : MonoBehaviour
{
	public List<Powerup> powerups;

	[Header ("Icons")]
	public Sprite burningIcon;
	public Sprite highJumpIcon;
	public Sprite hiccupIcon;
	public Sprite drunkIcon;
	public Sprite stompIcon;
	public Sprite doubleJumpIcon;
	public Sprite jetpackIcon;
	public Sprite hauntedIcon;
	public Sprite lowGravityIcon;
	public Sprite ghostModeIcon;
	public Sprite feetLaserIcon;
	public Sprite jumpShotIcon;
	public Sprite tallIcon;

	[Header ("Prefabs to Attach")]
	public GameObject burningPrefab;
	public GameObject hiccupPrefab;
	public GameObject drunkPrefab;
	public GameObject jetpackPrefab;
	public GameObject ghostPrefab;
	public GameObject feetLaserPrefab;


	void Start ()
	{
		powerups = new List<Powerup> ();

		Init ();
	}

	//Create powerups, add them all to the list
	void Init ()
	{
		Powerup burning = new Powerup ("Smoking hot", "You're on fire! You run faster, but you also die if you stop moving...", burningIcon, "Burning");
		powerups.Add (burning);

		Powerup highJump = new Powerup ("Swole thighs", "If only you had these IRL. You jump like 20 feet higher now.", highJumpIcon, "HighJump");
		powerups.Add (highJump);

		Powerup hiccup = new Powerup ("Hiccup", "Better drink some water, buddy. Your interact and jump button will go off at random intervals.", hiccupIcon, "Hiccup");
		powerups.Add (hiccup);

		Powerup drunk = new Powerup ("Drunk", "You'd play better this way. All controls reversed!", drunkIcon, "Drunk");
		powerups.Add (drunk);

		Powerup stomp = new Powerup ("Spiked boots", "Like a certain plumber, you can now stomp the life out of your enemies by jumping on them.", stompIcon, "Stomp");
		powerups.Add (stomp);

		Powerup doubleJump = new Powerup ("Double jump", "What it says on the tin.", doubleJumpIcon, "DoubleJump");
		powerups.Add (doubleJump);

		Powerup jetpack = new Powerup ("Jetpack", "Mash that jump button to fly!", jetpackIcon, "Jetpack");
		powerups.Add (jetpack);

		Powerup haunted = new Powerup ("Haunted", "You're now haunted by the Ghost of Christmas Past. If it's any consolation, the ghost can also kill other players...", hauntedIcon, "Haunted");
		powerups.Add (haunted);

		Powerup lowGravity = new Powerup ("Low Gravity", "It's like space, but with more gravity.", lowGravityIcon, "LowGravity");
		powerups.Add (lowGravity);

		Powerup feetLaser = new Powerup ("Feet Lasers", "Imagine you could shoot lasers out of your eyes, but instead of your eyes, they come out of your toes.", feetLaserIcon, "FeetLaser");
		powerups.Add (feetLaser);

		Powerup jumpShot = new Powerup ("Jump Shot", "Knee guns that only fire when you jump...", jumpShotIcon, "JumpShot");
		powerups.Add (jumpShot);

		/*Powerup tall = new Powerup ("Tall", "That's it. That's your superpower.", tallIcon, "Tall");
		powerups.Add (tall);*/

		/*Powerup ghostMode = new Powerup ("Ghost", "Don't worry, you're not dead...yet.", ghostModeIcon, "GhostMode");
		powerups.Add (ghostMode);*/
	}

	//----------
	//FUNCTIONS
	//----------

	//---------------------------------------------------------------------

	public void Tall ()
	{
		StartCoroutine (GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().BecomeTall ());
	}

	public void UndoTall ()
	{
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().UndoTall ();
	}

	//---------------------------------------------------------------------

	public void JumpShot ()
	{
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().jumpShot = true;
	}

	public void UndoJumpShot ()
	{
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().jumpShot = false;
	}

	//---------------------------------------------------------------------

	public void FeetLaser ()
	{
		GameControl.instance.GetTarget ().layer = 2;

		Instantiate (feetLaserPrefab, GameControl.instance.GetTarget ().transform);
	}

	public void UndoFeetLaser ()
	{
		GameControl.instance.GetTarget ().layer = 8;

		GameObject laser = GameControl.instance.GetTarget ().GetComponentInChildren <FeetLaser> ().gameObject;
		Destroy (laser);
	}

	//---------------------------------------------------------------------

	public void GhostMode ()
	{
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().ghostMode = true;
		//GameControl.instance.GetTarget ().GetComponent<BoxCollider2D> ().enabled = false;
	}

	public void UndoGhostMode ()
	{
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().ghostMode = false;
		//GameControl.instance.GetTarget ().GetComponent<BoxCollider2D> ().enabled = true;
	}

	//---------------------------------------------------------------------

	public void LowGravity ()
	{
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().gravityModifier = .3f;
	}

	public void UndoLowGravity ()
	{
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().gravityModifier = 2;
	}

	//---------------------------------------------------------------------

	public void Haunted ()
	{
		GameObject ghost = Instantiate (ghostPrefab);
		ghost.GetComponent<Ghost> ().target = GameControl.instance.GetTarget ();
	}

	public void UndoHaunted ()
	{
		GameObject ghost = GameObject.FindGameObjectWithTag ("Ghost");
		Destroy (ghost);
	}

	//---------------------------------------------------------------------

	public void Jetpack ()
	{
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().maxJumpsInAir = 1000;

		GameObject jetpack = Instantiate (jetpackPrefab, GameControl.instance.GetTarget ().transform);
		jetpack.transform.localPosition = new Vector3 (.02f, 0, 0);
	}

	public void UndoJetpack ()
	{
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().maxJumpsInAir = 0;

		GameObject jetpack = GameControl.instance.GetTarget ().GetComponentInChildren <Jetpack> ().gameObject;
		Destroy (jetpack);
	}

	//---------------------------------------------------------------------

	public void DoubleJump ()
	{
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().maxJumpsInAir = 1;
	}

	public void UndoDoubleJump ()
	{
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().maxJumpsInAir = 0;
	}

	//---------------------------------------------------------------------

	public void Stomp ()
	{
		GameControl.instance.GetTarget ().AddComponent <Stomp> ();
	}

	public void UndoStomp ()
	{
		Destroy (GameControl.instance.GetTarget ().GetComponent<Stomp> ());
	}

	//---------------------------------------------------------------------

	public void Drunk ()
	{
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().movementModifier = -1;

		Instantiate (drunkPrefab, GameControl.instance.GetTarget ().transform);
	}

	public void UndoDrunk ()
	{
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().movementModifier = 1;

		GameObject drunk = GameControl.instance.GetTarget ().GetComponentInChildren <Drunk> ().gameObject;
		Destroy (drunk);
	}

	//---------------------------------------------------------------------

	public void Hiccup ()
	{
		GameObject hiccup = Instantiate (hiccupPrefab, GameControl.instance.GetTarget ().transform);
		hiccup.transform.position += new Vector3 (.3f, .3f, 0f);
	}

	public void UndoHiccup ()
	{
		GameObject hiccup = GameControl.instance.GetTarget ().GetComponentInChildren <Hiccup> ().gameObject;
		Destroy (hiccup);
	}

	//---------------------------------------------------------------------

	public void HighJump ()
	{
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().jumpTakeOffSpeed *= 1.5f;
	}

	public void UndoHighJump ()
	{
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().jumpTakeOffSpeed *= (2f / 3f);
	}

	//---------------------------------------------------------------------

	public void Burning ()
	{
		//Increase target speed
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().maxSpeed *= 2f;

		//Add halo to target to show burning
		/*Behaviour halo = (Behaviour)target.GetComponent ("Halo");
		halo.enabled = true;*/

		Instantiate (burningPrefab, GameControl.instance.GetTarget ().transform);
	}

	public void UndoBurning ()
	{
		//Increase target speed
		GameControl.instance.GetTarget ().GetComponent<PlayerPlatformerController> ().maxSpeed /= 2f;

		//Add halo to target to show burning
		/*Behaviour halo = (Behaviour)target.GetComponent ("Halo");
		halo.enabled = false;*/

		GameObject emitter = GameControl.instance.GetTarget ().GetComponentInChildren <ParticleSystem> ().gameObject;
		Destroy (emitter);
	}

}
