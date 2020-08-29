using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public Room roomData;

    public Bounds roomBound;

    RoomManager roomManager;

    [SerializeField]
    private bool isInside = false;

    void Awake()
    {
        roomBound = GetComponent<BoxCollider2D>().bounds;
        roomManager = GameObject.Find("GameManager").GetComponent<RoomManager>();
        roomData.EmptyRoom();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isInside)
            {
                roomManager.PlayerEnter(this);
                isInside = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInside = false;
        }
    }
}
