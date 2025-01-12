using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    public Rigidbody2D rb;
    public Animator anim;
    public float moveSpeed = 3f;
    public Vector2 direction;
    public Spawner spawner;


    private float attackTimer = 0f; // 攻击计时器
    private Towel nearestTower; // 最近的防御塔

    public static Action OnEndReached = null;//敌人到达终点事件

    public WayPoints WayPoints { get; set; }//[SerializeField] private WayPoints waypoint;

    public Vector3 CurrentPointPosition => WayPoints.GetWaypointPosition(_currentWaypointIndex);



    private void Start()
    {
        _currentWaypointIndex = 0;
        currentHealth = intialHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spawner = GetComponent<Spawner>();
    }


    private void Update()
    {
        Move();
        if (IsArrived())
        {
            UpdateCurrentPointIndex();
        }

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            Attack();
            attackTimer = 0f;
        }

    }


    /// <summary>
    /// 敌人移动
    /// </summary>
    public override void Move()
    {

        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, moveSpeed * Time.deltaTime);


    }

    //判断敌人是否到达当前路线点
    private bool IsArrived()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            return true;
        }
        return false;
    }
    //更新当前路线点索引
    private void UpdateCurrentPointIndex() { 
        int lastIndex = WayPoints.Points.Length - 1;
        if (_currentWaypointIndex < lastIndex)
        {
            _currentWaypointIndex++;
        }
        else
        {
            ReturnEnermyToPool();
        }
    }

    //敌人到达终点后
    private void ReturnEnermyToPool() 
    {
        
        OnEndReached?.Invoke();
        
        ObjectPooler.ReturnToPool(gameObject);
    }
    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }



    /// <summary>
    /// 敌人攻击
    /// </summary>
    public override void Attack()
    {
        AttackNearestTower();
    }
    private void AttackNearestTower()
    {
        nearestTower = FindNearestTower();
        if (nearestTower != null)
        {
            // 执行攻击逻辑，例如减少防御塔的生命值
            //nearestTower.TakeDamage(1);
        }
    }
    private Towel FindNearestTower()
    {
        Towel[] towers = FindObjectsOfType<Towel>();
        Towel nearest = null;
        float minDistance = float.MaxValue;

        foreach (Towel tower in towers)
        {
            float distance = Vector3.Distance(transform.position, tower.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = tower;
            }
        }

        return nearest;
    }



    /// <summary>
    /// 受伤
    /// </summary>
    public override void Hurt()
    {
        anim.SetTrigger("Hurt");
        TakeDamage(1);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            spawner._enemiesRemaining--;
            Death();
        }
        else
        {
            Hurt();
        }
    }





    /// <summary>
    /// 死亡
    /// </summary>
    public override void Death()
    {
        anim.SetTrigger("Death");
        currentHealth = intialHealth;
        
        ObjectPooler.ReturnToPool(gameObject);
    }
}
