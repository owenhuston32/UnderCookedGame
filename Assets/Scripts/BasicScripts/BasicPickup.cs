using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPickup : IPickup
{
    private GameObject pickupObj;
    private bool canPickup = true;
    private FollowPosition followPosition;
    private IHold pickupHolder = null;
    public IHold PickupHolder { get => pickupHolder; set => pickupHolder = value; }
    public bool CanPickup { get => canPickup; set => canPickup = value; }
    public GameObject PickupObj { get => pickupObj; }
    public BasicPickup(GameObject obj)
    {
        pickupObj = obj;
        followPosition = obj.GetComponent<FollowPosition>();
    }

    public void Initialize()
    {

    }

    public void pickup(IHold newHolder)
    {
        pickupHolder = newHolder;
        canPickup = false;
    }

    public void setDown(IHold newHolder)
    {
        pickupHolder = newHolder;
        canPickup = true;

    }
    public void drop()
    {
        pickupHolder = null;
        canPickup = true;
    }
}
