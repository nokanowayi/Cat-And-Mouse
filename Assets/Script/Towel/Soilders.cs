using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System.Linq;

public class Soilders : MonoBehaviour
{
    public ObjectPool<GameObject> soilPool;
    private float oneEnemyDistance;
    private float minEnemyDistance;
    public float chaseRange;
    public float attackRange;
    public float maxHealth;
    public float curentHealth;
    public float damage; 
    public float speed;
    public Vector3 direction;
    public GameObject[] enemies;
    public GameObject nowEnemy;
    
    private void Awake()
    {
        minEnemyDistance = chaseRange+1; 
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (nowEnemy != null)
        {
            if (nowEnemy.activeSelf)
            {
                if (CountDistance(nowEnemy.transform)>chaseRange)
                {
                    nowEnemy = null;
                    minEnemyDistance = attackRange + 1;
                }

                if (nowEnemy != null && chaseRange > CountDistance(nowEnemy.transform)&CountDistance(nowEnemy.transform)>attackRange)
                {
                    Move();
                    if (direction.x<0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }
                else
                {
                    Attack();
                }

            }
            else
            {
                nowEnemy = null;
                minEnemyDistance = chaseRange + 1;
            }   
        } 
        foreach (var enemy in enemies)
        {
            oneEnemyDistance = CountDistance(enemy.transform);
            if (oneEnemyDistance < minEnemyDistance)
            {
                minEnemyDistance = oneEnemyDistance;
                nowEnemy = enemy;
            }
            if (!enemy.activeSelf)
            {
                GameObject[] newEnmeies = enemies.Where(x => x != nowEnemy).ToArray();
                enemies = newEnmeies;
            }
        } 
    }
    
    public float CountDistance(Transform nowEnemyTransform)
    {
        float rangeX = 0;
        float rangeY = 0;
        float realRange = 0;
        rangeX = transform.position.x-nowEnemyTransform.position.x;
        rangeY = transform.position.x-nowEnemyTransform.position.y;
        realRange = Mathf.Sqrt( rangeX * rangeX+rangeY * rangeY);
        Vector3 distance = nowEnemyTransform.position - transform.position; 
        float angle = (float)(Mathf.Atan2(distance.y, distance.x) * 180/Mathf.PI);
        direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0).normalized;
        return realRange;
    }

    public void Move()
    {
        transform.position += direction * speed * Time.deltaTime; 
    }
    
    public void Attack()
    {
        
    }
    
    public void Death()
    {
        soilPool.Release(gameObject);
    }
    
    public void TakeDamage(float damage)
    {
        curentHealth -= damage;
        if (curentHealth<0)
        {
            Death();
        }
    }
}
