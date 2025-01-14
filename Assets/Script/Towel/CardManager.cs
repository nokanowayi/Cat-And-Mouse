using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public BoolSO isPlant;
    public GameObject cardPrefab;
    public GameObject nowCard;
    public List<GameObject> cards = new List<GameObject>();
    public List<GameObject> allCards = new List<GameObject>();
    
    public static CardManager instance;

    private void Awake()
    {
       instance = this; 
    }

    private void Update()
    {
        if (isPlant.isDone)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlantCard();
                isPlant.isDone = false;
            }
        }
    }

    //放置
    public void PlantCard()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cardPos = new Vector3(pos.x, pos.y, 0);
        if (cardPrefab != null)
        {
            nowCard = Instantiate(cardPrefab, cardPos, Quaternion.identity); 
            CostManeger.instance.ChangeCost(-cardPrefab.GetComponent<Towel>().towelData.costNeeded);
            allCards.Add(nowCard);
        }
        cardPrefab = null;
    }
}
