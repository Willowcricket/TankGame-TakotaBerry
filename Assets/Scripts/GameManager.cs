using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerPreFab;
    public GameObject playerOne;
    public GameObject playerTwo;
    public float bulletDamage = 50.0f;
    public List<GameObject> playerSpawners;
    public List<ScoreData> scores;
    public bool twoPlayers = false;

    protected override void Awake()
    {
        base.Awake();
        playerSpawners = new List<GameObject>();
        scores.Sort();
        scores.Reverse();
    }

    public void SpawnPlayer()
    {
        int itemIndex = Random.Range(0, (playerSpawners.Count - 1));
        Transform pointToSpawn = playerSpawners[itemIndex].gameObject.transform;
        Instantiate(playerPreFab, pointToSpawn);
    }
}
