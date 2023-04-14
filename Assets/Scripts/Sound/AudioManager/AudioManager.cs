using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; // audio name
    public AudioClip clip; // mp3, wave 음원파일
    
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private Sound[] sfx = null; // 효과음
    [SerializeField] private Sound[] bgm = null; // 배경음
    [SerializeField] private AudioSource bgmPlayer = null;
    [SerializeField] private AudioSource[] sfxPlayer = null;

    private User aUser;
    private void MakeSingleTon()
    {
        if(instance != null)
            DestroyImmediate(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Awake()
    {
        MakeSingleTon();
        //aUser = DataBaseManager.Instance.getUserData();
    }
    public void PlayBGM(string bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (bgmName == bgm[i].name)
            {
                Debug.Log("bgmPlayer play " + bgmName);
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }
    }
    public bool CheckBGM(string bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (bgmName == bgm[i].name)
            {
                if(bgmPlayer.clip == bgm[i].clip)
                    return true;
            } 
        }
        return false;
    }
    
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }
    
    public void PlaySFX(string sfxName)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (sfxName == sfx[i].name)
            {
                for (int j = 0; j < sfxPlayer.Length; j++)
                {
                    // 재생중이지 않다면?
                    if (!sfxPlayer[j].isPlaying)
                    {
                        sfxPlayer[j].clip = sfx[i].clip;
                        sfxPlayer[j].Play();
                        
                        return;
                    }
                }
                Debug.Log("모든 sfx 오디오 플레이어가 재생중");
                return;
                
            }
        }
        Debug.Log(sfxName + "이름이 없음");
    }
}
