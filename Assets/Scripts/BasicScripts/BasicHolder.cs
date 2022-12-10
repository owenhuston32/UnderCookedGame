using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHolder :  IHold
{
    private GameObject holderObj;
    private Transform[] holdPositions;
    private GameObject currentHoldingObj = null;
    public GameObject HolderObj { get => holderObj; }
    public GameObject CurrentlyHoldingObj { get => currentHoldingObj; set => currentHoldingObj = value; }
    public Transform[] HoldPositions { get => holdPositions; }

    public BasicHolder(GameObject obj, Transform[] holdPositions)
    {
        holderObj = obj;
        this.holdPositions = holdPositions;
    }

    public void StartHolding(IHold oldHolder, IPickup pickup)
    {
        // stop following old holder
        if(oldHolder != null)
        {
            oldHolder.StopHolding(pickup);
            oldHolder.CurrentlyHoldingObj = null;
        }

        currentHoldingObj = pickup.PickupObj;
        pickup.PickupObj.GetComponent<FollowPosition>().startFollowing(holdPositions[0]);

        pickup.PickupHolder = this;
    }

    public void StopHolding(IPickup pickup)
    {
        currentHoldingObj = null;
        pickup.PickupObj.GetComponent<FollowPosition>().stopFollowing();
    }
}
