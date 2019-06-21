using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame
{
	public string name;
	public string description;

	public string sceneName;

	public Minigame (string name, string description, string sceneName)
	{
		this.name = name;
		this.description = description;

		this.sceneName = sceneName;
	}

	public bool Equals (string str)
	{
		return sceneName.Equals (str);
	}
}
