using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    public Slider slider;
    public AudioSource ac;
    public Toggle toggle;
    public static float volume;

    // Update is called once per frame
    void Update()
    {
        if (toggle.isOn)
        {
            ac.volume = slider.value;
        }
        else
        {
            ac.volume = 0;
        }
        volume = ac.volume;
    }
}
