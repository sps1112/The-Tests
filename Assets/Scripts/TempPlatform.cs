using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlatform : MonoBehaviour
{
    private bool hasPlayer = false;

    public float vanishTimePeriod;

    private float timer = 0f;

    private GameObject spriteObject;

    public float respawnTimePeriod;

    void Start()
    {
        spriteObject = transform.Find("Sprite").gameObject;
    }

    void Update()
    {
        if (spriteObject.activeSelf)
        {
            if (hasPlayer)
            {
                timer += Time.deltaTime;
                if (timer >= vanishTimePeriod)
                {
                    spriteObject.SetActive(false);
                    timer = 0f;
                }
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= respawnTimePeriod)
            {
                spriteObject.SetActive(true);
                timer = 0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasPlayer && spriteObject.activeSelf)
        {
            if (other.gameObject.tag == "Player")
            {
                hasPlayer = true;
                timer = 0f;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (hasPlayer)
        {
            if (other.gameObject.tag == "Player")
            {
                hasPlayer = false;
            }
        }
    }
}
