using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int lives = 10;
    public GameObject GameOverUI;


    public TMP_Text livesText;

    private void Start()
    {
        
        UpdateLivesUI();
    }

    private void UpdateLivesUI()
    {
        livesText.text = lives.ToString(); // 更新健康栏UI的文本
    }

    private void ReduceLives()
    {
        lives--;
        UpdateLivesUI();
        if (lives == 0)
        { 
            Time.timeScale = 0f;
            GameOverUI.SetActive(true);
        }
    }

    private void OnEnable()
    {
        Enemy1.OnEndReached += ReduceLives;
    }



    private void OnDisable()
    {
        Enemy1.OnEndReached -= ReduceLives;
    }



}
