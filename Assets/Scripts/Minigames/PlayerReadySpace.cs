using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReadySpace : MonoBehaviour
{
	private Text readyText;
	private Image panel;
	public Image aButtonImage;

	void Start ()
	{
		readyText = GetComponentInChildren <Text> ();
		panel = GetComponent <Image> ();

		readyText.text = "";
	}

	public void Ready ()
	{
		panel.color = Color.green;
		readyText.text = "Ready!";
		aButtonImage.enabled = false;
	}
}
