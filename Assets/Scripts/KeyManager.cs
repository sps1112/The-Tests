using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField]
    private List<KeyGateType> keyList;

    void Awake()
    {
        keyList = new List<KeyGateType>();
        if (GameData.hasData)
        {
            keyList = GameData.keyList;
        }
        SetUI();
    }

    public List<KeyGateType> GetList()
    {
        return keyList;
    }

    void SetUI()
    {
        int key1Count = 0;
        int key2Count = 0;
        foreach (KeyGateType keyi in keyList)
        {
            Debug.Log("key");
            if (keyi == KeyGateType.Level1)
            {
                key1Count++;
            }
            else if (keyi == KeyGateType.Level2)
            {
                key2Count++;
            }
        }
        GetComponent<UIManager>().SetKeyUI(key1Count, key2Count);
    }

    public void AddKey(KeyGateType newKey)
    {
        keyList.Insert(0, newKey);
        SetUI();
    }

    public void UseKey(KeyGateType useKey)
    {
        Debug.Log("use key");
        bool hasIndex = false;
        int index = 0;
        for (int i = 0; i < keyList.Count; i++)
        {
            if (keyList[i] == useKey)
            {
                index = i;
                hasIndex = true;
                break;
            }
        }
        if (hasIndex)
        {
            keyList.RemoveAt(index);
            keyList.TrimExcess();
        }
        SetUI();
    }

    public bool HasKey(KeyGateType keyGate)
    {
        return keyList.Contains(keyGate);
    }
}
