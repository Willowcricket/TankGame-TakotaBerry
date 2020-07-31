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
        PlayerOneOrTwo();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (this.gameObject.tag == "Player")
        {
            if (GameManager.Instance.playerOne == this.gameObject)
            {
                data.PlText.text = "Lives: " + GameManager.Instance.playerOneLives;
                data.PhText.text = "Health: " + data.currentHealth + "/" + data.maxHealth;
            }
            else if (GameManager.Instance.playerTwo == this.gameObject)
            {
                data.PlText.text = "Lives: " + GameManager.Instance.playerTwoLives;
                data.PhText.text = "Health: " + data.currentHealth + "/" + data.maxHealth;
            }
        }
    }

    private void PlayerOneOrTwo()
    {
        if (this.gameObject.tag == "Player")
        {
            if (GameManager.Instance.playerOne == null)
            {
                GameManager.Instance.playerOne = this.gameObject;
            }
            if (GameManager.Instance.playerOne != this.gameObject)
            {
                if (GameManager.Instance.playerTwo == null)
                {
                    GameManager.Instance.playerTwo = this.gameObject;
                    this.gameObject.GetComponent<InputManager>().input = InputManager.InputScheme.arrowKeys;
                }
            }
        }
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

    public bool RotateToward(Vector3 target, float speed)
    {
        Vector3 vectorToTarget = target - this.gameObject.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget);

        if (targetRotation == this.gameObject.transform.rotation)
        {
            return false;
        }

        this.gameObject.transform.rotation = Quaternion.RotateTowards(this.gameObject.transform.rotation, targetRotation, speed * Time.deltaTime);
        return true;
    }

    //Handle Shooting
    public void Fire()
    {
        if (data.readyToFire == true)
        {
            Instantiate(data.bulletPreFab, data.firePoint.position, data.firePoint.rotation);
            data.readyToFire = false;
            data.reloaded = data.reloadDelay;
            GameManager.Instance.GetComponent<AudioManager>().PlayFX(1);
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

    private void OnDestroy()
    {
        if (this.gameObject.tag == "Player")
        {
            if (GameManager.Instance.playerOne == this.gameObject)
            {
                if (GameManager.Instance.playerOneLives > 0)
                {
                    GameManager.Instance.SpawnPlayer();
                    GameManager.Instance.playerOneLives--;
                }
            }
            if (GameManager.Instance.playerTwo == this.gameObject)
            {
                if (GameManager.Instance.playerTwoLives > 0)
                {
                    GameManager.Instance.SpawnPlayer();
                    GameManager.Instance.playerTwoLives--;
                }
            }
        }
        GameManager.Instance.GetComponent<AudioManager>().PlayFX(3);
    }
}
