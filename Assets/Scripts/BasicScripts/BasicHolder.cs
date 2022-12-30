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

    public void StartHolding(IHold oldHolder, GameObject pickupObj, Transform followTransform)
    {
        // stop following old holder
        if(oldHolder != null)
        {
            oldHolder.StopHolding(pickupObj);
        }

        currentHoldingObj = pickupObj;

        pickupObj.GetComponent<Collider>().enabled = false;
        pickupObj.GetComponent<Rigidbody>().isKinematic = true;


        pickupObj.transform.position = followTransform.position;
        pickupObj.transform.localRotation = followTransform.transform.rotation;
        pickupObj.transform.SetParent(followTransform);
    }

    public void StopHolding(GameObject pickupObj)
    {
        currentHoldingObj = null;

        pickupObj.GetComponent<Collider>().enabled = true;
        pickupObj.GetComponent<Rigidbody>().isKinematic = false;

        pickupObj.transform.parent = null;
    }
}
