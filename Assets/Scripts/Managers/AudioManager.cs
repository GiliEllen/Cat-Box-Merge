using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Meow1;
    public AudioSource Meow2;
    public AudioSource Meow3;
    public AudioSource Meow4;
    public AudioSource Meow5;
    public AudioSource Meow6;
    public AudioSource Meow7;
    public AudioSource Meow8;
    public AudioSource Meow9;
    public AudioSource backgroundMusic;
    public List<AudioSource> CatMeowList;

    void Start() {
        InitializeCatMeowList();
    }

    void InitializeCatMeowList() {
        CatMeowList = new List<AudioSource>() {
            Meow1,
            Meow2,
            Meow3,
            Meow4,
            Meow5,
            Meow6,
            Meow7,
            Meow8,
            Meow9
        };
    }
    public void PlayBackgroundMusic() {
        backgroundMusic.Play();
    }
    public void PauseBackgroundMusic() {
        backgroundMusic.Pause();
    }

    public void PlayRandomMeow() {
        AudioSource randomMeow = CatMeowList.GetRandomItem();
        if (randomMeow != null) {
            randomMeow.Play();
        }
    }
}
