using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPickup : IPickup
{
    private GameObject pickupObj;
    private bool canPickup = true;
    private IHold pickupHolder = null;
    public IHold PickupHolder { get => pickupHolder; set => pickupHolder = value; }
    public bool CanPickup { get => canPickup; set => canPickup = value; }
    public GameObject PickupObj { get => pickupObj; }
    public BasicPickup(GameObject obj)
    {
        pickupObj = obj;
    }

    public void Initialize()
    {

    }

    public void Pickup(IHold newHolder)
    {
        pickupHolder = newHolder;
        canPickup = false;
    }

    public void SetDown(IHold newHolder)
    {
        pickupHolder = newHolder;
        canPickup = true;

    }
    public void Drop()
    {
        pickupHolder = null;
        canPickup = true;
    }
}
