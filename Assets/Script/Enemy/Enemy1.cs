using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    public Rigidbody2D rb;
    public Animator anim;
    public Vector2 direction;
    private Vector3 _targetPosition;//目标位置
    public Spawner spawner;

    private float attackTimer = 0f; // 攻击计时器
    //private Towel nearestTower; // 最近的防御塔
    private Soldiers nearestSoldiers; // 最近的士兵
   

    public static Action OnEndReached = null;//敌人到达终点事件
    public static Action OnEnemyDeath = null;//敌人死亡事件

    public WayPoints WayPoints { get; set; }//[SerializeField] private WayPoints waypoint;
    public Vector3 CurrentPointPosition => WayPoints.GetWaypointPosition(_currentWaypointIndex);



    private void Start()
    {
        _currentWaypointIndex = 0;
        currentHealth = intialHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spawner = FindObjectOfType<Spawner>(); // 在场景中查找 Spawner 组件
        if (WayPoints != null && WayPoints.Points.Length > 0)
        {
            _targetPosition = WayPoints.GetWaypointPosition(_currentWaypointIndex);
            //Debug.Log("初始目标位置: " + _targetPosition);
        }
        else
        {
            Debug.LogError("WayPoints 未正确设置或路径点为空");
        }

    }


    private void Update()
    {
        
        nearestSoldiers = FindNearestSoldiers();
        if (nearestSoldiers != null)
        {
            Debug.Log("找到最近的士兵: " + nearestSoldiers.name);
        }
        else
        {
            Debug.Log("没有找到士兵");
        }
        attackTimer += Time.deltaTime;

        if (nearestSoldiers != null)
        {
            float distanceToSoldier = Vector3.Distance(transform.position, nearestSoldiers.transform.position);

            if (distanceToSoldier <= attackRange)
            {
                if (attackTimer >= attackInterval)
                {
                    Attack(nearestSoldiers);
                    attackTimer = 0f;
                }
            }
            else
            {
                MoveTowardsSoldier(nearestSoldiers);
            }
        }
        else
        {
            Move();
            if (IsArrived())
            {
                UpdateCurrentPointIndex();
            }
        }

        //if (attackTimer >= attackInterval)
        //{
        //    Attack();
        //    attackTimer = 0f;
        //}

    }


    /// <summary>
    /// 敌人移动
    /// </summary>
    public override void Move()
    {
        //if (WayPoints == null || WayPoints.Points.Length == 0) return;
        Vector3 direction = (_targetPosition - transform.position).normalized;//敌人移动方向
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, moveSpeed * Time.deltaTime);//敌人移动

        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // 向左翻转
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // 向右翻转
        }


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
            _targetPosition = WayPoints.GetWaypointPosition(_currentWaypointIndex);
            //Debug.Log("更新目标位置: " + _targetPosition);
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
        _currentWaypointIndex = 0;
        ObjectPooler.ReturnToPool(gameObject);
    }
    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
        if (WayPoints != null && WayPoints.Points.Length > 0)
        {
            _targetPosition = WayPoints.GetWaypointPosition(_currentWaypointIndex);
        }
    }




    /// <summary>
    /// 敌人攻击
    /// </summary>
    public override void Attack(Soldiers soldiers)
    {
        soldiers.TakeDamage(1);
    }

    private Soldiers FindNearestSoldiers()
    { 
        Soldiers[] soldiers = FindObjectsOfType<Soldiers>();
        float minDistance = float.MaxValue;
        nearestSoldiers = null;

        foreach (Soldiers soldier in soldiers)
        {
            float distance = Vector3.Distance(transform.position, soldier.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestSoldiers = soldier;
            }
            return nearestSoldiers;
        }
        return null;

    }
    private void MoveTowardsSoldier(Soldiers soldier)
    {
        Vector3 direction = (soldier.transform.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, soldier.transform.position, moveSpeed * Time.deltaTime);

        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // 向左翻转
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // 向右翻转
        }
    }


















    /// <summary>
    /// 攻击最近的塔
    /// </summary>
    //private void AttackNearestTower()
    //{
    //    nearestTower = FindNearestTower();
    //    if (nearestTower != null)
    //    {
    //        // 执行攻击逻辑，例如减少防御塔的生命值
    //        //nearestTower.TakeDamage(1);
    //    }
    //}
    //private Towel FindNearestTower()
    //{
    //    Towel[] towers = FindObjectsOfType<Towel>();
    //    Towel nearest = null;
    //    float minDistance = float.MaxValue;

    //    foreach (Towel tower in towers)
    //    {
    //        float distance = Vector3.Distance(transform.position, tower.transform.position);
    //        if (distance < minDistance)
    //        {
    //            minDistance = distance;
    //            nearest = tower;
    //        }
    //    }

    //    return nearest;
    //}



    /// <summary>
    /// 受伤
    /// </summary>
    public override void Hurt()
    {
        anim.SetTrigger("Hurt");
       
    }
    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            
            Death();
        }
        else
        {
            Hurt();
        }
    }





    /// <summary>
    /// <summary>
    /// 死亡
    /// </summary>
    public override void Death()
    {
        anim.SetTrigger("Death");
        OnEnemyDeath?.Invoke();
        currentHealth = intialHealth;
        _currentWaypointIndex = 0;

        ObjectPooler.ReturnToPool(gameObject);
    }
}
