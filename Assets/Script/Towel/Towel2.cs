using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towel2 : Towel
{
    public BoolSO isIspector;
    public int cost;
    public float waitTime;
    public float waitTimeCounter;

    private void Awake()
    {
       currentHealth = towelData.maxHealth; 
    }

    private void Update()
    {
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

    public override void LevelUp()
    {
        level++;
        TowelInspector.instance.UpdateData();
        Debug.Log("level up");
    }

    public override void OnTowelClick()
    {
        isIspector.isDone = true;
        TowelInspector.instance.OnTowelClick(towelData,level,currentHealth);
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

    public void WaitTimeCounter()
    {
        waitTimeCounter += Time.deltaTime;
        if (waitTimeCounter >= waitTime)
        {
            waitTimeCounter = 0;
            CostManeger.instance.ChangeCost(cost);
        }
    }
}
