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
}
