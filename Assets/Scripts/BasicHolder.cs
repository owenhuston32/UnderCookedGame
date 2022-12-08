using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHolder :  IHold
{
    private GameObject holderObj;
    private Transform holdPosition;
    private GameObject currentHoldingObj = null;
    public GameObject HolderObj { get => holderObj; }
    public BasicHolder(GameObject obj, Transform holdPosition)
    {
        holderObj = obj;
        this.holdPosition = holdPosition;
    }

    public GameObject CurrentlyHoldingObj { get => currentHoldingObj; set => currentHoldingObj = value; }

    public Transform HoldPosition { get => holdPosition; set => holdPosition = value; }

}
