using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System.Linq;

public class Soldiers : MonoBehaviour
{
    public ObjectPool<GameObject> soilPool;
    public SoldierAnimation soldierAnimation;
    public bool isMove;
    private float oneEnemyDistance;
    private float minEnemyDistance;
    public float chaseRange;
    public float attackRange;
    public float maxHealth;
    public float curentHealth;
    public float damage; 
    public float speed;
    public Vector3 direction;
    public GameObject father;
    public GameObject[] enemies;
    public GameObject nowEnemy;
    
    private void Awake()
    {
        minEnemyDistance = chaseRange+1; 
        curentHealth = maxHealth;
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (nowEnemy != null)
        {
            if (nowEnemy.activeSelf)
            {
                if (CountDistance(nowEnemy.transform.position)>chaseRange)
                {
                    nowEnemy = null;
                    minEnemyDistance = chaseRange + 1;
                }

                if (nowEnemy != null && chaseRange > CountDistance(nowEnemy.transform.position)&CountDistance(nowEnemy.transform.position)>attackRange)
                {
                    Move();
                    soldierAnimation.isWalk = true;
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
                    soldierAnimation.isWalk = false;
                }

            }
            else
            {
                nowEnemy = null;
                minEnemyDistance = chaseRange + 1;
            }   
        }
        else
        {
            Vector3 fatherPos = new Vector3(father.transform.position.x, father.transform.position.y-(float)1.5, father.transform.position.z);
            for (int i = 0; i < father.GetComponent<Towel3>().soilders.Count; i++)
            {
                GameObject j = father.GetComponent<Towel3>().soilders[i];
                if (j == this)
                {
                    fatherPos.x = fatherPos.x - (float)1 + (float)i/2;
                }
            }

            if (Mathf.Abs(fatherPos.x)<0.1&&Mathf.Abs(fatherPos.y)<0.1)
            {
                CountDistance(fatherPos);
                Move();
                soldierAnimation.isWalk = true;
            }
            else
            {
                soldierAnimation.isWalk = false;
            }
        }

        foreach (var enemy in enemies)
        {
            oneEnemyDistance = CountDistance(enemy.transform.position);
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
    
    public float CountDistance(Vector3 nowEnemyTransform)
    {
        float rangeX = 0;
        float rangeY = 0;
        float realRange = 0;
        rangeX = transform.position.x-nowEnemyTransform.x;
        rangeY = transform.position.x-nowEnemyTransform.y;
        realRange = Mathf.Sqrt( rangeX * rangeX+rangeY * rangeY);
        Vector3 distance = nowEnemyTransform - transform.position; 
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
