using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public float energy;

    public float removeTime;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= removeTime)
        {
            timer = 0f;
            GameObject.Find("GameManager").GetComponent<EnergyManager>().Remove(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Health>().CheckStatus())
            {
                other.gameObject.GetComponent<Health>().ChangeHealth(+energy);
                transform.Find("Sprite").gameObject.SetActive(false);
                transform.Find("Particle").gameObject.GetComponent<ParticleSystem>().Play();
                GetComponent<CircleCollider2D>().enabled = false;
                Invoke("Remove", 0.2f);
            }
        }
    }

    void Remove()
    {
        GameObject.Find("GameManager").GetComponent<EnergyManager>().Remove(this.gameObject);
    }
}
