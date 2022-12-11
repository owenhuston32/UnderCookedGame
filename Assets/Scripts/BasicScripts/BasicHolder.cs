using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHolder :  IHold
{
    private GameObject holderObj;
    private GameObject currentHoldingObj = null;
    public GameObject HolderObj { get => holderObj; }
    public GameObject CurrentlyHoldingObj { get => currentHoldingObj; set => currentHoldingObj = value; }
    public BasicHolder(GameObject obj)
    {
        holderObj = obj;
    }

    public void StartHolding(IHold oldHolder, IPickup pickup, Transform followTransform)
    {
        // stop following old holder
        if(oldHolder != null)
        {
            oldHolder.StopHolding(pickup);
        }

        currentHoldingObj = pickup.PickupObj;

        pickup.PickupObj.GetComponent<Collider>().enabled = false;
        pickup.PickupObj.GetComponent<Rigidbody>().isKinematic = true;


        pickup.PickupObj.transform.position = followTransform.position;
        pickup.PickupObj.transform.localRotation = followTransform.transform.rotation;
        pickup.PickupObj.transform.SetParent(followTransform);
        //pickup.PickupObj.GetComponent<FollowPosition>().startFollowing(followTransform);
    }

    public void StopHolding(IPickup pickup)
    {
        currentHoldingObj = null;

        pickup.PickupObj.GetComponent<Collider>().enabled = true;
        pickup.PickupObj.GetComponent<Rigidbody>().isKinematic = false;

        pickup.PickupObj.transform.parent = null;
        //pickup.PickupObj.GetComponent<FollowPosition>().stopFollowing();
    }
}
