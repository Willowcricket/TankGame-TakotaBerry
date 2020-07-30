using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSplitter : Singleton<CameraSplitter>
{
    public List<Camera> cameras;
    public Camera startCam;

    // Start is called before the first frame update
    void Start()
    {
        if(!GameManager.Instance.twoPlayers)
        {
            if (GameManager.Instance.playerOne != null)
            {
                cameras[0] = GameManager.Instance.playerOne.gameObject.GetComponent<TankData>().camera;
                cameras[0].rect = new Rect(0, 0, 1, 1);
            }
        }
        else
        {
            if (GameManager.Instance.playerOne != null)
            {
                cameras[0] = GameManager.Instance.playerOne.gameObject.GetComponent<TankData>().camera;
                cameras[0].rect = new Rect(0, 0.5f, 1, 0.5f);
            }
            else
            {
                if (cameras[0] != null)
                {
                    cameras[0].rect = new Rect(0, 0, 1, 1);
                }
            }
            if (GameManager.Instance.playerTwo != null)
            {
                cameras[1] = GameManager.Instance.playerTwo.gameObject.GetComponent<TankData>().camera;
                cameras[1].rect = new Rect(0, 0, 1, 0.5f);
            }
            else
            {
                if (cameras[1] != null)
                {
                    cameras[1].rect = new Rect(0, 0, 1, 1);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.twoPlayers)
        {
            if (GameManager.Instance.playerOne != null)
            {
                cameras[0] = GameManager.Instance.playerOne.gameObject.GetComponent<TankData>().camera;
                cameras[0].rect = new Rect(0, 0, 1, 1);
            }
        }
        else
        {
            if (GameManager.Instance.playerOne != null)
            {
                cameras[0] = GameManager.Instance.playerOne.gameObject.GetComponent<TankData>().camera;
                cameras[0].rect = new Rect(0, 0.5f, 1, 0.5f);
            }
            else
            {
                if (cameras[0] != null)
                {
                    cameras[0].rect = new Rect(0, 0, 1, 1);
                }
            }
            if (GameManager.Instance.playerTwo != null)
            {
                cameras[1] = GameManager.Instance.playerTwo.gameObject.GetComponent<TankData>().camera;
                cameras[1].rect = new Rect(0, 0, 1, 0.5f);
            }
            else
            {
                if (cameras[1] != null)
                {
                    cameras[1].rect = new Rect(0, 0, 1, 1);
                }
            }
        }
    }
}
