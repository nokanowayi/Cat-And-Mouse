using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int lives = 10;


    private void ReduceLives()
    {
        lives--;
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
