using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 axis;

    public float speed;

    public Transform center;

    void Update()
    {
        float change = speed * Time.deltaTime;
        if (center.position == transform.position)
        {
            transform.Rotate(change * axis, Space.Self);
        }
        else
        {
            transform.RotateAround(center.position, axis, change);
        }
    }
}
