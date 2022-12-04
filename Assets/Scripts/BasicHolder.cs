using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHolder : BasicInteractable, IHold
{
    [SerializeField] private GameObject currentHoldingObj = null;

    public GameObject CurrentlyHoldingObj { get => currentHoldingObj; set => currentHoldingObj = value; }
    [SerializeField] private Transform holdPosition;
    public Transform HoldPosition { get => holdPosition; set => holdPosition = value; }

}
