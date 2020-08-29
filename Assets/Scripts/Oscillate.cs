using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour
{
    public Vector3 amplitude;

    private Vector3 origin;

    public float timePeriod;

    private float timer = 0;

    private float waveVelocity;

    void Start()
    {
        origin = transform.position;
        waveVelocity = 2 * Mathf.PI / timePeriod;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timePeriod)
        {
            timer -= timePeriod;
        }
        transform.position = origin + Mathf.Sin(waveVelocity * timer) * amplitude;
    }
}
