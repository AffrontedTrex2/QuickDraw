using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelect : MonoBehaviour
{
    EventSystem eventSystem;

    private void Start() {
        eventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        eventSystem.currentSelectedGameObject.GetComponent<ChangeButtonColor>().SelectButton();
    }
}
