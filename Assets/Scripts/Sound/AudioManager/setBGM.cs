using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;
public class setBGM : MonoBehaviour
{
    public AudioMixer BGM_mixer;
    public Slider BGM_slider;
    
    private float MusicVolumeDefaultValue = 0.75f;

    private User bgmUser;
    void Start()
    {
        bgmUser = DataBaseManager.Instance.getUserData();
        init(); 
    }
    
    void init()
    {
        BGM_slider.value = bgmUser.bgmVolume;
        BGM_mixer.SetFloat("BGM", BGM_slider.value);
    }

    public float GetDefaultBGMVolumeValue()
    {
        return MusicVolumeDefaultValue;
    }
    public void AudioControl()
    {
        float sound = BGM_slider.value;

        if (sound == -40f)
        {
            BGM_mixer.SetFloat("BGM", -80);
        }
        else
        {
            BGM_mixer.SetFloat("BGM", sound);
        }
        DataBaseManager.Instance.UpdateBGM(bgmUser.userEmail, sound);
    }
    /*
    public void SetBGMLevel(float sliderValue)
    {
        BGM_mixer.SetFloat("BGM_Volume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("BGM_Volume", sliderValue);
        // user info 에서도 volume 값을 데이터화해서 저장시켜줘야하는 메서드 추가해줘야됨.
    }
    */

    public void OnClickToggleVolume()
    {
        //AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;

        float sound = BGM_slider.value;
        if (sound > -40f)
        {
            sound = -80f;

        }
        else
        {
            sound = 0;
        }
        BGM_slider.value = sound;
        BGM_mixer.SetFloat("BGM", sound);
        //AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;

        DataBaseManager.Instance.UpdateBGM(bgmUser.userEmail, sound);
    }
    
  
}
