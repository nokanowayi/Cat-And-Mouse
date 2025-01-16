using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAnimation : MonoBehaviour
{
    private Animator animator;
    public bool isWalk;

    private void Awake()
    {
       animator = GetComponent<Animator>(); 
    }

    private void Update()
    {
       SetAnimation(); 
    }

    public void SetAnimation()
    {
        animator.SetBool("isW",isWalk);
    }

    public void SetAnimationTrigger()
    {
        animator.SetTrigger("hit");
        //Debug.Log("HIT");
    }
}
