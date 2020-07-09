using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public GameObject pickUpPreFab;
    public List<GameObject> pickUpPreFabs;
    public GameObject currentPickUp;
    public float spawnDelay;
    private float nextSpawnTime;
    private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        nextSpawnTime = Time.time + spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPickUp == null)
        {
            if (Time.time > nextSpawnTime)
            {
                pickUpPreFab = pickUpPreFabs[Random.Range(0, pickUpPreFabs.Count)];
                currentPickUp = Instantiate(pickUpPreFab, tf.position, Quaternion.identity);
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else
        {
            nextSpawnTime = Time.time + spawnDelay;
        }
    }
}
