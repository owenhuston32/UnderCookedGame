using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

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
    private List<GameObject> interactables = new List<GameObject>();

    public List<GameObject> Interactables { get => interactables; }

    public void removeInteractableAfterSeconds(GameObject obj, float waitTime)
    {
        StartCoroutine(waitThenRemove(obj, waitTime));
    }
    private IEnumerator waitThenRemove(GameObject obj, float despawnWaitTime)
    {
        yield return new WaitForSeconds(despawnWaitTime);

        ObjectManager.Instance.removeInteractable(obj);
    }

    public void addInteractable(GameObject obj)
    {
        interactables.Add(obj);
    }
    public void removeInteractable(GameObject obj)
    {
        interactables.Remove(obj);

        obj.SetActive(false);
    }

}
