using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Egg : BasicInteractable, IPickup, Iinteractable
{
    private IPickup basicPickup;
    public bool CanPickup { get => basicPickup.CanPickup; set => basicPickup.CanPickup = value; }
    public IHold PickupHolder { get => basicPickup.PickupHolder; set => basicPickup.PickupHolder = value; }
    public GameObject PickupObj { get => basicPickup.PickupObj; }

    public void Initialize()
    {
        basicPickup = new BasicPickup(gameObject);
        ObjectManager.Instance.addInteractable(gameObject);
    }


    public void pickup(IHold newHolder)
    {
        newHolder.StartHolding(PickupHolder, this, null);
        basicPickup.pickup(newHolder);
    }
    public void setDown(IHold newHolder)
    {
        newHolder.StartHolding(PickupHolder, this, null);
        basicPickup.setDown(newHolder);

        // we can't pickup the egg after setting on a pan or plate
        if (newHolder.HolderObj.CompareTag(StaticStrings.Pan) || newHolder.HolderObj.CompareTag(StaticStrings.Plate))
        {
            CanPickup = false;
        }
    }
    public void drop()
    {
        PickupHolder.StopHolding(this);

        basicPickup.drop();
    }

}
