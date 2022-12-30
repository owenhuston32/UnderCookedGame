using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] float spawnWaitTime;
    [SerializeField] GameObject objPrefab;
    [SerializeField] Transform parent;

    public GameObject SpawnObj(IHold holder)
    {
        return Spawn(holder);
    }

    public void WaitThenSpawn(IHold holder)
    {
        StartCoroutine(Spawning(holder));
    }

    private GameObject Spawn(IHold holder)
    {
        GameObject spawnedObject = Instantiate(objPrefab, spawnPosition.position, Quaternion.identity, parent);
        
        spawnedObject.GetComponent<SpawnedObj>().Spawner = this;
        if (holder != null)
        {
            // put obj on holder
            IPickup pickup = spawnedObject.GetComponent(typeof(IPickup)) as IPickup;

            pickup.Initialize();

            pickup.SetDown(holder);

        }
        return spawnedObject;
    }

    private IEnumerator Spawning(IHold holder)
    {
        yield return new WaitForSeconds(spawnWaitTime);
        Spawn(holder);
    }

}
