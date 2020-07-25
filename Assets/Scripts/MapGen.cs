using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGen : MonoBehaviour
{
    public int rows;
    public int colums;
    private float roomWidth = 50f;
    private float roomHeight = 50f;
    public GameObject[] gridPreFabs;
    private Room[,] grid;
    public int mapSeed;

    public enum MapType { Seeded, Random, MapOfTheDay }
    public MapType mapType = MapType.Random;

    public int DateToInt(DateTime dateToUse)
    {
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    public GameObject RandomRoomPreFab()
    {
        return gridPreFabs[UnityEngine.Random.Range(0, gridPreFabs.Length)];
    }

    public void GenerateGrid()
    {
        UnityEngine.Random.InitState(mapSeed);
        grid = new Room[colums, rows];
        for (int gridRow = 0; gridRow < rows; gridRow++)
        {
            for (int gridColum = 0; gridColum < colums; gridColum++)
            {
                float xPosition = roomWidth * gridColum;
                float zPosition = roomHeight * gridRow;
                Vector3 newPosition = new Vector3(xPosition, 0, zPosition);

                GameObject tempRoomObj = Instantiate(RandomRoomPreFab(), newPosition, Quaternion.identity) as GameObject;

                tempRoomObj.transform.parent = this.transform;

                tempRoomObj.name = "Room_" + gridColum + ", " + gridRow;

                Room tempRoom = tempRoomObj.GetComponent<Room>();

                if (rows == 1)
                {

                }
                else if (gridRow == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (gridRow == rows - 1)
                {
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }

                if (colums == 1)
                {

                }
                else if (gridColum == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                else if (gridColum == colums - 1)
                {
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }

                grid[gridColum, gridRow] = tempRoom;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        switch (mapType)
        {
            case MapType.Seeded:
                break;
            case MapType.Random:
                mapSeed = DateToInt(DateTime.Now);
                break;
            case MapType.MapOfTheDay:
                mapSeed = DateToInt(DateTime.Now.Date);
                break;
            default:
                Debug.LogWarning("[MapGen]: MapType not set");
                break;
        }
        GenerateGrid();
        GameManager.Instance.SpawnPlayer();
        if (GameManager.Instance.twoPlayers)
        {
            GameManager.Instance.SpawnPlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
