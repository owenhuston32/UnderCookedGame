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
    private void Start()
    {
        basicHolder = new BasicHolder(gameObject, holdPosition);
        basicPickup = new BasicPickup(gameObject);
        ObjectManager.Instance.addInteractable(gameObject);
    }

    public IHold Holder { get => basicPickup.Holder; set => basicPickup.Holder = value; }
    public GameObject CurrentlyHoldingObj { get => basicHolder.CurrentlyHoldingObj; set => basicHolder.CurrentlyHoldingObj = value; }
    public Transform HoldPosition { get => basicHolder.HoldPosition; set => basicHolder.HoldPosition = value; }

    public void interact(GameObject player, GameObject highlightedObj, GameObject playerHoldingObj)
    {
        if (playerHoldingObj.CompareTag("Food"))
        {

            // if the pan is on the burner start cooking
            if (Holder != null
                && Holder.HolderObj.CompareTag("Burner"))
            {
                playerHoldingObj.GetComponent<Cook>().cook();
            }
            IPickup playerHoldingObjPickup = playerHoldingObj.GetComponent(typeof(IPickup)) as IPickup;
            playerHoldingObjPickup.setDown(playerHoldingObj, this);
        }
    }

    public void pickup(IHold newHolder)
    {
        if (CurrentlyHoldingObj != null)
        {
            CurrentlyHoldingObj.GetComponent<Cook>().stopCook();
        }
        basicPickup.pickup(newHolder);
    }

    public void setDown(GameObject obj, IHold holder)
    {
        basicPickup.setDown(obj, holder);
    }
    public void drop()
    {
        basicPickup.drop();
    }

}
