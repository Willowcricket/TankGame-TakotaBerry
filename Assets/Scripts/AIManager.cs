using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankData))]
public class AIManager : MonoBehaviour
{
    private TankMotor motor;
    private TankData data;
        
    // Start is called before the first frame update
    void Start()
    {
        motor = gameObject.GetComponent<TankMotor>();
        data = gameObject.GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
    }

    private void InputHandler()
    {
        motor.Fire();
    }

    public void OnDestroy()
    {
        GameManager.Instance.score += data.scoreToGive;
    }
}
