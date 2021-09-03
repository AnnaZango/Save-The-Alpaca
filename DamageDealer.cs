using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] int hidrationValue = 0; 

    public int GetDamageInflicted()
    {
        return damage;
    }

    public int GetHidration()
    {
        return hidrationValue;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
