using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerPreFab;
    public GameObject playerOne;
    public GameObject playerTwo;
    public int playerOneLives = 3;
    public int playerTwoLives = 3;
    public float bulletDamage = 50.0f;
    public List<GameObject> playerSpawners;
    public List<ScoreData> scores;
    public GameObject mapGenner;
    public GameObject map;
    public GameObject gameOverScreen;
    public bool twoPlayers = false;

    protected override void Awake()
    {
        base.Awake();
        playerSpawners = new List<GameObject>();
        scores.Sort();
        scores.Reverse();
    }

    private void Update()
    {
        if (SaveManager.Instance.playerCount == 0)
        {
            twoPlayers = false;
            if (playerOneLives == 0)
            {
                if (playerOne == null)
                {
                    CameraSplitter.Instance.startCam.enabled = true;
                    Destroy(map);
                    gameOverScreen.SetActive(true);
                }
            }
        }
        else
        {
            twoPlayers = true;
            if (playerOneLives == 0 && playerTwoLives == 0)
            {
                if (playerOne == null && playerTwo == null)
                {
                    CameraSplitter.Instance.startCam.enabled = true;
                    Destroy(map);
                    gameOverScreen.SetActive(true);
                }
            }
        }
        this.gameObject.GetComponent<AudioManager>().MusicS.volume = SaveManager.Instance.musicVolume;
        this.gameObject.GetComponent<AudioManager>().FxS.volume = SaveManager.Instance.fxVolume;
    }

    public void SpawnPlayer()
    {
        int itemIndex = Random.Range(0, (playerSpawners.Count - 1));
        Transform pointToSpawn = playerSpawners[itemIndex].gameObject.transform;
        Instantiate(playerPreFab, pointToSpawn);
    }

    public void SpawnMapGen()
    {
        Instantiate(mapGenner);
    }
}
