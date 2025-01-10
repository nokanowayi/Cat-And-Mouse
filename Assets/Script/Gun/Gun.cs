using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject towel;
    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 direction; 
    public int speed;
    private void Update()
    {
        //transform.position = towel.transform.position; 
        rb.velocity = direction.normalized*speed;
    }
}
