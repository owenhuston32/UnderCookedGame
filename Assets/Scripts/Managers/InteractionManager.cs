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
    public void Interact(GameObject player, GameObject highlightedObj, GameObject playerHoldingObj)
    {
        // if we arent highlighting anything drop whatever we are holding
        if (highlightedObj == null)
        {
            Drop(playerHoldingObj);
        }

        // we arent holding anything we can pick stuff up
        else if (playerHoldingObj == null)
        {
            Pickup(highlightedObj, player);
        }

        // if we are holding something and are highlighting something
        else
        {

            IHold highlightedHolder = highlightedObj.GetComponent(typeof(IHold)) as IHold;
            IPickup highlightedPickup = highlightedObj.GetComponent(typeof(IPickup)) as IPickup;

            if (highlightedHolder != null)
            {
                SetDown(playerHoldingObj, highlightedHolder);
            }
            else if (highlightedPickup != null)
            {
                // place something on top of what the player is holding
                Pickup(highlightedObj, playerHoldingObj);
            }
        }
    }

    private void Drop(GameObject playerHoldingObj)
    {
        if (playerHoldingObj != null)
        {
            IPickup playerHoldingObjPickup = playerHoldingObj.GetComponent(typeof(IPickup)) as IPickup;
            playerHoldingObjPickup.Drop();
        }
    }
    private void SetDown(GameObject obj, IHold newHolder)
    {
        IPickup pickup = obj.GetComponent(typeof(IPickup)) as IPickup;

        if (pickup != null)
            pickup.SetDown(newHolder);
    }
    private void Pickup(GameObject obj, GameObject holder)
    {
        IPickup pickup = obj.GetComponent(typeof(IPickup)) as IPickup;
        if (pickup != null)
        {
            pickup.Pickup(holder.GetComponent(typeof(IHold)) as IHold);
        }
        // interact with crate
        else
        {
            if (obj.TryGetComponent<FoodCrate>(out var crate))
            {
                crate.SpawnObj(holder.GetComponent(typeof(IHold)) as IHold);
            }
        }
    }
}
