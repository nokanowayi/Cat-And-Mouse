using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class TowelInspector : MonoBehaviour
{
     public static TowelInspector instance;

     public BoolSO isInspector;
     [Header("观察面板")]
     public GameObject TowelPanel;
     public GameObject Image;
     public TMP_Text damegeText;
     public TMP_Text maxHealthText;
     public TMP_Text currentHealthText;
     public Canvas canvas;

     [Header("数据")] 
     public bool isLevelUp = false;
     public TowelSO towelData;
     public int level;
     public float currenthealth;
     public NumberSO Cost;
     public GameObject nowGameObject;

     private void Awake()
     {
         instance = this; 
     }
     
     public void OnTowelClick(TowelSO towel, int level, float currenthealth,GameObject target)
     {
          this.level = level;
          this.currenthealth = currenthealth;
          towelData = towel;
          TowelPanel.SetActive(true);
          UpdateData();
          CameraController.instance.TurnCameraSize(2);
          CameraController.instance.TurnCameraPositon(target.transform.position);
          Time.timeScale = 0;
          canvas.gameObject.SetActive(false);
          nowGameObject = target;
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
          nowGameObject = null;
     }

     public void OnDelete()
     { 
          CardManager.instance.allCards.Remove(nowGameObject);
          Destroy(nowGameObject);
          OnExitClick();
     }
     public void UpdateData()
     {
          damegeText.text = towelData.damage.ToString();
          maxHealthText.text = towelData.maxHealth.ToString();
          currentHealthText.text = currenthealth.ToString();
          Image.GetComponent<Image>().sprite = towelData.panelSprite;
     }
}
