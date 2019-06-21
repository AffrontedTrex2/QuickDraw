using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance = null;
	public AudioSource audioSource;

	[Header ("BGM")]
	public AudioClip menu;
    public AudioClip background;
    public AudioClip win;

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
        audioSource.Play();
	}

    public void PlayBackground() {
        audioSource.clip = background;
        audioSource.Play();
    }

    public void PlayWin() {
        audioSource.clip = win;
        audioSource.Play();
    }
}
