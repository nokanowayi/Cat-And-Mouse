using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSlots : MonoBehaviour
{  
   public BoolSO isClick;
   public bool isCostEnough;
   public List<TMP_Text> cardsCost = new List<TMP_Text>();
   public List<GameObject> cards = new List<GameObject>();
   public List<GameObject> towers = new List<GameObject>();

   private void Awake()
   {
      UpdateCards(); 
   }

   public void OnCardSlotsClick()
   {
       if (!isClick.isDone&&isCostEnough)
       {
           isClick.isDone = true; 
       }
       else
       {
           isClick.isDone = false;
       } 
   }

   public void UpdateCards()
   {
       for (int i = 0; i < cards.Count; i++)
       {
           cards[i].GetComponent<Image>().sprite = towers[i].GetComponent<SpriteRenderer>().sprite;
           cardsCost[i].text = towers[i].GetComponent<Towel>().towelData.costNeeded.ToString();
       }
   }
   
   public void Card1()
   {
       if (towers[0] != null&&towers[0].GetComponent<Towel>().towelData.costNeeded<=CostManeger.instance.costSO.number)
       {
           isCostEnough = true;
           CardManager.instance.cardPrefab = towers[0]; 
       }
       else
       {
           isCostEnough = false;
       }
       OnCardSlotsClick();
   }

   public void Card2()
   {
       if (towers[1] != null&&towers[1].GetComponent<Towel>().towelData.costNeeded<=CostManeger.instance.costSO.number)
       {
           isCostEnough = true;
           CardManager.instance.cardPrefab = towers[1]; 
       }
       else
       {
           isCostEnough = false;
       }
       OnCardSlotsClick();
   }

   public void Card3()
   {
       if (towers[2] != null&&towers[2].GetComponent<Towel>().towelData.costNeeded<=CostManeger.instance.costSO.number)
       {
           isCostEnough = true;
           CardManager.instance.cardPrefab = towers[2];
       }
       else
       {
           isCostEnough = false;
       }
       OnCardSlotsClick();
   }
}
