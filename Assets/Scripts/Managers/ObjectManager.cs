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

    public void RemoveInteractableAfterSeconds(GameObject obj, float waitTime)
    {
        StartCoroutine(WaitThenRemove(obj, waitTime));
    }
    private IEnumerator WaitThenRemove(GameObject obj, float despawnWaitTime)
    {
        yield return new WaitForSeconds(despawnWaitTime);

        ObjectManager.Instance.RemoveInteractable(obj);
    }

    public void AddInteractable(GameObject obj)
    {
        interactables.Add(obj);
    }
    public void RemoveInteractable(GameObject obj)
    {
        interactables.Remove(obj);

        obj.SetActive(false);
    }

}
