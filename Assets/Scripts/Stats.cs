using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Stats : ScriptableObject
{
    public string objectName;

    public float health;

    public float attack;

    public float defence;

    public float immunityPeriod;

    public float hurtShakeFactor;

    public float attackRate;

    public float moveSpeed;
}
