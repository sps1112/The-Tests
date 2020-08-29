using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> currentObjects;

    public Transform[] wayPoints;

    public float speed;

    [SerializeField]
    private int currentIndex;

    public float stoppingDistance;

    [SerializeField]
    private Vector3 target;

    void Awake()
    {
        currentObjects = new List<GameObject>();
        currentIndex = wayPoints.Length - 1;
        NextPoint();
    }

    void NextPoint()
    {
        currentIndex++;
        currentIndex %= wayPoints.Length;
        target = wayPoints[currentIndex].position;
    }

    void FixedUpdate()
    {
        Vector3 direction = target - transform.position;
        if (direction.magnitude <= stoppingDistance)
        {
            NextPoint();
            direction = target - transform.position;
        }
        direction.Normalize();
        transform.position += direction * speed * Time.fixedDeltaTime;
        foreach (GameObject objecti in currentObjects)
        {
            objecti.transform.position += direction * speed * Time.fixedDeltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject rootObject = other.gameObject.transform.root.gameObject;
        if (rootObject.tag != "Other")
        {
            if (!currentObjects.Contains(rootObject))
            {
                currentObjects.Add(rootObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        GameObject rootObject = other.gameObject.transform.root.gameObject;
        if (rootObject.tag != "Other")
        {
            if (!currentObjects.Contains(rootObject))
            {
                currentObjects.Add(rootObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        GameObject rootObject = other.gameObject.transform.root.gameObject;
        if (currentObjects.Contains(rootObject))
        {
            currentObjects.Remove(rootObject);
        }
    }
}
