using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TowelInspector : MonoBehaviour
{
     public static TowelInspector instance;

     public BoolSO isInspector;
     [Header("观察面板")]
     public GameObject TowelPanel;
     public TMP_Text levelText;
     public TMP_Text damegeText;
     public TMP_Text attackIntervalText;
     public TMP_Text maxHealthText;
     public TMP_Text currentHealthText;
     public TMP_Text nameText;
     public Canvas canvas;

     [Header("数据")] 
     public bool isLevelUp = false;
     public TowelSO towelData;
     public int level;
     public float currenthealth;
     public NumberSO Cost;

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
          Time.timeScale = 0;
          canvas.gameObject.SetActive(false);
     }

     public void GetIfLevelUp()
     {
          isLevelUp = true;
     }
     

     public void OnExitClick()
     {
          TowelPanel.SetActive(false);
          CameraController.instance.TurnCameraSize(5);
          CameraController.instance.TurnBackCameraPositon();
          isInspector.isDone = false;
          Time.timeScale = 1;
          canvas.gameObject.SetActive(true);
     }
     
     public void UpdateData()
     {
          levelText.text = "Level:"+level.ToString();
          damegeText.text = "Damage:"+towelData.damage.ToString();
          attackIntervalText.text = "Attack Interval:"+towelData.attackInterval.ToString();
          maxHealthText.text = "Max Health:"+towelData.maxHealth.ToString();
          currentHealthText.text = "CurrentHealth:"+currenthealth.ToString();
          nameText.text = towelData.towelName;
     }
     

}
