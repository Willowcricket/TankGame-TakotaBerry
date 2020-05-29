using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float backwardMoveSpeed = 3.0f;
    public float rotateSpeed = 90.0f;
    public float bulletDamage = 5.0f;
    public Transform firePoint;
    public GameObject bulletPreFab;
}
