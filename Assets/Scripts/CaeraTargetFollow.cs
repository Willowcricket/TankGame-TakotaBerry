using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaeraTargetFollow : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameManager.Instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            Player = GameManager.Instance.player;
        }
        FollowPlayer();
    }

    //Handles Following The Player
    private void FollowPlayer()
    {
        if (Player == null)
        {
            Player = this.gameObject;
        }
        this.gameObject.transform.position = Player.transform.position;
        this.gameObject.transform.rotation = Player.transform.rotation;
    }
}
