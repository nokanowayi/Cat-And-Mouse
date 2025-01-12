using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    public Rigidbody2D rb;
    public Animator anim;
    public int speed;
    public Vector2 direction;

    private void Update()
    {
       Move(); 
    }

    public override void Move()
    {
        rb.velocity = new Vector2(direction.x*speed, rb.velocity.y); 
    }

    public override void Attack()
    {
        
    }

    public override void Hurt()
    {
        
    }

    public override void Death()
    {
        
    }
}
