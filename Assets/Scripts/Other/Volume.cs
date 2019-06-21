using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour {

    Slider slider;

    private void Start() {
        slider = GetComponent<Slider>();
        slider.value = SoundManager.instance.audioSource.volume;
    }

    public void OnValueChanged() {
        SoundManager.instance.audioSource.volume = slider.value;
    }
}
