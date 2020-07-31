using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankData : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float backwardMoveSpeed = 3.0f;
    public float rotateSpeed = 90.0f;

    public Transform firePoint;
    public GameObject bulletPreFab;

    public float reloadDelay = 3.0f;
    public float reloaded = 0.0f;
    public bool readyToFire = true;

    public float maxHealth = 100.0f;
    public float currentHealth = 100.0f;

    public Camera camera;

    public Text PlText;
    public Text PhText;
}
