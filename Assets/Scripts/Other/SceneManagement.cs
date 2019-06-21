using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
	public void ColorCraze ()
	{
		SceneManager.LoadScene (GameInformation.colorCraze);
	}

	public void ScoreScreen ()
	{
		SceneManager.LoadScene (GameInformation.scoreScreen);
	}

    public void OptionsScreen() {
        SceneManager.LoadScene(GameInformation.options);
    }

	public void WinScreen ()
	{
		SceneManager.LoadScene (GameInformation.winScreen);
        SoundManager.instance.PlayWin();
	}

	public void PlayerSelect ()
	{
		SceneManager.LoadScene (GameInformation.playerSelect);
	}

	public void KeyboardSelect ()
	{
		SceneManager.LoadScene (GameInformation.keyboardSelect);
	}

	public void MainMenu ()
	{
        GameControl.instance.Init();
		SceneManager.LoadScene (GameInformation.mainMenu);
        SoundManager.instance.PlayMenu();
	}

	public void BombTag ()
	{
		SceneManager.LoadScene (GameInformation.bombTag);
	}

	public void GhostScare ()
	{
		SceneManager.LoadScene (GameInformation.ghostScare);
	}

	public void SpikeBall ()
	{
		SceneManager.LoadScene (GameInformation.spikeBall);
	}

	public void Stomp ()
	{
		SceneManager.LoadScene (GameInformation.stomp);
	}

	public void FallingBlocks ()
	{
		SceneManager.LoadScene (GameInformation.fallingBlocks);
	}

	public void Vertigo ()
	{
		SceneManager.LoadScene (GameInformation.vertigo);
	}

	public void SugarRush ()
	{
		SceneManager.LoadScene (GameInformation.sugarRush);
	}

	public void ChainsawBackstab ()
	{
		SceneManager.LoadScene (GameInformation.chainsawBackstab);
	}

	public void FruitForecast ()
	{
		SceneManager.LoadScene (GameInformation.fruitForecast);
	}

	public void Quit ()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}
}
