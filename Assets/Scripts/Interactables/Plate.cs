using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Plate : BasicInteractable, IPickup, IHold, Iinteractable
{
    [SerializeField] GameObject plateRespawnHolderObj;
    [SerializeField] Transform[] holdPositions;
    private IHold basicHolder;
    private IPickup basicPickup;
    public bool CanPickup { get => basicPickup.CanPickup; set => basicPickup.CanPickup = value; }
    public GameObject PickupObj { get => basicPickup.PickupObj; }
    public GameObject HolderObj { get => gameObject; }

    public IHold PickupHolder { get => basicPickup.PickupHolder; set => basicPickup.PickupHolder = value; }
    public GameObject CurrentlyHoldingObj { get => basicHolder.CurrentlyHoldingObj; set => basicHolder.CurrentlyHoldingObj = value; }

    public void Initialize()
    {
        basicHolder = new BasicHolder(gameObject);
        basicPickup = new BasicPickup(gameObject);
        ObjectManager.Instance.AddInteractable(gameObject);
    }

    public void Pickup(IHold newHolder)
    {
        newHolder.StartHolding(PickupHolder, PickupObj, null);
        basicPickup.Pickup(newHolder);
    }

    public void SetDown(IHold newHolder)
    {
        newHolder.StartHolding(PickupHolder, PickupObj, null);
        basicPickup.SetDown(newHolder);
        
    }
    public void Drop()
    {
        PickupHolder.StopHolding(PickupObj);
        basicPickup.Drop();
    }

    public void StartHolding(IHold oldHolder, GameObject pickupObj, Transform followTransform)
    {
        basicHolder.StartHolding(oldHolder, pickupObj, holdPositions[0]);
    }

    public void StopHolding(GameObject pickupObj)
    {
        basicHolder.StopHolding(pickupObj);
    }

}
