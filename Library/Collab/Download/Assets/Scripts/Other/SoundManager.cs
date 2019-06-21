using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance = null;
	private AudioSource audioSource;

	[Header ("BGM")]
	public AudioClip menu;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (this);
	}

	void Start ()
	{
		audioSource = GetComponent <AudioSource> ();
	}

	//Volume is a float from 0 to 1
	public void SetVolume (float volume)
	{
		audioSource.volume = volume;
	}

	public void PlayMenu ()
	{
		audioSource.clip = menu;
	}
}
