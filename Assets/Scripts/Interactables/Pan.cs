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
        newHolder.StartHolding(PickupHolder, this, null);
        basicPickup.pickup(newHolder);
    }

    public void setDown(IHold newHolder)
    {
        // set the food on the plate and drop pan
        if(newHolder.HolderObj.CompareTag(StaticStrings.Plate))
        {
            IPickup food = CurrentlyHoldingObj.GetComponent(typeof(IPickup)) as IPickup;
            food.setDown(newHolder);
            drop();
        }
        else
        {
            newHolder.StartHolding(PickupHolder, this, null);
            basicPickup.setDown(newHolder);
        }

    }
    public void drop()
    {
        PickupHolder.StopHolding(this);
        basicPickup.drop();
    }

    public void StartHolding(IHold oldHolder, IPickup pickup, Transform followTransform)
    {
        if(pickup.PickupObj.CompareTag(StaticStrings.Food))
        {
            if(PickupHolder != null && PickupHolder.HolderObj.CompareTag(StaticStrings.Burner))
            {
                pickup.PickupObj.GetComponent<Cook>().cook();
            }
            else
            {
                pickup.PickupObj.GetComponent<Cook>().stopCook();
            }
        }
        basicHolder.StartHolding(oldHolder, pickup, holdPositions[0]);
    }

    public void StopHolding(IPickup pickup)
    {
        basicHolder.StopHolding(pickup);
    }
}
