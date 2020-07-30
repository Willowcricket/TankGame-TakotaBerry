using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject titleMenu;

    public void StartGame()
    {
        GameManager.Instance.SpawnMapGen();
        optionsMenu.SetActive(false);
        titleMenu.SetActive(false);
        CameraSplitter.Instance.startCam.enabled = false;
        GameManager.Instance.GetComponent<AudioManager>().PlayFX(5);
    }

    public void Reset()
    {
        GameManager.Instance.playerOneLives = 3;
        GameManager.Instance.playerTwoLives = 3;
        titleMenu.SetActive(true);
        GameManager.Instance.gameOverScreen.SetActive(false);
        GameManager.Instance.GetComponent<AudioManager>().PlayFX(5);
    }

    public void Options()
    {
        if (optionsMenu.activeSelf == false)
        {
            optionsMenu.SetActive(true);
        }
        else
        {
            optionsMenu.SetActive(false);
        }
        GameManager.Instance.GetComponent<AudioManager>().PlayFX(5);
    }

    public void QuitGame()
    {
        GameManager.Instance.GetComponent<AudioManager>().PlayFX(5);
        Application.Quit();
    }
}
