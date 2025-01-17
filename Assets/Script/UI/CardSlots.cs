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
    public NumberSO nowCount;
    public BoolSO isClick;
   public bool isCostEnough;
   public int maxTowelCount;
   public int lastIDNumber;
   public int nowIDNumber;
   public int randomNumber;
   public int lastNumber;
   public int currentNumber;
   Random random = new Random();
   //public SpriteRenderer cardImage;
   public List<TMP_Text> cardsCost = new List<TMP_Text>();
   public List<TMP_Text> levels = new List<TMP_Text>();
   public List<GameObject> cards = new List<GameObject>();
   public List<GameObject> towels = new List<GameObject>();
   public List<GameObject> towelReady = new List<GameObject>();
   public UnityEvent OnClick;

   private void Awake()
   {
      UpdateCards(); 
      nowCount.number = 0;
   }

   private void Update()
   {
       Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       pos.z = 0;
      //cardImage.transform.position = Camera.main.WorldToScreenPoint(pos); 
   }

   public void OnCardSlotsClick()
   {
       lastIDNumber = nowIDNumber;
       nowIDNumber = -1;
       if (!isClick.isDone&& isCostEnough)
       {
           Debug.Log("plant");
           isClick.isDone = true; 
           isCostEnough = false;
       }
       else
       {
           //cardImage.gameObject.SetActive(false);
           Debug.Log(isClick.isDone);
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
           //cardImage.gameObject.SetActive(false);
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
       //cardImage.gameObject.SetActive(false);
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
           //cardImage.gameObject.SetActive(false);
           isCostEnough = false;
           nowIDNumber = -1;
           lastIDNumber = -2;
           currentNumber = -1;
           lastNumber = -2;
       }
   }

   public void GetIsMax()
   {
       if (nowCount.number>maxTowelCount)
       {
           //cardImage.gameObject.SetActive(false);
           OnClick.Invoke();
           isCostEnough = false;
       }
   }

   public void AfterClick(int number)
   {
       if (cards[number] != null&towels[number].GetComponent<Towel>().towelData.costNeeded<=CostManeger.instance.costSO.number&currentNumber!=number)
       {
           //cardImage.sprite = towelReady[number].GetComponent<SpriteRenderer>().sprite;
           //cardImage.gameObject.SetActive(true);
           currentNumber = number;
           nowIDNumber = towelReady[number].GetComponent<Towel>().ID;
           isCostEnough = true;
           CardManager.instance.cardPrefab = towelReady[number]; 
           cards[number].GetComponent<Image>().sprite = towelReady[number].GetComponent<SpriteRenderer>().sprite;
           cardsCost[number].text = towelReady[number].GetComponent<Towel>().towelData.costNeeded.ToString();
           GetIsStarUp(number,lastIDNumber);
           GetIsMax();
           lastNumber = currentNumber;
           Debug.Log("oh");
       }
       else
       {
           Debug.Log("no");
           //cardImage.gameObject.SetActive(false);
           isCostEnough = false;
           nowIDNumber = -1;
           lastIDNumber = -2;
           currentNumber = -1;
           lastNumber = -2; 
       }
   }

   public void Card1()
   {
       AfterClick(0);
       OnCardSlotsClick();
   }

   public void Card2()
   {
       AfterClick(1); 
       OnCardSlotsClick();
   }

   public void Card3()
   {
       AfterClick(2);
       OnCardSlotsClick();
   }
   public void Card4()
   {
       AfterClick(3);
       OnCardSlotsClick();
   } 
   public void Card5()
   {
       AfterClick(4); 
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
