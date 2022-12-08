using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : BasicInteractable, Iinteractable, IHold
{
    [SerializeField] private Transform holdPosition;
    private IHold holder;
    public GameObject HolderObj { get => gameObject; }

    public GameObject CurrentlyHoldingObj { get => holder.CurrentlyHoldingObj; set => holder.CurrentlyHoldingObj = value; }

    public Transform HoldPosition { get => holdPosition; set => holdPosition = value; }

    // Start is called before the first frame update
    void Start()
    {
        holder = new BasicHolder(gameObject, holdPosition);
        ObjectManager.Instance.addInteractable(gameObject);
    }

    public void interact(GameObject player, GameObject highlightedObj, GameObject playerHoldingObj)
    {

    }


}
