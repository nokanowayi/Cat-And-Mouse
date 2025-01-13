using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CostManeger : MonoBehaviour
{
    public static CostManeger instance;
    public TMP_Text costNumber;
    public NumberSO costSO;

    private void Awake()
    {
       instance = this;
       costNumber.text = "50";
       costSO.number = 50;
    }

    public void ChangeCost(int cost)
    {
        int currentCost = int.Parse(costNumber.text);
        costNumber.text = (currentCost+cost).ToString();
        costSO.number = currentCost+cost;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
