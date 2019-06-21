using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeTileColor : MonoBehaviour
{
	//Put on player
	//When the player touches a tile, the color changes to their color

	public GameObject tilemapObj;

	//Tile to change touched tiles to
	public TileBase changedTile;

	private Tilemap tilemap;

	void Start ()
	{
		if (tilemapObj != null) {
			tilemap = tilemapObj.GetComponent<Tilemap> ();
		}
	}

	void OnCollisionStay2D (Collision2D collision)
	{
		//If you're dead, you can't change tile color any more
		if (!GetComponent <PlayerPlatformerController> ().isAlive) {
			return;
		}

		Vector3 hitPosition = Vector3.zero;

		if (tilemap != null && tilemapObj == collision.gameObject) {

			foreach (ContactPoint2D hit in collision.contacts) {
				hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
				hitPosition.y = hit.point.y - 0.01f * hit.normal.y;

				//Only change the tile to the colored tile if there is a tile at the pos
				if (tilemap.HasTile (tilemap.WorldToCell (hitPosition))) {
					tilemap.SetTile (tilemap.WorldToCell (hitPosition), changedTile);
				}
			}

		}
	}
}
