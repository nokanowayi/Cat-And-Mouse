using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    public GameObject enemy;
    public GameObject towel;
    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 direction; 
    public int speed;
    public ObjectPool<GameObject> pool;
    

    private void Update()
    {
        CountDirection();
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
         pool.Release(gameObject);   
    }
    

    public void CountDirection()
    {
        float rangeX = 0;
        float rangeY = 0;
        rangeX = -(transform.position.x-enemy.transform.position.x);
        rangeY = -(transform.position.x-enemy.transform.position.y);
        direction = new Vector2(rangeX,rangeY).normalized;
        //transform.rotation = new Quaternion(0,0,Mathf.Asin(direction.y),0);
    }

    public void Move()
    {
        rb.velocity = direction.normalized*speed;
    }
}
