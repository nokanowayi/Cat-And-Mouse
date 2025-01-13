using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TowelSO", menuName = "SO/TowelSO")]
public class TowelSO : ScriptableObject
{
    public Sprite sprite;
    public string towelName;
    public int damage;
    public float maxHealth;
    public float attackInterval;
    public int speed;
    public int bulletDisposeTime;
    public float attackRange;
    public Vector2 position;
    public int costNeeded;
}
