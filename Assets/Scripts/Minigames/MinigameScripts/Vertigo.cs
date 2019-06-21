using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Vertigo : MonoBehaviour
{
	public GameObject grid;
	public GameObject[] tilemapObjs;

	private float summonSpeed = 2.5f;
	private float timer = 0;

	private int prevIndex = -1;

	void Update ()
	{
		if (GameControl.instance.minigameOver || Time.timeScale == 0) {
			return;
		}

		//Summon the tiles if the timer runs out
		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			SummonPlatform ();
			timer = summonSpeed;
		}
	}

	void SummonPlatform ()
	{
		//Spawn a block
		GameObject block = Instantiate (GetRandomPlatform (), grid.transform);

		//Set block position
		block.transform.position = new Vector3 (-2.804f, 3.5f, 0f);

		//Set z to 0
		/*Vector3 blockPos = block.transform.position;
		blockPos.z = 1;
		block.transform.position = blockPos;*/
	}

	//Returns a random gameobject from tilemapObjs
	GameObject GetRandomPlatform ()
	{
		int index = prevIndex;

		//Make sure it's not a repeat
		while (index == prevIndex) {
			index = Random.Range (0, tilemapObjs.Length);
		}

		prevIndex = index;

		return tilemapObjs [index];
	}
}
