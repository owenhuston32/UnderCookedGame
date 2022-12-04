using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] float spawnWaitTime;
    [SerializeField] GameObject objPrefab;
    [SerializeField] private GameObject[] spawnedObjects;
    [SerializeField] private GameObject[] holders;
    [SerializeField] Transform parent;
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
                    spawnedObjects[i] = Instantiate(objPrefab, spawnPositions[i].position, Quaternion.identity, parent);
                    if(spawnedObjects[i].CompareTag("Plate"))
                    {
                        // put plate on table
                        spawnedObjects[i].GetComponent<BasicPickup>().Holder = holders[i];


                        spawnedObjects[i].GetComponent<Collider>().enabled = false;
                        spawnedObjects[i].GetComponent<Rigidbody>().useGravity = false;

                        // add to new holder
                        IHold newHolder = holders[i].GetComponent(typeof(IHold)) as IHold;
                        newHolder.CurrentlyHoldingObj = gameObject;

                        FollowPosition followScript = spawnedObjects[i].GetComponent<FollowPosition>();
                        followScript.FollowTransform = newHolder.HoldPosition;


                    }
                }
            }

            yield return new WaitForSeconds(spawnWaitTime);
        }
    }

}
