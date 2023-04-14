using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;
public class setSFX : MonoBehaviour
{
    public AudioMixer SFX_mixer;
    public Slider SFX_slider;

    private float MusicVolumeDefaultValue = 0.75f;

    private User sfxUser;
 
    void Start()
    {

        sfxUser = DataBaseManager.Instance.getUserData();
        init(); 
    }
    
    void init()
    {
        SFX_slider.value = sfxUser.sfxVolume;
        SFX_mixer.SetFloat("SFX", SFX_slider.value);
    }

    public float GetDefaultSfxVolumeValue()
    {
        return MusicVolumeDefaultValue;
    }
    public void AudioControl()
    {
        float sound = SFX_slider.value;

        if (sound == -40f)
        {
            SFX_mixer.SetFloat("SFX", -80);
        }
        else
        {
            SFX_mixer.SetFloat("SFX", sound);
        }
        DataBaseManager.Instance.UpdateSFX(sfxUser.userEmail, sound);
    }

    public void OnClickToggleVolume()
    {
        float sound = SFX_slider.value;
        if (sound > -40f)
        {
            sound = -80f;

        }
        else
        {
            sound = 0; 
        }
        SFX_slider.value = sound;
        SFX_mixer.SetFloat("SFX", sound);
        //AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;

        DataBaseManager.Instance.UpdateSFX(sfxUser.userEmail, sound);
    }
    
    /*
    public void SetSFXLevel(float sliderValue)
    {
        SFX_mixer.SetFloat("SFX_Volume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFX_Volume", sliderValue);
    }
    */
}
