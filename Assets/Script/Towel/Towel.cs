using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Towel : MonoBehaviour
{
    public TowelCodeName towelCodeName;
    public TowelType towelType;
    public abstract void Attack();
    public abstract void Hurt();
    public abstract void Death();
}
