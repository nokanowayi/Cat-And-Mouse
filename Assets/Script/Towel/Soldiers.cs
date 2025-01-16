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
        foreach (var enemy in enemies)
        {
            //Debug.Log(minEnemyDistance);
            oneEnemyDistance = CountDistance(enemy.transform);
            if (oneEnemyDistance < minEnemyDistance)
            {
                minEnemyDistance = oneEnemyDistance;
                nowEnemy = enemy;
                Debug.Log(nowEnemy.name);
            }
            if (!enemy.activeSelf)
            {
                GameObject[] newEnmeies = enemies.Where(x => x != nowEnemy).ToArray();
                enemies = newEnmeies;
            }
        } 
        if (nowEnemy != null)
        {
            //nowEnemy.GetComponent<Enemy1>().isAttacking = true;
            if (nowEnemy.activeSelf)
            {
                if (CountDistance(nowEnemy.transform)>chaseRange)
                {
                    //nowEnemy.GetComponent<Enemy1>().isAttacking = false;
                    nowEnemy = null;
                    minEnemyDistance = chaseRange + 1;
                    Debug.Log("go");
                }
                else if (nowEnemy != null && chaseRange > CountDistance(nowEnemy.transform)&CountDistance(nowEnemy.transform)>attackRange)
                {
                    Debug.Log("ok");
                    Move(nowEnemy.transform.position);
                    soldierAnimation.isWalk = true;
                    if (direction.x<0)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                }
                else 
                {
                    if (direction.x<0)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    Debug.Log("attack");
                    Attack();
                }

            }
            else
            {
                //nowEnemy.GetComponent<Enemy1>().isAttacking = false;
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
                if (j == this.gameObject)
                {
                    fatherPos.x = fatherPos.x - (float)1 + (float)i/2;
                }
            }

            if (Mathf.Abs(fatherPos.x-this.gameObject.transform.position.x)>0.001&&Mathf.Abs(fatherPos.y-this.gameObject.transform.position.y)>0.001)
            {
                Debug.Log("budao");
                Move(fatherPos);
                soldierAnimation.isWalk = true;
            }
            else
            {
                soldierAnimation.isWalk = false;
            }
        }
    }
    
    public float CountDistance(Transform nowEnemyTransform)
    {
        float rangeX = 0;
        float rangeY = 0;
        float realRange = 0;
        rangeX = this.gameObject.transform.position.x-nowEnemyTransform.position.x;
        rangeY = this.gameObject.transform.position.y-nowEnemyTransform.position.y;
        //Debug.Log(rangeX+","+rangeY);
        realRange = Mathf.Sqrt( rangeX * rangeX+rangeY * rangeY);
        return realRange;
    }

    public void Move(Vector3 nowEnemyTransform)
    {
        Vector3 distance = nowEnemyTransform - transform.position; 
        float angle = (float)(Mathf.Atan2(distance.y, distance.x) * 180/Mathf.PI);
        direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),0).normalized;
        transform.position += direction * speed * Time.deltaTime; 
    }
    
    public void Attack()
    {
        soldierAnimation.SetAnimationTrigger();
    }
    
    public void Death()
    {
        //nowEnemy.GetComponent<Enemy1>().isAttacking = false;
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
