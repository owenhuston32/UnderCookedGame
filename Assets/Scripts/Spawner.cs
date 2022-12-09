using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] float spawnWaitTime;
    [SerializeField] GameObject objPrefab;
    [SerializeField] private GameObject holder;
    [SerializeField] Transform parent;

    public void SpawnObj()
    {
        spawn();
    }

    public void WaitThenSpawn()
    {
            StartCoroutine(spawning());
    }

    private void spawn()
    {
        GameObject spawnedObject = Instantiate(objPrefab, spawnPosition.position, Quaternion.identity, parent);
        
        spawnedObject.GetComponent<SpawnedObj>().Spawner = this;
        if (holder != null)
        {
            // put obj on holder
            IPickup pickup = spawnedObject.GetComponent(typeof(IPickup)) as IPickup;

            pickup.Initialize();

            pickup.setDown(spawnedObject, holder.GetComponent(typeof(IHold)) as IHold, null);

        }
    }

    private IEnumerator spawning()
    {
        yield return new WaitForSeconds(spawnWaitTime);
        spawn();
    }

}
