using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timerDelay = 3.0f;
    private float timerUntilNextEvent = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerUntilNextEvent > 0)
        {
            timerUntilNextEvent -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Timer has Expired.");
            timerUntilNextEvent = timerDelay;
        }
    }
}
