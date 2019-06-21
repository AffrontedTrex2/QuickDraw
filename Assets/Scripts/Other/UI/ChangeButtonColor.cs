using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeButtonColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    //Changes button text's color as you hover/click on it
    private Text text;

    private void Start() {
        text = GetComponentInChildren<Text>();
    }

    //These two are used for eventsystem joystick selection
    public void SelectButton() {
        text.color = Color.yellow;
    }

    public void LeaveButton() {
        text.color = Color.white;
    }

    //Used for changing text on mouse over
    public void OnPointerEnter(PointerEventData eventData) {
        text.color = Color.yellow;
    }

    public void OnPointerDown(PointerEventData eventData) {
        text.color = Color.green;
    }

    public void OnPointerUp(PointerEventData eventData) {
        text.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData) {
        text.color = Color.white;
    }
}
