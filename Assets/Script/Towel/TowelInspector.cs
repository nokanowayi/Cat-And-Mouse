using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TowelInspector : MonoBehaviour
{
     public static TowelInspector instance;
     
     [Header("观察面板")]
     public GameObject TowelPanel;
     public TMP_Text levelText;
     public TMP_Text damegeText;
     public TMP_Text attackIntervalText;
     public TMP_Text maxHealthText;
     public TMP_Text currentHealthText;
     public TMP_Text nameText;
     
     [Header("数据")]
     public TowelSO towelData;
     public int level;
     public float currenthealth;

     private void Awake()
     {
         instance = this; 
     }

     public void OnTowelClick(TowelSO towel, int level, float currenthealth)
     {
          this.level = level;
          this.currenthealth = currenthealth;
          towelData = towel;
          TowelPanel.SetActive(true);
          UpdateData();
          CameraController.instance.TurnCameraSize(2);
          CameraController.instance.TurnCameraPositon(towel.position);
     }

     public void OnExitClick()
     {
          TowelPanel.SetActive(false);
          CameraController.instance.TurnCameraSize(5);
          CameraController.instance.TurnBackCameraPositon();
     }
     
     private void UpdateData()
     {
          levelText.text = "Level:"+level.ToString();
          damegeText.text = "Damage:"+towelData.damage.ToString();
          attackIntervalText.text = "Attack Interval:"+towelData.attackInterval.ToString();
          maxHealthText.text = "Max Health:"+towelData.maxHealth.ToString();
          currentHealthText.text = "CurrentHealth:"+currenthealth.ToString();
          nameText.text = towelData.towelName;
     }
}
