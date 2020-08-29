using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public KeyGateType gateType;

    public RoomTrigger[] roomTriggers;

    public bool canClose;

    public GameObject sprite;

    void Start()
    {
        sprite = transform.Find("Sprite").gameObject;
        if (!canClose)
        {
            sprite.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Controller>().GetStatus())
            {
                if (sprite.activeSelf)
                {
                    if (gateType == KeyGateType.Normal)
                    {
                        sprite.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("not normal");
                        if (CheckKey())
                        {
                            GameObject.Find("GameManager").GetComponent<KeyManager>().UseKey(gateType);
                            sprite.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    public void CloseGate()
    {
        if (canClose)
        {
            sprite.SetActive(true);
        }
    }

    public bool CheckKey()
    {
        return GameObject.Find("GameManager").GetComponent<KeyManager>().HasKey(gateType);
    }

}
