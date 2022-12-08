using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPickup : IPickup
{
    private GameObject pickupObj;
    private bool canPickup = true;
    private FollowPosition followPosition;
    private IHold holder = null;
    public IHold Holder { get => holder; set => holder = value; }
    public bool CanPickup { get => canPickup; set => canPickup = value; }
    public GameObject PickupObj { get => pickupObj; }
    public BasicPickup(GameObject obj)
    {
        pickupObj = obj;
        followPosition = obj.GetComponent<FollowPosition>();
    }

    public void pickup(IHold newHolder)
    {
        canPickup = false;
        // if we have an old holder remove it
        if (this.holder != null)
        {
            this.holder.CurrentlyHoldingObj = null;
        }

        pickupObj.GetComponent<Collider>().enabled = false;
        pickupObj.GetComponent<Rigidbody>().useGravity = false;
        this.holder = newHolder;


        newHolder.CurrentlyHoldingObj = pickupObj;
        followPosition.FollowTransform = newHolder.HoldPosition;
    }

    public void setDown(GameObject obj, IHold newHolder)
    {
        canPickup = true;
        // remove old holder
        this.holder.CurrentlyHoldingObj = null;

        // set new holder
        this.holder = newHolder;
        holder.CurrentlyHoldingObj = obj;
        followPosition.FollowTransform = holder.HoldPosition;
    }
    public void drop()
    {
        canPickup = true;
        pickupObj.GetComponent<Collider>().enabled = true;
        pickupObj.GetComponent<Rigidbody>().useGravity = true;
        followPosition.FollowTransform = null;
        holder.CurrentlyHoldingObj = null;
        this.holder = null;
    }
}
