using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPickup : BasicInteractable, IPickup
{
    [SerializeField] private FollowPosition followPosition;
    private GameObject holder = null;
    public GameObject Holder { get => holder; set => holder = value; }

    public void pickup(GameObject holder)
    {
        // if we have an old holder remove it
        if (this.holder != null)
            this.holder.GetComponent<BasicHolder>().CurrentlyHoldingObj = null;


        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        this.holder = holder;
        holder.GetComponent<BasicHolder>().CurrentlyHoldingObj = gameObject;
        followPosition.FollowTransform = holder.GetComponent<BasicHolder>().HoldPosition;
    }

    public void setDown(GameObject obj, GameObject holder)
    {
        // remove old holder
        this.holder.GetComponent<BasicHolder>().CurrentlyHoldingObj = null;

        // set new holder
        this.holder = holder;
        holder.GetComponent<BasicHolder>().CurrentlyHoldingObj = obj;
        followPosition.FollowTransform = holder.GetComponent<BasicHolder>().HoldPosition;
    }
    public void drop()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
        followPosition.FollowTransform = null;
        this.holder.GetComponent<BasicHolder>().CurrentlyHoldingObj = null;
        this.holder = null;
    }
}
