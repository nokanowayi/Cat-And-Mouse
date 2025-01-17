using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Enemy : MonoBehaviour
{
    [Header("基础数值")]
    public float intialHealth;
    public float currentHealth;
    public float moveSpeed;
    public float damage;
    public float attackRange; // 攻击范围
    public float searchRange; // 索敌范围

    protected float attackInterval = 3f; // 攻击间隔时间

    public int _currentWaypointIndex;

    public abstract void TakeDamage(float damage);
    public abstract void Move();
    public abstract void Attack(Soldiers soldiers);
    public abstract void Hurt();
    public abstract void Death();
}
