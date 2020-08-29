using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    static Settings instance;

    public static float volumeValue;

    public static bool fullScreenStatus;

    public static int qualityIndex;

    public static int resolutionIndex;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            qualityIndex = 3;
            volumeValue = 0;
            fullScreenStatus = true;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}