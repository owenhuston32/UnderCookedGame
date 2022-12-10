using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InteractionManager : MonoBehaviour, Iinteractable
{
    public static InteractionManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void interact(GameObject player, GameObject highlightedObj, GameObject playerHoldingObj)
    {
        // if we arent highlighting anything drop whatever we are holding
        if (highlightedObj == null)
        {
            drop(playerHoldingObj);
        }

        // we arent holding anything we can pick stuff up
        else if (playerHoldingObj == null)
        {
            pickup(highlightedObj, player);
        }

        // if we are holding something and are highlighting something
        else
        {

            IHold highlightedHolder = highlightedObj.GetComponent(typeof(IHold)) as IHold;
            IPickup highlightedPickup = highlightedObj.GetComponent(typeof(IPickup)) as IPickup;

            if (highlightedHolder != null)
            {
                setDown(playerHoldingObj, highlightedHolder, playerHoldingObj);
            }
            else if (highlightedPickup != null)
            {
                // place something on top of what the player is holding
                pickup(highlightedObj, playerHoldingObj);
            }
        }
    }

    private void drop(GameObject playerHoldingObj)
    {
        if (playerHoldingObj != null)
        {
            IPickup playerHoldingObjPickup = playerHoldingObj.GetComponent(typeof(IPickup)) as IPickup;
            playerHoldingObjPickup.drop();
        }
    }
    private void setDown(GameObject obj, IHold newHolder, GameObject playerHoldingObj)
    {
        IPickup pickup = obj.GetComponent(typeof(IPickup)) as IPickup;

        if (pickup != null)
            pickup.setDown(newHolder);
    }
    private void pickup(GameObject obj, GameObject holder)
    {
        IPickup pickup = obj.GetComponent(typeof(IPickup)) as IPickup;
        if (pickup != null)
        {
            pickup.pickup(holder.GetComponent(typeof(IHold)) as IHold);
        }
        // interact with crate
        else
        {
            FoodCrate crate = obj.GetComponent<FoodCrate>();
            if (obj.GetComponent<FoodCrate>() != null && obj)
            {
                crate.SpawnObj(holder.GetComponent(typeof(IHold)) as IHold);
            }
        }
    }
}
