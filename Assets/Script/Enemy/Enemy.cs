using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Enemy : MonoBehaviour
{
    [Header("基础数值")]
    public int intialHealth;
    public int currentHealth;

    protected float attackInterval = 1f; // 攻击间隔时间

    public int _currentWaypointIndex;

    public abstract void Move();
    public abstract void Attack();
    public abstract void Hurt();
    public abstract void Death();
}
