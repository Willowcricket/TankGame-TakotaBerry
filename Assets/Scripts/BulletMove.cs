using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float timerDelay = 3.0f;
    private float timerUntilNextEvent = 0.0f;
    public Transform tf;
    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        timerUntilNextEvent = timerDelay;
    }

    // Update is called once per frame
    void Update()
    {
        tf.position += tf.forward * speed * Time.deltaTime;

        if (timerUntilNextEvent > 0)
        {
            timerUntilNextEvent -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Timer has Expired.");
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Die();
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Debug.Log("Bullet Has Hit Something");
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
