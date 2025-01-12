using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public BoolSO isPlant;
    public GameObject cardPrefab;
    
    private void Update()
    {
        if (isPlant.isDone)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlantCard();
            }
        }
    }

    //放置
    public void PlantCard()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         Instantiate(cardPrefab, pos, Quaternion.identity);
    }
}
