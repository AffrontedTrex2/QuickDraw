using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupUIImage : MonoBehaviour
{
	public Image winnerImage;
	public Image[] loserImages;

    public void SetImages(GameObject winner) {
        int winnerIndex = winner.GetComponent<PlayerPlatformerController>().playerNum - 1;

        //Set the color of the winner to that of the winning player
        winnerImage.color = GameControl.instance.playerColors[winnerIndex];

        //Disable all the images
        for (int i = 0; i < loserImages.Length; i++) {
            loserImages[i].enabled = false;
        }

        //Create an array contaning the player numbers
        int[] players = new int[GameControl.instance.numOfPlayers - 1];
        int index = 0;
        for (int i = 0; i < GameControl.instance.numOfPlayers; i++) {

            //Make sure that the current player you're pulling isn't the winner
            if (i != winnerIndex) {

                //Add it to the array
                players[index] = i;
                index++;
            }
        }

        //Enable and set colors
        for (int i = 0; i < players.Length; i++) {
            loserImages[i].enabled = true;

            int currentPlayer = players[i];
            loserImages[i].color = GameControl.instance.playerColors[currentPlayer];
        }
    }

	/*public void SetImages2 (GameObject winner)
	{
		int winnerIndex = winner.GetComponent<PlayerPlatformerController> ().playerNum - 1;

		//Set the color of the winner to that of the winning player
		winnerImage.color = GameControl.instance.playerColors [winnerIndex];

        //Disable all the images
        for (int i = 0; i < loserImages.Length; i++) {
            loserImages[i].enabled = false;
        }

        //If the winner is player 1, use another method (broken, so I hardcoded it)
        if (winnerIndex == 0) {
            SetImages();
            return;
        }

		//Set the rest of the images
		int index = 0;
		for (int i = 0; i < loserImages.Length; i++) {

			//If you've run out of players, disable the rest of the images
			if (i + 1 >= GameControl.instance.numOfPlayers) {
				loserImages [index].enabled = false;
				index++;
				continue;
			}

            //Enable the image
			loserImages [index].enabled = true;

			//If this player didn't win
			if (i != winnerIndex) {
				//Set their color
				loserImages [index].color = GameControl.instance.playerColors [i];
				index++;
			}
		}
	}

    void SetImages() {
        //For each of the rest of the players
        for (int i = 0; i < GameControl.instance.numOfPlayers - 1; i++) {
            //Enable the image
            loserImages[i].enabled = true;

            //Set the color to the player color
            //i + 1 because player 1 won
            loserImages[i].color = GameControl.instance.playerColors[i + 1];
        }
    }*/
}
