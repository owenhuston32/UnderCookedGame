using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] float spawnWaitTime;
    [SerializeField] GameObject objPrefab;
    private GameObject[] spawnedObjects = new GameObject[3];

    public static Spawner Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(spawning());
    }
    public void removeObj(GameObject obj)
    {
        for (int i = 0; i < spawnedObjects.Length; i++)
        {
            if (spawnedObjects[i] != null && spawnedObjects[i].Equals(obj))
            {
                spawnedObjects[i] = null;
            }
        }
    }
    private IEnumerator spawning()
    {
        while (true)
        {
            for (int i = 0; i < spawnedObjects.Length; i++)
            {
                if (spawnedObjects[i] == null)
                {
                    spawnedObjects[i] = Instantiate(objPrefab, spawnPositions[i].position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(spawnWaitTime);
        }
    }

}
