
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burner : MonoBehaviour, IHold, Iinteractable
{
    private GameObject currentHoldingObj = null;
    public IHighlight highlight { get => gameObject.GetComponent<IHighlight>(); }

    public GameObject CurrentlyHoldingObj { get => currentHoldingObj; set => currentHoldingObj = value; }
    [SerializeField] private Transform holdPosition;
    public Transform HoldPosition { get => holdPosition; set => holdPosition = value; }
    private void Start()
    {
        ObjectManager.Instance.addInteractable(gameObject);
    }

    public void interact(GameObject player, GameObject highlightedObj, GameObject playerHoldingObj)
    {

    }
}