using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public PlayerData data;

    private float attack;

    private float hitRecoil;

    void Start()
    {
        SetStats();
    }

    void SetStats()
    {
        attack = data.attack;
        hitRecoil = data.impactForce;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (GetComponent<Controller>().GetStatus())
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<Health>().ChangeHealth(-attack);
                if (other.gameObject.GetComponent<Health>().GetHealth() > 0)
                {
                    GetComponent<Controller>().AddReaction(other.gameObject.transform.position, hitRecoil);
                }
            }
        }
    }
}
