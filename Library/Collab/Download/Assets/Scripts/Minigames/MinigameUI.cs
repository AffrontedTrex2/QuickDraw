using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameUI : MonoBehaviour
{
	public GameObject minigameUI;

	public Text nameText;
	public Text descriptionText;

	private Animator demoAnimator;

	void Awake ()
	{
		demoAnimator = minigameUI.GetComponentInChildren <Animator> ();
	}

	//Updates the UI with the minigame information
	public void UpdateMinigameUI (Minigame minigame)
	{
		nameText.text = minigame.name;
		descriptionText.text = minigame.description;
		demoAnimator.SetTrigger (minigame.sceneName);
		//demoImage.sprite = minigame.demo;
	}

	public void ShowMinigameUI ()
	{
		minigameUI.SetActive (true);

		//Pause game
		GameControl.instance.Pause ();
	}

	//Clears icon, name, and description, and sets inactive
	public void ClearAndHideMinigameUI ()
	{
		nameText.text = "";
		descriptionText.text = "";
		demoAnimator.gameObject.SetActive (false);

		minigameUI.SetActive (false);

		//Resume game
		GameControl.instance.Resume ();
	}
}
