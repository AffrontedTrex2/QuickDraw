using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPowerupAnimation : MonoBehaviour
{
	//Used by GetPowerupUI's quickdraw animation to call PowerupController/other methods

	public GameObject powerupControlObj;

	private PowerupControl powerupControl;

	void Start ()
	{
		powerupControl = powerupControlObj.GetComponent<PowerupControl> ();
	}

	public void SetTimescale (float scale)
	{
		Time.timeScale = scale;
	}   

	public void FinishAnimation ()
	{
		powerupControl.animationFinished = true;
		gameObject.SetActive (false);
	}
}
