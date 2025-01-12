using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    public GameObject enemy;
    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 direction; 
    public int speed;
    public float waitTime;
    public float waitTimeCounter=0;
    public ObjectPool<GameObject> pool;

    private void Update()
    {
        TurnAngle();
        WaitTimeCounter();
        Move();
    }
    //子弹碰撞消失
    private void OnTriggerEnter2D(Collider2D other)
    {
       pool.Release(gameObject); 
    }

    //子弹自动消失时间
    public void WaitTimeCounter()
    {
        waitTimeCounter += Time.deltaTime;
        if (waitTimeCounter>waitTime)
        {
            waitTimeCounter = 0;
            pool.Release(gameObject);
        }
    } 
    //子弹转动
    public void TurnAngle()
    {
       Vector3 distance = enemy.transform.position - transform.position; 
       float angle = (float)(Mathf.Atan2(distance.y, distance.x) * 180/Mathf.PI);
       transform.rotation = Quaternion.Euler(0,0,angle+90);
       direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }


    public void Move()
    {
        rb.velocity = direction.normalized*speed;
    }
}
