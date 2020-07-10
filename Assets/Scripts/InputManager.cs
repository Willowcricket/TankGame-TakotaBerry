using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankData))]
public class InputManager : MonoBehaviour
{
    private TankMotor motor;
    private TankData data;
    public enum InputScheme { WASD, arrowKeys };
    public InputScheme input = InputScheme.WASD;

    // Start is called before the first frame update
    void Start()
    {
        motor = gameObject.GetComponent<TankMotor>();
        data = gameObject.GetComponent<TankData>();
        if (GameManager.Instance.player == null)
        {
            GameManager.Instance.player = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
    }

    private void InputHandler()
    {
        switch (input)
        {
            case InputScheme.WASD:
                if (Input.GetKey(KeyCode.W))
                {
                    motor.Move(data.moveSpeed);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    motor.Move(data.backwardMoveSpeed * -1);
                }
                else
                {
                    motor.Move(0);
                }

                if (Input.GetKey(KeyCode.D))
                {
                    motor.Rotate(data.rotateSpeed);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    motor.Rotate(data.rotateSpeed * -1);
                }
                else
                {
                    motor.Rotate(0);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    motor.Fire();
                }
                break;
            case InputScheme.arrowKeys:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    motor.Move(data.moveSpeed);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    motor.Move(data.backwardMoveSpeed * -1);
                }
                else
                {
                    motor.Move(0);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    motor.Rotate(data.rotateSpeed);
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    motor.Rotate(data.rotateSpeed * -1);
                }
                else
                {
                    motor.Rotate(0);
                }
                break;
            default:
                Debug.LogError("[InputManager]: Undefined Input Scheme");
                break;
        }
    }
}
