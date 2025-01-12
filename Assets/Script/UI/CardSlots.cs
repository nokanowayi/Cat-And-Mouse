using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlots : MonoBehaviour
{  
   public BoolSO isClick;
   public TowelCodeNameSO nowTowel;
   public List<GameObject> towers = new List<GameObject>();
   public void OnCardSlotsClick()
   {
       if (!isClick.isDone)
       {
           isClick.isDone = true; 
       }
       else
       {
           isClick.isDone = false;
       } 
   }

   public void Card1()
   {
       //Towel towel = towers[1].GetComponent<Towel>();
       //nowTowel.towelName = towel.towelCodeName;
   }
}
