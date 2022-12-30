using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : BasicInteractable, IPickup, IHold, Iinteractable
{
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
        ObjectManager.Instance.addInteractable(gameObject);
    }

    public void pickup(IHold newHolder)
    {
        if (CurrentlyHoldingObj != null)
        {
            CurrentlyHoldingObj.GetComponent<Cook>().stopCook();
        }
        newHolder.StartHolding(PickupHolder, PickupObj, null);
        basicPickup.pickup(newHolder);
    }

    public void setDown(IHold newHolder)
    {
        // set the food on the plate and drop pan
        if(newHolder.HolderObj.CompareTag(StaticStrings.Plate))
        {
            IPickup food = CurrentlyHoldingObj.GetComponent(typeof(IPickup)) as IPickup;
            food.setDown(newHolder);
        }
        else
        {
            newHolder.StartHolding(PickupHolder, PickupObj, null);
            basicPickup.setDown(newHolder);
        }

    }
    public void drop()
    {
        PickupHolder.StopHolding(PickupObj);
        basicPickup.drop();
    }

    public void StartHolding(IHold oldHolder, GameObject pickupObj, Transform followTransform)
    {
        if(pickupObj.CompareTag(StaticStrings.Food))
        {
            if(PickupHolder != null && PickupHolder.HolderObj.CompareTag(StaticStrings.Burner))
            {
                pickupObj.GetComponent<Cook>().cook();
            }
            else
            {
                pickupObj.GetComponent<Cook>().stopCook();
            }
        }
        basicHolder.StartHolding(oldHolder, pickupObj, holdPositions[0]);
    }

    public void StopHolding(GameObject pickupObj)
    {
        basicHolder.StopHolding(pickupObj);
    }
}
