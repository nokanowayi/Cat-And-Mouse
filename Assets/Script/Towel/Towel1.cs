using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Pool;
using System.Linq;

public class Towel1 : Towel
{
    public ObjectPool<GameObject> gunPool;
    public GameObject[] enemies;
    public GameObject nowEnemy;
    public GameObject gun;
    private float oneEnemyDistance;
    private float minEnemyDistance;
    public float attackRange;
    public float waitTime;
    public float waitTimeCounter;
    public bool isAttacking;

    private void Awake()
    {
        gunPool = new ObjectPool<GameObject>(CreatPool,GetPool,ReleasePool,DestroyPool,true,10,100);
        minEnemyDistance = attackRange+1;
    }
    //对象池相关函数
    public void DestroyPool(GameObject obj)
    {
        Destroy(obj);
    }

    public void GetPool(GameObject obj)
    {
        obj.transform.position = transform.position;
        obj.SetActive(true);
    }

    public void ReleasePool(GameObject obj)
    {
        obj.SetActive(false);   
    }

    public GameObject CreatPool()
    {
        var obj = Instantiate(gun,transform.position,Quaternion.identity);
        obj.GetComponent<Gun>().pool = gunPool;
        obj.GetComponent<Gun>().enemy = nowEnemy;
        return obj;
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (nowEnemy != null)
        {
            if (!nowEnemy.activeSelf)
            {
                GameObject[] newEnmeies = enemies.Where(x => x != nowEnemy).ToArray();
                enemies = newEnmeies;
            } 
        }

        foreach (var enemy in enemies)
        {
            oneEnemyDistance = CountDistance(enemy.transform);
            if (oneEnemyDistance<minEnemyDistance)
            {
                minEnemyDistance = oneEnemyDistance;
                nowEnemy = enemy;
            }
        }
        if (attackRange>CountDistance(nowEnemy.transform)) 
        { 
            Attack(); 
            isAttacking = true;
        }
        if (isAttacking)
        {
            WaitTimeCounter();
        }
    }
    //敌人距离
    public float CountDistance(Transform nowEnemyTransform)
    {
        float rangeX = 0;
        float rangeY = 0;
        float realRange = 0;
        rangeX = transform.position.x-nowEnemyTransform.position.x;
        rangeY = transform.position.x-nowEnemyTransform.position.y;
        realRange = Mathf.Sqrt( rangeX * rangeX+rangeY * rangeY);
        return realRange;
    }
    //攻击间隔
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
        if (!isAttacking)
        {
            gunPool.Get();
        }
    }

    public override void Hurt()
    {
        
    }

    public override void Death()
    {
        
    }
}
