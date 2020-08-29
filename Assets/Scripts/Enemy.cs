using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 origin;

    void Awake()
    {
        origin = transform.position;
    }

    public void ResetEnemy()
    {
        transform.position = origin;
        GetComponent<Health>().Reset();
        GetComponent<Patrol>().Reset();
    }
}
