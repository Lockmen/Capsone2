using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [HeaderAttribute("사운드 등록")]
    [SerializeField] Sound[] bgmSounds;
    [SerializeField] Sound[] sfxSounds;

    [HeaderAttribute("브금 플레이어")]
    [SerializeField] AudioSource bgmPlayer;

    [Header("효과음 플레이어")]
    [SerializeField] AudioSource[] sfxPlayer;


    void Start()
    {
        instance = this;
        PlayRandomBGM();    
    }

    public void PlaySE(string _soundName)
    {

        

        for(int i = 0; i< sfxSounds.Length; i++) //사운드 등록 만큼 반복
        {
            if(_soundName == sfxSounds[i].soundName) 
            {
                for(int x = 0; x < sfxPlayer.Length; x++)
                {
                    if (!sfxPlayer[x].isPlaying)  // x번째의 mp3플레이어가 재생중이지 않다면 만족하는 조건문
                    {
                        sfxPlayer[x].clip = sfxSounds[i].clip; // 재생중이지 않은 x번재 mp3플레이어에 전 조건문에서 찾아낸 i번째 mp3를 넣어줌
                        sfxPlayer[x].Play();
                        return; // 찾으면 종류
                    }
                }
                Debug.Log("모든 효과음 플레이어가 사용중입니다.!!");
                return;

            }
        }
        Debug.Log("등록된 효과음이 없습니다.");
    }
    public void PlayRandomBGM()
    {
        int random = Random.Range(0, 2);
        bgmPlayer.clip = bgmSounds[random].clip;
        bgmPlayer.Play();
    }

    
}
