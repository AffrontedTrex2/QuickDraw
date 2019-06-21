using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		//Set pauseUI in gamecontrol in this script because pauseUI needs to be disabled when the info screen is up
		//And gamecontrol only finds players after info screen is closed
		//So therefore I put the code here
		GameControl.instance.pauseUI = gameObject;
		gameObject.SetActive (false);
	}

	public void Resume ()
	{
		GameControl.instance.Resume ();
	}

	public void MainMenu ()
	{
        Resume();
		GameControl.instance.MainMenu ();
	}

	public void Settings ()
	{
		//TODO have a settings screen maybe
	}
}
