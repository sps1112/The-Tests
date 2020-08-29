using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private List<RoomTrigger> currentRooms;

    [SerializeField]
    private List<RoomTrigger> previousRooms;

    RoomTrigger playerRoom;

    void Awake()
    {
        currentRooms = new List<RoomTrigger>();
        previousRooms = new List<RoomTrigger>();
    }

    public void PlayerEnter(RoomTrigger nextRoom)
    {
        playerRoom = nextRoom;
        Camera.main.GetComponent<CameraFollow>().ChangeRooms(playerRoom.roomBound);
        StartCoroutine(SwitchRooms());
    }

    IEnumerator SwitchRooms()
    {
        yield return new WaitForSeconds(0.15f);
        AddRoom(playerRoom);
        foreach (GameObject gatei in playerRoom.roomData.gates)
        {
            RoomTrigger otherTrigger = gatei.GetComponent<Gate>().roomTriggers[0];
            if (gatei.GetComponent<Gate>().roomTriggers[0] == playerRoom)
            {
                otherTrigger = gatei.GetComponent<Gate>().roomTriggers[1];
            }
            AddRoom(otherTrigger);
        }
        RefillRooms();
    }

    private void RefillRooms()
    {
        foreach (RoomTrigger roomi in previousRooms)
        {
            if (!currentRooms.Contains(roomi))
            {
                roomi.roomData.EmptyRoom();
            }
        }
        foreach (RoomTrigger roomi in currentRooms)
        {
            roomi.roomData.RefillRoom();
        }
        previousRooms = currentRooms;
        currentRooms = new List<RoomTrigger>();
    }

    private void AddRoom(RoomTrigger newTrigger)
    {
        if (!currentRooms.Contains(newTrigger))
        {
            currentRooms.Add(newTrigger);
        }
    }
}
