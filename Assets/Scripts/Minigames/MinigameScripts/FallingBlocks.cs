using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlocks : MonoBehaviour
{
	//What will spawn from the top
	public GameObject deathBlock;

	float spawnSpeed = .7f;
	float spawnTimer = .7f;
	float timer = 3f;

	void Update ()
	{
		if (GameControl.instance.minigameOver) {
			return;
		}
			
		//If spawn timer is zero, spawn block
		if (spawnTimer > 0) {
			spawnTimer -= Time.deltaTime;
		} else {
			SpawnBlock ();
			spawnTimer = spawnSpeed;
		}

		//If timer runs to zero, decrease spawn speed
		if (timer > 0) {
			timer -= Time.deltaTime;
		} else {
			//If time has run out, lower spawn speed
			spawnSpeed -= .05f;
			timer = 3f;
		}
	}

	void SpawnBlock ()
	{
		//Spawn a block
		GameObject block = Instantiate (deathBlock);

		//Randomize block x position
		float x = Random.Range (0f, 1f);

		//Set block position
		block.transform.position = Camera.main.ViewportToWorldPoint (new Vector3 (x, 1f, 0f));

		//Set z to 0
		Vector3 blockPos = block.transform.position;
		blockPos.z = 1;
		block.transform.position = blockPos;
	}
}
