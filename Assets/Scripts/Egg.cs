using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : BasicInteractable, IPickup, Iinteractable
{
    private IHold basicHolder;
    private IPickup basicPickup;
    public bool CanPickup { get => basicPickup.CanPickup; set => basicPickup.CanPickup = value; }
    public IHold Holder { get => basicHolder; set => basicHolder = value; }
    public GameObject PickupObj { get => basicPickup.PickupObj; }

    private void Start()
    {
        basicPickup = new BasicPickup(gameObject);
        ObjectManager.Instance.addInteractable(gameObject);
    }


    public void interact(GameObject player, GameObject highlightedObj, GameObject playerHoldingObj)
    {

    }

    public void pickup(IHold newHolder)
    {
        basicPickup.pickup(newHolder);
    }
    public void setDown(GameObject obj, IHold holder)
    {
        basicPickup.setDown(obj, holder);
        if (holder.HolderObj.CompareTag("Pan"))
            CanPickup = false;
    }
    public void drop()
    {
        basicPickup.drop();
    }

}
