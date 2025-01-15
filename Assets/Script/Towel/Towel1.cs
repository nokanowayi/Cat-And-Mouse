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
    public float attackInterval;
    public float waitTimeCounter;
    public bool isAttacking;
    private void Awake()
    {
        level = 1;
        health = towelData.maxHealth;
        attackRange = towelData.attackRange;
        attackInterval = towelData.attackInterval;
        towelData.position = transform.position;
        gunPool = new ObjectPool<GameObject>(CreatPool,GetPool,ReleasePool,DestroyPool,true,100,1000);
        minEnemyDistance = attackRange+1;
    }
    //对象池相关函数
    public void DestroyPool(GameObject obj)
    {
        Destroy(obj);
    }

    public void GetPool(GameObject obj)
    {
        //Debug.Log(nowEnemy.activeSelf);
        obj.GetComponent<Gun>().enemy = nowEnemy;
        obj.transform.position = transform.position;
        obj.SetActive(true);
    }

    public void ReleasePool(GameObject obj)
    {
        obj.SetActive(false);   
        obj.GetComponent<Gun>().enemy = null;
    }

    public GameObject CreatPool()
    {
        var obj = Instantiate(gun,transform.position,Quaternion.identity);
        obj.GetComponent<Gun>().pool = gunPool;
        return obj;
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (nowEnemy != null)
        {
            if (nowEnemy.activeSelf)
            {
                if (CountDistance(nowEnemy.transform)>attackRange)
                {
                    nowEnemy = null;
                    minEnemyDistance = attackRange + 1;
                }

                if (nowEnemy != null && attackRange > CountDistance(nowEnemy.transform))
                {
                    if (!isAttacking)
                    {
                        Attack();
                    }
                    isAttacking = true;
                }

                if (isAttacking)
                {
                    WaitTimeCounter();
                }
            }
            else
            {
                nowEnemy = null;
                minEnemyDistance = attackRange + 1;
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
        if (Input.GetMouseButtonDown(0)) // 检测鼠标左键是否按下
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject) // 检测是否点击到当前物体
            {
                OnTowelClick();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10.0f;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Debug.Log("World Position: " + worldPosition);
        }
    }

    public override void LevelUp()
    {
        level++;
        TowelInspector.instance.level = level;
        TowelInspector.instance.UpdateData();
        Debug.Log("level up");
    }
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
        if (waitTimeCounter>attackInterval)
        {
            waitTimeCounter = 0;
            isAttacking = false;
        }
    }

    public override void OnTowelClick()
    {
        isInspector.isDone = true;
        TowelInspector.instance.OnTowelClick(towelData,level,currentHealth);
    }

    public override void Attack()
    { 
        gunPool.Get();
    }

    public override void Hurt()
    {
        
    }

    public override void Death()
    {
        
    }
}
