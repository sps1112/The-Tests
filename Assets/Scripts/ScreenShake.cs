using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float amplitude;

    public float timePeriod;

    [Range(0, 1)]
    public float randomness;

    private float value;

    private bool toShake = false;

    private float timer = 0f;

    private Vector3 origin;

    public void Shake(float factor)
    {
        value = factor * amplitude;
        timer = 0f;
        toShake = true;
        origin = transform.position;
        GetComponent<CameraFollow>().SetFollow(false);
    }

    void Update()
    {
        if (toShake)
        {
            timer += Time.deltaTime;
            if (timer >= timePeriod)
            {
                timer = 0f;
                value = amplitude;
                toShake = false;
                transform.position = origin;
                GetComponent<CameraFollow>().SetFollow(true);
            }
            else
            {
                ShakeScreen();
            }
        }
    }

    void ShakeScreen()
    {
        float xValue = Random.Range(-1, 1) * randomness * value;
        float yValue = Random.Range(-1, 1) * randomness * value;
        float zValue = Random.Range(-1, 1) * randomness * value;
        transform.position = origin + new Vector3(xValue, yValue, zValue);
    }
}
