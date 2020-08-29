using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room
{
    public GameObject[] gates;

    public GameObject[] enemies;

    public bool isEmpty = false;

    public void EmptyRoom()
    {
        if (!isEmpty)
        {
            foreach (GameObject enemyi in enemies)
            {
                enemyi.SetActive(false);
            }
            foreach (GameObject gatei in gates)
            {
                gatei.GetComponent<Gate>().CloseGate();
            }
            isEmpty = true;
        }
    }

    public void RefillRoom()
    {
        if (isEmpty)
        {
            foreach (GameObject enemyi in enemies)
            {
                enemyi.SetActive(true);
                enemyi.GetComponent<Enemy>().ResetEnemy();
            }
            isEmpty = false;
        }
    }
}
