using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Enemy : MonoBehaviour
{
    public abstract void Move();
    public abstract void Attack();
    public abstract void Hurt();
    public abstract void Death();
}
