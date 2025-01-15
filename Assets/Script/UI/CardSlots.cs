using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Random = System.Random;
using UnityEngine.EventSystems;

public class CardSlots : MonoBehaviour
{  
   public BoolSO isClick;
   public bool isCostEnough;
   public int lastIDNumber;
   public int nowIDNumber;
   public int randomNumber;
   public int lastNumber;
   public int currentNumber;
   Random random = new Random();
   public List<TMP_Text> cardsCost = new List<TMP_Text>();
   public List<TMP_Text> levels = new List<TMP_Text>();
   public List<GameObject> cards = new List<GameObject>();
   public List<GameObject> towels = new List<GameObject>();
   public List<GameObject> towelReady = new List<GameObject>();
   public UnityEvent OnClick;

   private void Awake()
   {
      UpdateCards(); 
   }

   
   public void OnCardSlotsClick()
   {
       lastIDNumber = nowIDNumber;
       nowIDNumber = -1;
       if (!isClick.isDone&& isCostEnough )
       {
           Debug.Log("plant");
           isClick.isDone = true; 
           isCostEnough = false;
       }

       else
       {
           isClick.isDone = false;
           isCostEnough = false;
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

   public void UpdateOneCard(int number,int ID)
   {
       if (ID < 9)
       {
           cards[number].GetComponent<Image>().sprite = towels[ID+3].GetComponent<SpriteRenderer>().sprite;
           cardsCost[number].text = towels[ID+3].GetComponent<Towel>().towelData.costNeeded.ToString();
           towelReady[number] = towels[ID+3];
           lastNumber = -2;
       }

   }
   
   public void RandomUpdateOneCard(int number)
   {
       currentNumber = -1;
       randomNumber = random.Next(1, 3);
       cards[number].GetComponent<Image>().sprite = towels[randomNumber].GetComponent<SpriteRenderer>().sprite;
       cardsCost[number].text = towels[randomNumber].GetComponent<Towel>().towelData.costNeeded.ToString();
       towelReady[number] = towels[randomNumber];
       lastIDNumber = -2;
   }

   public void GetIsStarUp(int number,int ID)
   {
       if (lastIDNumber == nowIDNumber)
       {
           Debug.Log("ok");
           CostManeger.instance.ChangeCost(-towels[ID].GetComponent<Towel>().towelData.costNeeded);
           OnClick.Invoke();
           RandomUpdateOneCard(lastNumber);
           UpdateOneCard(number,ID);
           isCostEnough = false;
           nowIDNumber = -1;
           lastIDNumber = -2;
           currentNumber = -1;
           lastNumber = -2;
       }
   }
   
   public void Card1()
   {
       if (cards[0] != null&&towels[0].GetComponent<Towel>().towelData.costNeeded<=CostManeger.instance.costSO.number&&currentNumber!=0)
       {
           currentNumber = 0;
           nowIDNumber = towelReady[0].GetComponent<Towel>().ID;
           isCostEnough = true;
           CardManager.instance.cardPrefab = towelReady[0]; 
           cards[0].GetComponent<Image>().sprite = towelReady[0].GetComponent<SpriteRenderer>().sprite;
           cardsCost[0].text = towelReady[0].GetComponent<Towel>().towelData.costNeeded.ToString();
           towelReady[0] = null;
           GetIsStarUp(0,lastIDNumber);
           lastNumber = currentNumber;
       }
       else
       {
           isCostEnough = false;
       }
       OnCardSlotsClick();
   }

   public void Card2()
   {
       if (towelReady[1] != null&&towelReady[1].GetComponent<Towel>().towelData.costNeeded<=CostManeger.instance.costSO.number&&currentNumber!=1)
       {
           currentNumber = 1;
           isCostEnough = true;
           nowIDNumber = towelReady[1].GetComponent<Towel>().ID;
           CardManager.instance.cardPrefab = towelReady[1]; 
           cards[1].GetComponent<Image>().sprite = towelReady[1].GetComponent<SpriteRenderer>().sprite;
           cardsCost[1].text = towelReady[1].GetComponent<Towel>().towelData.costNeeded.ToString(); 
           towelReady[1] = null;
           GetIsStarUp(1,lastIDNumber); 
           lastNumber = currentNumber;
       }
       else
       {
           isCostEnough = false;
       }
       OnCardSlotsClick();
   }

   public void Card3()
   {
       if (towelReady[2] != null&&towelReady[2].GetComponent<Towel>().towelData.costNeeded<=CostManeger.instance.costSO.number&&currentNumber!=2)
       {
           currentNumber = 2;
           isCostEnough = true;
           nowIDNumber = towelReady[2].GetComponent<Towel>().ID;
           CardManager.instance.cardPrefab = towelReady[2];
           cards[2].GetComponent<Image>().sprite = towelReady[2].GetComponent<SpriteRenderer>().sprite;
           cardsCost[2].text = towelReady[2].GetComponent<Towel>().towelData.costNeeded.ToString();
           towelReady[2] = null;
           GetIsStarUp(2,lastIDNumber); 
           lastNumber = currentNumber;
       }
       else
       {
           isCostEnough = false;
       }
       OnCardSlotsClick();
   }
   public void Card4()
   {
       if (towelReady[3] != null&&towelReady[3].GetComponent<Towel>().towelData.costNeeded<=CostManeger.instance.costSO.number&&currentNumber!=3)
       {
           currentNumber = 3;
           isCostEnough = true;
           nowIDNumber = towelReady[3].GetComponent<Towel>().ID;
           CardManager.instance.cardPrefab = towelReady[3];
           cards[3].GetComponent<Image>().sprite = towelReady[3].GetComponent<SpriteRenderer>().sprite;
           cardsCost[3].text = towelReady[3].GetComponent<Towel>().towelData.costNeeded.ToString(); 
           towelReady[3] = null;
           GetIsStarUp(3,lastIDNumber); 
           lastNumber = currentNumber;
       }
       else
       {
           isCostEnough = false;
       }
       OnCardSlotsClick();
   } 
   public void Card5()
   {
       if (towelReady[4] != null&&towelReady[4].GetComponent<Towel>().towelData.costNeeded<=CostManeger.instance.costSO.number&&currentNumber!=4)
       {
           currentNumber = 4;
           isCostEnough = true;
           nowIDNumber = towelReady[4].GetComponent<Towel>().ID;
           CardManager.instance.cardPrefab = towelReady[4];
           cards[4].GetComponent<Image>().sprite = towelReady[4].GetComponent<SpriteRenderer>().sprite;
           cardsCost[4].text = towelReady[4].GetComponent<Towel>().towelData.costNeeded.ToString(); 
           towelReady[4] = null;
           GetIsStarUp(4,lastIDNumber); 
           lastNumber = currentNumber;
       }
       else
       {
           isCostEnough = false;
       }
       OnCardSlotsClick();
   } 
   public void Card6()
   {
       if (towelReady[5] != null&&towelReady[5].GetComponent<Towel>().towelData.costNeeded<=CostManeger.instance.costSO.number&&currentNumber!=5)
       {
           currentNumber = 5;
           isCostEnough = true;
           nowIDNumber = towelReady[5].GetComponent<Towel>().ID;
           CardManager.instance.cardPrefab = towelReady[5];
           cards[5].GetComponent<Image>().sprite = towelReady[5].GetComponent<SpriteRenderer>().sprite;
           cardsCost[5].text = towelReady[5].GetComponent<Towel>().towelData.costNeeded.ToString();
           towelReady[5] = null;
           GetIsStarUp(5,lastIDNumber); 
           lastNumber = currentNumber;
       }
       else
       {
           isCostEnough = false;
       }
       OnCardSlotsClick();
   } 
}
