using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    public TowelSO towel;
    public GameObject enemy;
    public Animator animator;
    public Vector3 direction;
    public int damage;
    public int speed;
    public float waitTime;
    private float waitTimeCounter;
    public ObjectPool<GameObject> pool;

    private void Awake()
    {
        speed = towel.speed;
        damage = towel.damage;
        speed = towel.speed;
        waitTime = towel.bulletDisposeTime;;
    }

    private void Update()
    {
        TurnAngle();
        WaitTimeCounter();
        Move();
        if (!enemy.activeSelf)
        {
            Debug.Log("hi");
            pool.Release(gameObject);
            waitTimeCounter = 0;
        }
    }
    //子弹碰撞消失
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject enterEnemy = other.gameObject; 
            if (enterEnemy == enemy)
            {
                pool.Release(gameObject);
            }
            other.GetComponent<Enemy1>().TakeDamage(damage);
        }
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
       direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0).normalized;
    }


    public void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
