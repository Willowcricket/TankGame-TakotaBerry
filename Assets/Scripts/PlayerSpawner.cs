using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.playerSpawners.Add(this.gameObject);
    }

    void OnDestroy()
    {
        GameManager.Instance.playerSpawners.Remove(this.gameObject);
    }
}
