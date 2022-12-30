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
        ObjectManager.Instance.AddInteractable(gameObject);
    }

    public void Pickup(IHold newHolder)
    {
        if (CurrentlyHoldingObj != null)
        {
            CurrentlyHoldingObj.GetComponent<Cook>().StopCook();
        }
        newHolder.StartHolding(PickupHolder, PickupObj, null);
        basicPickup.Pickup(newHolder);
    }

    public void SetDown(IHold newHolder)
    {
        // set the food on the plate and drop pan
        if(newHolder.HolderObj.CompareTag(StaticStrings.Plate))
        {
            IPickup food = CurrentlyHoldingObj.GetComponent(typeof(IPickup)) as IPickup;
            food.SetDown(newHolder);
        }
        else
        {
            newHolder.StartHolding(PickupHolder, PickupObj, null);
            basicPickup.SetDown(newHolder);
        }

    }
    public void Drop()
    {
        PickupHolder.StopHolding(PickupObj);
        basicPickup.Drop();
    }

    public void StartHolding(IHold oldHolder, GameObject pickupObj, Transform followTransform)
    {
        // start cooking if the pan is starting to hold food
        // and a burner is holding the pan
        if(pickupObj.CompareTag(StaticStrings.Food))
        {
            if(PickupHolder.HolderObj.CompareTag(StaticStrings.Burner))
            {
                pickupObj.GetComponent<Cook>().StartCooking();
            }
        }
        basicHolder.StartHolding(oldHolder, pickupObj, holdPositions[0]);
    }

    public void StopHolding(GameObject pickupObj)
    {
        basicHolder.StopHolding(pickupObj);
    }
}
