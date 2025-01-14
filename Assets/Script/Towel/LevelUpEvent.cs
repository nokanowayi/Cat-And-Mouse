using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpEvent: MonoBehaviour
{
    public GameObject father;
    public NumberSO cost;
    public BoolSO isInspector;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 检测鼠标左键是否按下
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject) // 检测是否点击到当前物体
            {
                if (isInspector.isDone&cost.number>father.GetComponent<Towel>().towelData.costNeeded&father.GetComponent<Towel>().level==1)
                {
                    father.GetComponent<Towel>().LevelUp();
                    CostManeger.instance.ChangeCost(-father.GetComponent<Towel>().towelData.costNeeded);
                }
            }
        } 
    }
}
