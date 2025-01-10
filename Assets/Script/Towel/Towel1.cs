using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towel1 : Towel
{
    public GameObject gun;
    public GameObject enemy;
    public float attackRange;
    public Vector2 direction;
    public float waitTime;
    public float waitTimeCounter;
    public bool isAttacking;
    private void Update()
    {
        if (enemy !=null)
        {
            if (attackRange<CountRange()&!isAttacking)
            {isAttacking = true;
             Attack();
             WaitTimeCounter();
            }
        }
    }

    public float CountRange()
    {
        float rangeX = 0;
        float rangeY = 0;
        float realRange = 0;
        rangeX = Mathf.Abs(transform.position.x-enemy.transform.position.x);
        rangeY = Mathf.Abs(transform.position.x-enemy.transform.position.y);
        direction = new Vector2(rangeX,rangeY).normalized;
        realRange = rangeX * rangeX+rangeY * rangeY;
        return realRange;
    }

    public void WaitTimeCounter()
    {
        waitTimeCounter += Time.deltaTime;
        if (waitTimeCounter>waitTime)
        {
            waitTimeCounter = 0;
            isAttacking = false;
        }
    } 
        
    public override void Attack()
    {
        Instantiate(gun,transform.position,Quaternion.identity);
    }

    public override void Hurt()
    {
        
    }

    public override void Death()
    {
        
    }
}
