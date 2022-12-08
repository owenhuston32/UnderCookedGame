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
        Debug.Log(player + " : " + highlightedObj + " : " + playerHoldingObj);


        // if we arent highlighting anything drop whatever we are holding
        if (highlightedObj == null)
        {
            if (playerHoldingObj != null)
            {
                IPickup pickup = playerHoldingObj.GetComponent(typeof(IPickup)) as IPickup;
                pickup.drop();
            }
        }

        // we arent holding anything we can pick stuff up
        else if (playerHoldingObj == null)
        {
            IPickup pickup = highlightedObj.GetComponent(typeof(IPickup)) as IPickup;
            if (pickup != null)
            {
                pickup.pickup(player.GetComponent(typeof(IHold)) as IHold);
            }
        }

        // if we are holding something and are highlighting something
        else
        {

            IHold holder = highlightedObj.GetComponent(typeof(IHold)) as IHold;
            IPickup pickup = highlightedObj.GetComponent(typeof(IPickup)) as IPickup;

            if (holder != null)
            {
                Iinteractable interactable = highlightedObj.GetComponent(typeof(Iinteractable)) as Iinteractable;
                interactable.interact(player, highlightedObj, playerHoldingObj);

            }
            else if (pickup != null)
            {
                IHold playerHoldingObjHolder = playerHoldingObj.GetComponent(typeof(IHold)) as IHold;
                pickup.pickup(playerHoldingObjHolder);
            }
        }
    }
    /*
    private void interactWithBurner(GameObject playerHoldingObj, GameObject highlightedObj)
    {
        if (playerHoldingObj.CompareTag("Pan"))
        {
            if (playerHoldingObj.GetComponent<BasicHolder>().CurrentlyHoldingObj != null)
            {
                playerHoldingObj.GetComponent<BasicHolder>().CurrentlyHoldingObj.GetComponent<Cook>().cook();
            }
            playerHoldingObj.GetComponent<BasicPickup>().setDown(playerHoldingObj, highlightedObj);
        }
    }
    private void interactWithPlate(GameObject playerHoldingObj, GameObject highlightedObj)
    {
        // if we are carrying a pan set the food down and drop pan
        if(playerHoldingObj.CompareTag("Pan"))
        {
            BasicHolder panHolder = playerHoldingObj.GetComponent<BasicHolder>();
            panHolder.CurrentlyHoldingObj.GetComponent<BasicPickup>().setDown(panHolder.CurrentlyHoldingObj, highlightedObj);
            playerHoldingObj.GetComponent<BasicPickup>().drop();
        }
    }
    private void interactWithSubmissionTable(GameObject player, GameObject playerHoldingObj, GameObject highlightedObj)
    {
        if(playerHoldingObj.CompareTag("Plate"))
        {
            ScoreManager.Instance.AddScore(player, playerHoldingObj.GetComponent<BasicHolder>().CurrentlyHoldingObj);

            // remove food from plate
            GameObject food = playerHoldingObj.GetComponent<BasicHolder>().CurrentlyHoldingObj;

            food.GetComponent<SpawnedObj>().Spawner.removeObj(food);

            food.SetActive(false);
            food = null;
            
            // remove plate from player
            playerHoldingObj.GetComponent<BasicPickup>().Holder.GetComponent<BasicHolder>().CurrentlyHoldingObj = null;
            playerHoldingObj.GetComponent<BasicPickup>().Holder = null;

            playerHoldingObj.GetComponent<SpawnedObj>().Spawner.removeObj(playerHoldingObj);

            playerHoldingObj.SetActive(false);
        }
    }
    */
}
