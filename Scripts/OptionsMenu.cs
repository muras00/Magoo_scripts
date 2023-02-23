using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider BGM_slider;
    public Slider SE_slider;
    public void SetMasterVol(float volume)
    {
        audioMixer.SetFloat("master", volume);
        //BGM
        audioMixer.SetFloat("bgm", volume);
        BGM_slider.SetValueWithoutNotify(volume);
        //SE
        audioMixer.SetFloat("sound_effect", volume);
        SE_slider.SetValueWithoutNotify(volume);
    }
    public void SetBGMVol(float volume)
    {
        audioMixer.SetFloat("bgm", volume);
    }
    public void SetSEVol(float volume)
    {
        audioMixer.SetFloat("sound_effect", volume);
    }
}