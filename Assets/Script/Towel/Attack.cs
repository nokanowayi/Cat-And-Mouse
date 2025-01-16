using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject father;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == father.GetComponent<Soldiers>().nowEnemy)
        {
            other.GetComponent<Enemy>().TakeDamage(father.GetComponent<Soldiers>().damage);
        }
    }

}
