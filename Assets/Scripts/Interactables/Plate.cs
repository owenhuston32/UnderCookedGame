using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Plate : BasicInteractable, IPickup, IHold, Iinteractable
{
    [SerializeField] GameObject plateRespawnHolderObj;
    [SerializeField] Transform[] holdPositions;
    private IHold basicHolder;
    private IPickup basicPickup;
    public bool CanPickup { get => basicPickup.CanPickup; set => basicPickup.CanPickup = value; }
    public GameObject PickupObj { get => basicPickup.PickupObj; }
    public GameObject HolderObj { get => gameObject; }

    public IHold PickupHolder { get => basicPickup.PickupHolder; set => basicPickup.PickupHolder = value; }
    public GameObject CurrentlyHoldingObj { get => basicHolder.CurrentlyHoldingObj; set => basicHolder.CurrentlyHoldingObj = value; }
    public Transform[] HoldPositions { get => basicHolder.HoldPositions; }

    public void Initialize()
    {
        basicHolder = new BasicHolder(gameObject, holdPositions);
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
        
    }
    public void drop()
    {
        PickupHolder.StopHolding(this);
        basicPickup.drop();
    }

    public void StartHolding(IHold oldHolder, IPickup pickup)
    {
        basicHolder.StartHolding(oldHolder, pickup);
    }

    public void StopHolding(IPickup pickup)
    {
        basicHolder.StopHolding(pickup);
    }

}
