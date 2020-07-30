using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource MusicS;
    public AudioSource FxS;

    public AudioClip Shoot;
    public AudioClip Hit;
    public AudioClip Explode;
    public AudioClip PowerUp;
    public AudioClip Select;
    public AudioClip BGMusic;

    void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        MusicS.clip = BGMusic;
        MusicS.Play();
    }

    public void PlayFX(int Fx)
    {
        switch (Fx)
        {
            case 1:
                FxS.clip = Shoot;
                FxS.Play();
                break;
            case 2:
                FxS.clip = Hit;
                FxS.Play();
                break;
            case 3:
                FxS.clip = Explode;
                FxS.Play();
                break;
            case 4:
                FxS.clip = PowerUp;
                FxS.Play();
                break;
            case 5:
                FxS.clip = Select;
                FxS.Play();
                break;
        }
    }
}
