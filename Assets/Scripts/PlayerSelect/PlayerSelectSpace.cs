using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectSpace : MonoBehaviour
{
	private Text text;
    private Image image;

    private void Start() {
        text = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
    }

    public void Connect (int player)
	{
		text.text = "Player " + player + " connected!";
        SetTransparent(false);
	}

	public void Disconnect ()
	{
		text.text = "Connect a controller to play!";
        SetTransparent(true);
	}

    void SetTransparent(bool transparent) {
        Color temp = image.color;

        if (transparent) {
            temp.a = .4f;
        } else {
            temp.a = 1;
        }

        image.color = temp;
    }
}
