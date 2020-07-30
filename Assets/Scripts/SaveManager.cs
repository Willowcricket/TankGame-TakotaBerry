using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : Singleton<SaveManager>
{
    public float musicVolume;
    public float fxVolume;
    public int playerCount;
    public int mapType;
    public int mapSeed;

    public Slider MusicSlider;
    public Slider FXSlider;
    public Dropdown PlayerCountDD;
    public Dropdown MapTypeDD;
    public InputField SeedIF;

    private void Start()
    {
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
        PlayerPrefs.SetFloat("FXVolume", FXSlider.value);
        PlayerPrefs.SetInt("PlayerCount", PlayerCountDD.value);
        PlayerPrefs.SetInt("MapType", MapTypeDD.value);
        if (SeedIF.text.Length == 0)
        {
            PlayerPrefs.SetString("MapSeed", "0");
        }
        else
        {
            PlayerPrefs.SetString("MapSeed", SeedIF.text);
        }
        PlayerPrefs.Save();
        Load();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }
        if (PlayerPrefs.HasKey("FXVolume"))
        {
            fxVolume = PlayerPrefs.GetFloat("FXVolume");
        }
        if (PlayerPrefs.HasKey("PlayerCount"))
        {
            playerCount = PlayerPrefs.GetInt("PlayerCount");
        }
        if (PlayerPrefs.HasKey("MapType"))
        {
            mapType = PlayerPrefs.GetInt("MapType");
        }
        if (PlayerPrefs.HasKey("MapSeed"))
        {
            mapSeed = Int32.Parse(PlayerPrefs.GetString("MapSeed"));
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
