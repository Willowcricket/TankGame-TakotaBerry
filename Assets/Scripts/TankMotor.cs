using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(TankData))]
public class TankMotor : MonoBehaviour
{
    //Charatcer Controller Comp. Ref.
    private CharacterController characterController;
    private TankData data;
    private Transform tf;


    private void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        data = gameObject.GetComponent<TankData>();
        tf = gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        Timer();
        HealthCheck();
    }

    //Chech The Tanks Health
    private void HealthCheck()
    {
        if (data.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //Handle Moving Tank
    public void Move(float speed)
    {
        //Speed Data Vector, Points Forward w/ GameObj, Is A Length of Speed
        Vector3 speedVector = tf.forward * speed;
        
        //Handles Moving
        characterController.SimpleMove(speedVector);
    }

    //Handle Rotating Tank
    public void Rotate(float speed)
    {
        Vector3 rotateVector = Vector3.up * speed * Time.deltaTime;
        tf.Rotate(rotateVector, Space.Self);
    }

    //Handle Shooting
    public void Fire()
    {
        if (data.readyToFire == true)
        {
            Instantiate(data.bulletPreFab, data.firePoint.position, data.firePoint.rotation);
            data.readyToFire = false;
            data.reloaded = data.reloadDelay;
        }
    }

    //Handle Reloading Of Tank
    private void Timer()
    {
        if (data.reloaded > 0)
        {
            data.reloaded -= Time.deltaTime;
        }
        else if (data.reloaded <= 0)
        {
            if (data.readyToFire == false)
            {
                data.readyToFire = true;
            }
        }
    }
}
