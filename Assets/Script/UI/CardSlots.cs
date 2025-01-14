using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = System.Random;

public class CardSlots : MonoBehaviour
{  
   public BoolSO isClick;
   public bool isCostEnough;
   public int nowNumber;
   Random random = new Random();
   public List<TMP_Text> cardsCost = new List<TMP_Text>();
   public List<TMP_Text> levels = new List<TMP_Text>();
   public List<GameObject> cards = new List<GameObject>();
   public List<GameObject> towels = new List<GameObject>();
   public List<GameObject> towelReady = new List<GameObject>();

   private void Awake()
   {
      UpdateCards(); 
   }

   public void OnCardSlotsClick(int number)
   {
       if (!isClick.isDone&&isCostEnough)
       {
           isClick.isDone = true; 
           UpdateOneCard(number);
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
           cards[i].GetComponent<Image>().sprite = towels[i].GetComponent<SpriteRenderer>().sprite;
           cardsCost[i].text = towels[i].GetComponent<Towel>().towelData.costNeeded.ToString();
           towelReady[i] = towels[i];
       }
   }

   public void UpdateOneCard(int number)
   {
       nowNumber = random.Next(0, cards.Count-1);
       cards[number].GetComponent<Image>().sprite = towels[nowNumber].GetComponent<SpriteRenderer>().sprite;
       cardsCost[number].text = towels[nowNumber].GetComponent<Towel>().towelData.costNeeded.ToString();
       towelReady[number] = towels[nowNumber];
   }
   
   public void Card1()
   {
       if (cards[0] != null&&towels[0].GetComponent<Towel>().towelData.costNeeded<=CostManeger.instance.costSO.number)
       {
           isCostEnough = true;
           CardManager.instance.cardPrefab = towelReady[0]; 
           cards[0].GetComponent<Image>().sprite = towelReady[0].GetComponent<SpriteRenderer>().sprite;
           cardsCost[0].text = towelReady[0].GetComponent<Towel>().towelData.costNeeded.ToString();
           towelReady[0] = null;
       }
       else
       {
           isCostEnough = false;
       }
       OnCardSlotsClick(0);
   }

   public void Card2()
   {
       if (towelReady[1] != null&&towelReady[1].GetComponent<Towel>().towelData.costNeeded<=CostManeger.instance.costSO.number)
       {
           isCostEnough = true;
           CardManager.instance.cardPrefab = towelReady[1]; 
           cards[1].GetComponent<Image>().sprite = towelReady[1].GetComponent<SpriteRenderer>().sprite;
           cardsCost[1].text = towelReady[1].GetComponent<Towel>().towelData.costNeeded.ToString(); 
           towelReady[1] = null;
       }
       else
       {
           isCostEnough = false;
       }
       OnCardSlotsClick(1);
   }

   public void Card3()
   {
       if (towelReady[2] != null&&towelReady[2].GetComponent<Towel>().towelData.costNeeded<=CostManeger.instance.costSO.number)
       {
           isCostEnough = true;
           CardManager.instance.cardPrefab = towelReady[2];
           cards[2].GetComponent<Image>().sprite = towelReady[2].GetComponent<SpriteRenderer>().sprite;
           cardsCost[2].text = towelReady[2].GetComponent<Towel>().towelData.costNeeded.ToString(); 
           towelReady[2] = null;
       }
       else
       {
           isCostEnough = false;
       }
       OnCardSlotsClick(2);
   }
}
