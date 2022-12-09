using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : BasicInteractable, IPickup, IHold, Iinteractable
{
    [SerializeField] Transform holdPosition;
    private IHold basicHolder;
    private IPickup basicPickup;
    public bool CanPickup { get => basicPickup.CanPickup; set => basicPickup.CanPickup = value; }
    public GameObject PickupObj { get => basicPickup.PickupObj; }
    public GameObject HolderObj { get => gameObject; }

    public void Initialize()
    {
        basicHolder = new BasicHolder(gameObject, holdPosition);
        basicPickup = new BasicPickup(gameObject);
        ObjectManager.Instance.addInteractable(gameObject);
    }



    public IHold PickupHolder { get => basicPickup.PickupHolder; set => basicPickup.PickupHolder = value; }
    public GameObject CurrentlyHoldingObj { get => basicHolder.CurrentlyHoldingObj; set => basicHolder.CurrentlyHoldingObj = value; }
    public Transform HoldPosition { get => basicHolder.HoldPosition; set => basicHolder.HoldPosition = value; }

    public void pickup(IHold newHolder)
    {
        if (CurrentlyHoldingObj != null)
        {
            CurrentlyHoldingObj.GetComponent<Cook>().stopCook();
            CurrentlyHoldingObj.GetComponent<FollowPosition>().startFollowing(holdPosition);
        }
        basicPickup.pickup(newHolder);
    }

    public void setDown(GameObject obj, IHold newHolder, GameObject playerHoldingObj)
    {

        if(newHolder.HolderObj.CompareTag("Burner"))
        {
            if(CurrentlyHoldingObj != null && CurrentlyHoldingObj.CompareTag("Food"))
            {
                CurrentlyHoldingObj.GetComponent<Cook>().cook();
            }
        }

        // set the food on the plate and drop pan
        if(newHolder.HolderObj.CompareTag("Plate"))
        {
            IPickup food = CurrentlyHoldingObj.GetComponent(typeof(IPickup)) as IPickup;
            food.setDown(food.PickupObj, newHolder, playerHoldingObj);
            drop();
        }
        else
        {

            basicPickup.setDown(obj, newHolder, playerHoldingObj);
        }

    }
    public void drop()
    {
        basicPickup.drop();
    }

}
