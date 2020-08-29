using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public GameObject[] energyPrefabs;

    public float generateTimeDelay;

    private Vector3 point;

    [Range(0f, 1f)]
    public float spawnChance;

    [SerializeField]
    private List<GameObject> currentList;

    public float maxLimit;

    void Start()
    {
        currentList = new List<GameObject>();
    }

    public void GenerateEnergy(Vector3 position)
    {
        point = position;
        StartCoroutine(CreateEnergy());
    }

    IEnumerator CreateEnergy()
    {
        yield return new WaitForSeconds(generateTimeDelay);
        if (Random.Range(0f, 1f) <= spawnChance)
        {
            float xValue = Random.Range(-1f, 1f);
            float yValue = Random.Range(0f, 1f);
            Vector3 spawnPoint = point + new Vector3(xValue, yValue, 0);
            if (Random.Range(0f, 1f) > 0.3f)
            {
                GameObject newEnergy = Instantiate(energyPrefabs[0], spawnPoint, Quaternion.identity);
                AddEnergy(newEnergy);
            }
            else
            {
                GameObject newEnergy = Instantiate(energyPrefabs[1], spawnPoint, Quaternion.identity);
                AddEnergy(newEnergy);
            }
        }
    }

    void AddEnergy(GameObject newEnergy)
    {
        if (currentList.Count >= maxLimit)
        {
            GameObject energy = currentList[currentList.Count - 1];
            Destroy(energy);
            //currentList[currentList.Count - 1] = null;
            currentList.RemoveAt(currentList.Count - 1);
            currentList.TrimExcess();
        }
        currentList.Insert(0, newEnergy);
    }

    public void Remove(GameObject testEnergy)
    {
        int index = currentList.IndexOf(testEnergy);
        Destroy(testEnergy);
        //currentList[index] = null;
        currentList.RemoveAt(index);
        currentList.TrimExcess();
    }
}
