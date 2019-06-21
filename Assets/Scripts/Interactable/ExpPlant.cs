using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPlant : DestructibleObject
{

	protected override void Start ()
	{
		base.Start ();

		Debug.Log ("health set");
		health = 3;
	}

	protected override void DestroySelf ()
	{
		base.DestroySelf ();

		//GameControl.instance.changeExp (GameInformation.expPlant);
	}
}
