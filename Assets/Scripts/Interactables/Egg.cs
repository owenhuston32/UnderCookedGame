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
        ObjectManager.Instance.AddInteractable(gameObject);
    }


    public void Pickup(IHold newHolder)
    {
        newHolder.StartHolding(PickupHolder, PickupObj, null);
        basicPickup.Pickup(newHolder);
    }
    public void SetDown(IHold newHolder)
    {
        newHolder.StartHolding(PickupHolder, PickupObj, null);
        basicPickup.SetDown(newHolder);

        // we can't pickup the egg after setting on a pan or plate
        if (newHolder.HolderObj.CompareTag(StaticStrings.Pan) || newHolder.HolderObj.CompareTag(StaticStrings.Plate))
        {
            CanPickup = false;
        }
    }
    public void Drop()
    {
        PickupHolder.StopHolding(PickupObj);

        basicPickup.Drop();
    }

}
