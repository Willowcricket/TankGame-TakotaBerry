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

    private GameObject Player;

    public Transform[] waypoints;
    public int currentWaypoint = 0;
    public float closeToWaypoint = 1.0f;

    public enum LoopType {Loop, PingPong, Random};
    public LoopType loopType;
    private bool isPatrolForward = true;

    public enum Mode {Patrol, Attack, Flee};
    public Mode mode;

    public enum Avoidence {NotAvoiding, Rotating, Moving};
    public Avoidence avoidence;

    public float avoidTime = 1.0f;
    private float exitTime;

    public enum AIPersonality {Agresive, Cautious, Sentry, Coward};
    public AIPersonality aiPersonality;

    public float fovDistance = 7.5f;

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
        if (aiPersonality == AIPersonality.Agresive)
        {
            Agresive();
        }
        else if (aiPersonality == AIPersonality.Cautious)
        {
            Cautious();
        }
        else if (aiPersonality == AIPersonality.Sentry)
        {
            Sentry();
        }
        else if (aiPersonality == AIPersonality.Coward)
        {
            Coward();
        }
    }





    private void Coward()
    {
        if (CanSeePlayer())
        {
            mode = Mode.Flee;
        }
        else
        {
            mode = Mode.Patrol;
        }

        if (mode == Mode.Patrol)
        {
            Patrol();
        }
        else if (mode == Mode.Flee)
        {
            if (!(avoidence == Avoidence.NotAvoiding))
            {
                Avoid();
            }
            else
            {
                Flee();
            }
        }
    }

    private void Sentry()
    {
        if (CanSeePlayer())
        {
            mode = Mode.Attack;
        }
        else
        {
            mode = Mode.Patrol;
        }

        if (mode == Mode.Patrol)
        {
            motor.Move(0);
            motor.Rotate(data.rotateSpeed);
        }
        else if (mode == Mode.Attack)
        {
            motor.RotateToward(Player.transform.position, data.rotateSpeed);
            motor.Fire();
        }
    }

    private void Cautious()
    {
        if (CanSeePlayer())
        {
            mode = Mode.Attack;
            if (data.currentHealth < 60.0f)
            {
                mode = Mode.Flee;
            }
        }
        else
        {
            mode = Mode.Patrol;
        }

        if (mode == Mode.Patrol)
        {
            Patrol();
        }
        else if (mode == Mode.Attack)
        {
            if (!(avoidence == Avoidence.NotAvoiding))
            {
                Avoid();
            }
            else
            {
                Chase();
            }
        }
        else if (mode == Mode.Flee)
        {
            if (!(avoidence == Avoidence.NotAvoiding))
            {
                Avoid();
            }
            else
            {
                Flee();
            }
        }
    }

    private void Agresive()
    {
        if (CanSeePlayer())
        {
            mode = Mode.Attack;
        }
        else
        {
            mode = Mode.Patrol;
        }

        if (mode == Mode.Patrol)
        {
            Patrol();
        }
        else if (mode == Mode.Attack)
        {
            if (!(avoidence == Avoidence.NotAvoiding))
            {
                Avoid();
            }
            else
            {
                Chase();
            }
        }
    }





    private void Patrol()
    {
        if (motor.RotateToward(waypoints[currentWaypoint].position, data.rotateSpeed))
        {
            //Do Nothing, Is Rotating
        }
        else
        {
            motor.Move(data.moveSpeed);
        }

        if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - this.gameObject.transform.position) < (closeToWaypoint * closeToWaypoint))
        {
            if (loopType == LoopType.Loop)
            {
                Loop();
            }
            else if (loopType == LoopType.PingPong)
            {
                PingPong();
            }
            else if (loopType == LoopType.Random)
            {
                currentWaypoint = UnityEngine.Random.Range(0, waypoints.Length);
            }
        }
    }

    private void Avoid()
    {
        if (avoidence == Avoidence.Rotating)
        {
            motor.Rotate(-1 * data.rotateSpeed);
            if (CanMove())
            {
                avoidence = Avoidence.Moving;
                exitTime = avoidTime;
            }
        }
        else if (avoidence == Avoidence.Moving)
        {
            if (CanMove())
            {
                exitTime -= Time.deltaTime;
                motor.Move(data.moveSpeed);
                if (exitTime <= 0)
                {
                    avoidence = Avoidence.NotAvoiding;
                }
            }
            else
            {
                avoidence = Avoidence.Rotating;
            }
        }
    }

    private void Chase()
    {
        if (CanMove())
        {
            if (motor.RotateToward(Player.transform.position, data.rotateSpeed))
            {
                //Do Nothing, Is Rotating
            }
            else
            {
                motor.Fire();
                motor.Move(data.moveSpeed);
            }
        }
        else
        {
            avoidence = Avoidence.Rotating;
        }
    }

    private void Flee()
    {
        if (CanMove())
        {
            if (motor.RotateToward(this.gameObject.transform.position - Player.transform.position, data.rotateSpeed))
            {
                //Do Nothing, Is Rotating
            }
            else
            {
                motor.Move(data.moveSpeed);
            }
        }
        else
        {
            avoidence = Avoidence.Rotating;
        }
    }

    private bool CanMove()
    {
        RaycastHit hit;
        Ray frontRay = new Ray(this.gameObject.transform.position, this.gameObject.transform.forward);
        if (Physics.Raycast(frontRay, out hit, fovDistance))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                return false;
            }
        }
        return true;
    }

    private bool CanSeePlayer()
    {
        if (GameManager.Instance.playerOne != null)
        {
            RaycastHit hit;
            Ray playerOneRay = new Ray(this.gameObject.transform.position, GameManager.Instance.playerOne.transform.position - this.gameObject.transform.position);
            if (Physics.Raycast(playerOneRay, out hit, fovDistance))
            {
                Debug.DrawLine(this.gameObject.transform.position, hit.point);
                if (hit.collider.CompareTag("Player"))
                {
                    Player = hit.collider.gameObject;
                    return true;
                }
            }
        }
        if (GameManager.Instance.twoPlayers)
        {
            if (GameManager.Instance.playerTwo != null)
            {
                RaycastHit hit2;
                Ray playerTwoRay = new Ray(this.gameObject.transform.position, GameManager.Instance.playerTwo.transform.position - this.gameObject.transform.position);
                if (Physics.Raycast(playerTwoRay, out hit2, fovDistance))
                {
                    Debug.DrawLine(this.gameObject.transform.position, hit2.point);
                    if (hit2.collider.CompareTag("Player"))
                    {
                        Player = hit2.collider.gameObject;
                        return true;
                    }
                }
            }
        }
        return false;
    }





    private void PingPong()
    {
        if (isPatrolForward == true)
        {
            if (currentWaypoint < waypoints.Length - 1)
            {
                currentWaypoint++;
            }
            else
            {
                isPatrolForward = false;
                currentWaypoint--;
            }
        }
        else
        {
            if (currentWaypoint > 0)
            {
                currentWaypoint--;
            }
            else
            {
                isPatrolForward = true;
                currentWaypoint++;
            }
        }
    }

    private void Loop()
    {
        if (currentWaypoint == waypoints.Length - 1)
        {
            currentWaypoint = 0;
        }
        else
        {
            currentWaypoint++;
        }
    }
}
