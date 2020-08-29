using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMove : MonoBehaviour
{
    [Range(-1, 1)]
    public float parallaxStrength;

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = Camera.main.transform.position;
    }

    void FixedUpdate()
    {
        Vector3 displacement = Camera.main.transform.position - lastPosition;
        transform.position += displacement * parallaxStrength;
        lastPosition = Camera.main.transform.position;
    }
}
