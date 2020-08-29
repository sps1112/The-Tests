using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;

    public Vector3 offset;

    [Range(0, 1)]
    public float smoothFactor;

    [Range(0, 1)]
    public float changeSpeed;

    [SerializeField]
    private Vector2[] bounds;

    [SerializeField]
    private bool isChanging = false;

    private float timer = 0f;

    public float transitionPeriod;

    private Bounds roomBounds;

    private bool toFollow = true;

    void Awake()
    {
        if (GameData.hasData)
        {
            roomBounds = GameData.roomBounds;
            transform.position = GameData.cameraPosition;
            bounds = new Vector2[2];
            bounds = CalculateBounds();
        }
    }

    void Update()
    {
        if (isChanging)
        {
            timer += Time.deltaTime;
            if (timer >= transitionPeriod)
            {
                timer = 0f;
                isChanging = false;
            }
        }
        bounds = CalculateBounds();
    }

    void FixedUpdate()
    {
        if (toFollow)
        {
            Vector3 targetPOS = target.transform.position + offset;
            if (!isChanging)
            {
                Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPOS, smoothFactor);
                smoothPosition.x = Mathf.Clamp(smoothPosition.x, bounds[0].x, bounds[1].x);
                smoothPosition.y = Mathf.Clamp(smoothPosition.y, bounds[0].y, bounds[1].y);
                transform.position = smoothPosition;
            }
            else
            {
                targetPOS.x = Mathf.Clamp(targetPOS.x, bounds[0].x, bounds[1].x);
                targetPOS.y = Mathf.Clamp(targetPOS.y, bounds[0].y, bounds[1].y);
                transform.position = Vector3.Lerp(transform.position, targetPOS, changeSpeed);
                if ((transform.position - targetPOS).magnitude == 0f)
                {
                    isChanging = false;
                    timer = 0f;
                }
            }
        }
    }

    public void ChangeRooms(Bounds newBound)
    {
        roomBounds = newBound;
        isChanging = true;
        timer = 0f;
    }

    private Vector2[] CalculateBounds()
    {
        Vector2[] newBounds = bounds;
        Vector2 cameraDistance = (new Vector2(Camera.main.aspect, 1)) * Camera.main.orthographicSize;
        Vector2 bound1 = (Vector2)(roomBounds.min) + cameraDistance;
        Vector2 bound2 = (Vector2)(roomBounds.max) - cameraDistance;
        newBounds[0] = new Vector2(Mathf.Min(bound1.x, bound2.x), Mathf.Min(bound1.y, bound2.y));
        newBounds[1] = new Vector2(Mathf.Max(bound1.x, bound2.x), Mathf.Max(bound1.y, bound2.y));
        return newBounds;
    }

    public void SetFollow(bool status)
    {
        toFollow = status;
    }

    public Bounds GetBounds()
    {
        return roomBounds;
    }
}
