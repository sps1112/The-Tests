using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    public EnemyData data;

    private float attack;

    private float collisionForce;

    void Start()
    {
        SetStats();
    }

    void SetStats()
    {
        attack = data.attack;
        collisionForce = data.collisionForce;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.tag == "Player")
        {
            GameObject player = other.collider.gameObject;
            if (!player.GetComponent<Controller>().GetStatus())
            {
                player.GetComponent<Controller>().AddReaction(transform.position, collisionForce);
                player.GetComponent<Health>().ChangeHealth(-attack);
            }
        }
    }
}
