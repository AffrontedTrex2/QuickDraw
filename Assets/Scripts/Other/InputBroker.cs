using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBroker
{
	static HashSet<string> forcedButtonDowns = new HashSet<string> ();

	public static bool GetButtonDown (string button)
	{
		return Input.GetButtonDown (button) || forcedButtonDowns.Contains (button);
	}

	public static void SetButtonDown (string button)
	{
		forcedButtonDowns.Add (button);
	}

	public static void RemoveButtonDown (string button)
	{
		if (forcedButtonDowns.Contains (button)) {
			forcedButtonDowns.Remove (button);
		}
	}
}
