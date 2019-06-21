using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    SceneManagement sceneManager;

    private void Start() {
        sceneManager = GetComponent<SceneManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        //If cancel is pressed, go back
        if (Input.GetButtonDown("Cancel")) {
            sceneManager.MainMenu();
        }
    }
}
