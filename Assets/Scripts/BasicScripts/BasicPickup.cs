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
        canPickup = false;
        // if we have an old holder remove it
        if (this.pickupHolder != null)
        {
            this.pickupHolder.CurrentlyHoldingObj = null;
        }

        pickupObj.GetComponent<Collider>().enabled = false;
        pickupObj.GetComponent<Rigidbody>().useGravity = false;
        this.pickupHolder = newHolder;


        newHolder.CurrentlyHoldingObj = pickupObj;
        followPosition.FollowTransform = newHolder.HoldPosition;
    }

    public void setDown(GameObject obj, IHold newHolder, GameObject playerHoldingObj)
    {

        canPickup = true;
        // remove old holder
        if(pickupHolder != null)
            pickupHolder.CurrentlyHoldingObj = null;

        // set new holder
        pickupHolder = newHolder;
        pickupHolder.CurrentlyHoldingObj = obj;
        followPosition.FollowTransform = pickupHolder.HoldPosition;
    }
    public void drop()
    {
        canPickup = true;
        pickupObj.GetComponent<Collider>().enabled = true;
        pickupObj.GetComponent<Rigidbody>().useGravity = true;
        followPosition.FollowTransform = null;
        pickupHolder.CurrentlyHoldingObj = null;
        this.pickupHolder = null;
    }
}
