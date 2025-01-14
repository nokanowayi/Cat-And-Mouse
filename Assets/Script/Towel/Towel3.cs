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
    public List<GameObject> soilders;
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
        obj.transform.position = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
    }

    public void ReleasePool(GameObject obj)
    {
        obj.SetActive(false);   
    }

    public GameObject CreateFunc()
    {
        var obj = Instantiate(soilder,transform.position,Quaternion.identity);
        obj.GetComponent<Soilders>().soilPool = soilderPool;
        return obj;
    }
    private void Update()
    {
        GetISNeedSummon();
       WaitTimeCounter(); 
    }

    public override void OnTowelClick()
    {
        isInspector.isDone = true;
        TowelInspector.instance.OnTowelClick(towelData,level,currentHealth);
    }
    public void WaitTimeCounter()
    {
        waitTimeCounter += Time.deltaTime;
        if (waitTimeCounter>summonTime&isNeedSummon)
        {
            waitTimeCounter = 0;
            Attack();
            summonCounter++;
        }
    }
    public override void Attack()
    {
        soilderPool.Get();
    }

    public void GetISNeedSummon()
    {
        if (summonCounter<4)
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
