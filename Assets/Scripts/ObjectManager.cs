using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] float spawnWaitTime;
    [SerializeField] GameObject eggPrefab;
    private GameObject[] eggs = new GameObject[3];

    public static ObjectManager Instance { get; private set; }
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

    private List<GameObject> interactables = new List<GameObject>();

    public List<GameObject> Interactables { get => interactables; }


    public void addInteractable(GameObject obj)
    {
        interactables.Add(obj);
    }
    public void removeInteractable(GameObject obj)
    {
        obj.SetActive(false);
        interactables.Remove(obj);
    }

    public void pickupEgg(GameObject egg)
    {
        for (int i = 0; i < eggs.Length; i++)
        {
            if (eggs[i] != null && eggs[i].Equals(egg))
            {
                eggs[i] = null;
            }
        }
    }
    private IEnumerator spawning()
    {
        while(true)
        {
            for(int i = 0; i < eggs.Length; i++)
            {
                if (eggs[i] == null)
                {
                    eggs[i] = Instantiate(eggPrefab, spawnPositions[i].position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(spawnWaitTime);
        }
    }

}
