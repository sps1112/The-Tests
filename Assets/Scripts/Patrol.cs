using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public EnemyData data;

    private Vector3 displacement;

    private float timePeriod;

    private Vector3 origin;

    private float timer = 0;

    private float speed;

    void Start()
    {
        SetStats();
        origin = transform.position;
        speed = displacement.magnitude / (timePeriod / 4f);
    }

    void SetStats()
    {
        displacement = data.patrolDisplacement;
        timePeriod = data.patrolTimePeriod;
    }

    void Update()
    {
        Vector3 destination = transform.position;
        timer += Time.deltaTime;
        if (timer >= timePeriod)
        {
            timer -= timePeriod;
        }
        if (timer <= timePeriod / 4f)
        {
            destination = Vector3.Lerp(origin, origin + displacement, timer / (timePeriod / 4f));
        }
        else if (timer >= timePeriod / 4f && timer <= 3 * timePeriod / 4f)
        {
            destination = Vector3.Lerp(origin + displacement, origin - displacement, (timer - timePeriod / 4f) / (timePeriod / 2f));
        }
        else if (timer >= 3 * timePeriod / 4f && timer <= timePeriod)
        {
            destination = Vector3.Lerp(origin - displacement, origin, (timer - 3 * timePeriod / 4f) / (timePeriod / 4f));
        }
        transform.position = destination;
    }

    public void Reset()
    {
        timer = 0f;
    }
}
