using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public KeyGateType keyType;

    private bool isActive = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive)
        {
            if (other.gameObject.tag == "Player")
            {
                isActive = false;
                transform.Find("Sprite").gameObject.SetActive(false);
                transform.Find("Particle").gameObject.GetComponent<ParticleSystem>().Play();
                GetComponent<BoxCollider2D>().enabled = false;
                GameObject.Find("GameManager").GetComponent<KeyManager>().AddKey(keyType);
                Invoke("End", 0.2f);
            }
        }
    }

    void End()
    {
        Destroy(this.gameObject);
    }
}
