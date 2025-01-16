using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Towel3 : Towel
{
    public float waitTimeCounter;
    public float summonTime;
    public bool isNeedSummon;
    public int summonCounter;
    public ObjectPool<GameObject> soilderPool;
    public List<GameObject> soilders = new List<GameObject>();
    public GameObject soilder;

    private void Awake()
    {
        soilderPool = new ObjectPool<GameObject>(CreateFunc, GetPool, ReleasePool, DestroyPool, true, 3, 4);
    }

    public override void LevelUp()
    {
        level++;
        TowelInspector.instance.level = level;
        TowelInspector.instance.UpdateData();
        Debug.Log("level up"); 
    }
//对象池相关函数
    public void DestroyPool(GameObject obj)
    {
        Destroy(obj);
    }

    public void GetPool(GameObject obj)
    {
        obj.SetActive(true);
        obj.GetComponent<Soldiers>().father = this.gameObject;
        soilders.Add(obj);
        obj.transform.position = new Vector3(transform.position.x-(float)1+(float)soilders.Count/2, transform.position.y-(float)1.5, transform.position.z);
    }

    public void ReleasePool(GameObject obj)
    {
        obj.SetActive(false);   
        soilders.Remove(obj);
        summonCounter--;
    }

    public GameObject CreateFunc()
    {
        var obj = Instantiate(soilder,transform.position,Quaternion.identity);
        obj.GetComponent<Soldiers>().soilPool = soilderPool;
        return obj;
    }
    private void Update()
    {
        GetISNeedSummon();
       WaitTimeCounter(); 
       if (Input.GetMouseButtonDown(0)) // 检测鼠标左键是否按下
       {
           RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
           if (hit.collider != null && hit.collider.gameObject == gameObject) // 检测是否点击到当前物体
           {
               OnTowelClick();
           }
       } 
    }

    public override void OnTowelClick()
    {
        isInspector.isDone = true;
        TowelInspector.instance.OnTowelClick(towelData,level,currentHealth,this.gameObject);
    }
    public void WaitTimeCounter()
    {
        waitTimeCounter += Time.deltaTime;
        if (waitTimeCounter>summonTime&isNeedSummon)
        {
            waitTimeCounter = 0;
            Attack(); ;
        }
    }
    public override void Attack()
    {
        soilderPool.Get();
    }

    public void GetISNeedSummon()
    {
        if (summonCounter>soilders.Count)
        {
            isNeedSummon = true;
        }
        else
        {
            isNeedSummon = false;
        }
    }
    
    public override void Hurt()
    {
        
    }

    public override void Death()
    {
        
    }
}
