using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    static GameData instance;

    public static Bounds roomBounds;

    public static Vector3 playerPosition;

    public static Vector3 cameraPosition;

    public static bool hasData;

    public static float playerHealth;

    public static Vector2 playerVelocity;

    public static List<KeyGateType> keyList;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            hasData = false;
            keyList = new List<KeyGateType>();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
