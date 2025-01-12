using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Towel : MonoBehaviour
{
    public TowelSO towelData;
    public TowelCodeName towelCodeName;
    public TowelType towelType;
    public float health;
    public abstract void OnTowelClick();
    public abstract void Attack();
    public abstract void Hurt();
    public abstract void Death();
}
