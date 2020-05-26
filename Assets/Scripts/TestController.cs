using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
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
        if (Input.GetKey(KeyCode.W))
        {
            motor.Move(data.moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            motor.Move(data.moveSpeed * -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            motor.Rotate(data.rotateSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            motor.Rotate(data.rotateSpeed * -1);
        }
    }
}
