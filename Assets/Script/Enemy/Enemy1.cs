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

    public static Action OnEndReached = null;

    public WayPoints WayPoints { get; set; }//[SerializeField] private WayPoints waypoint;

    public Vector3 CurrentPointPosition => WayPoints.GetWaypointPosition(_currentWaypointIndex);

    private void Start()
    {
        _currentWaypointIndex = 0;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }





    private void Update()
    {
        Move();
        if (IsArrived())
        {
            UpdateCurrentPointIndex();
        }
    }

    public override void Move()
    {

        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, moveSpeed * Time.deltaTime);


    }

    private bool IsArrived()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            return true;
        }
        return false;
    }
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




    public override void Attack()
    {
        
    }

    public override void Hurt()
    {
        
    }

    public override void Death()
    {
        
    }
}
