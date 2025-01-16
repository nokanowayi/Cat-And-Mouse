using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int lives = 10;
    public GameObject GameOverUI;


    private void ReduceLives()
    {
        lives--;
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
