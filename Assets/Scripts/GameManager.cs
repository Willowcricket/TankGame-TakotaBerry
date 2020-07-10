using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerPreFab;
    public GameObject player;
    public float bulletDamage = 50.0f;
    public float score = 0.0f;
    public List<GameObject> playerSpawners;

    protected override void Awake()
    {
        base.Awake();
        playerSpawners = new List<GameObject>();
    }

    public void SpawnPlayer()
    {
        int itemIndex = Random.Range(0, (playerSpawners.Count - 1));
        Transform pointToSpawn = playerSpawners[itemIndex].gameObject.transform;
        Instantiate(playerPreFab, pointToSpawn);
    }
}
