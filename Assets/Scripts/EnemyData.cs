using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : Stats
{
    public Vector3 patrolDisplacement;

    public float patrolTimePeriod;

    public float collisionForce;
}
