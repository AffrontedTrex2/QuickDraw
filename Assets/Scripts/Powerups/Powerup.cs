using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup
{
	public string name;
	public string description;
	public Sprite icon;
	//public GameObject target;

	public string function;

	public Powerup (string name, string description, Sprite icon, string function)
	{
		this.name = name;
		this.description = description;
		this.icon = icon;

		this.function = function;
	}

	/*public void SetTarget (GameObject target)
	{
		this.target = target;
	}*/
}
