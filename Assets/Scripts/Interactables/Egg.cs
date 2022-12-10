using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Egg : BasicInteractable, IPickup, Iinteractable
{
    private IHold basicHolder;
    private IPickup basicPickup;
    public bool CanPickup { get => basicPickup.CanPickup; set => basicPickup.CanPickup = value; }
    public IHold PickupHolder { get => basicHolder; set => basicHolder = value; }
    public GameObject PickupObj { get => basicPickup.PickupObj; }

    public void Initialize()
    {
        basicPickup = new BasicPickup(gameObject);
        ObjectManager.Instance.addInteractable(gameObject);
    }


    public void pickup(IHold newHolder)
    {
        newHolder.StartHolding(PickupHolder, this);
        basicPickup.pickup(newHolder);
    }
    public void setDown(GameObject obj, IHold newHolder, GameObject playerHoldingObj)
    {

        newHolder.StartHolding(PickupHolder, this);
        basicPickup.setDown(obj, newHolder, playerHoldingObj);

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
