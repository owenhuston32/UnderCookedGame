using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasicInteractable : MonoBehaviour, Iinteractable
{
    private void Start()
    {
        ObjectManager.Instance.addInteractable(gameObject);
    }

    public void interact(GameObject player, GameObject highlightedObj, GameObject playerHoldingObj)
    {
        // if we arent highlighting anything drop whatever we are holding
        if (highlightedObj == null)
        {
            if(playerHoldingObj != null)
            {
                playerHoldingObj.GetComponent<BasicPickup>().drop();
            }
        }

        // we arent holding anything we can pick stuff up
        else if(playerHoldingObj == null)
        {
            BasicPickup pickup = highlightedObj.GetComponent<BasicPickup>();
            if (pickup != null)
            {

                // stop cooking if we pickup pan
                if (highlightedObj.CompareTag("Pan"))
                {
                    if (highlightedObj.GetComponent<BasicHolder>().CurrentlyHoldingObj != null)
                    {
                        highlightedObj.GetComponent<BasicHolder>().CurrentlyHoldingObj.GetComponent<Cook>().stopCook();
                    }
                }

                pickup.pickup(player);
            }
        }

        // if we are holding something and are highlighting something
        else
        {

            BasicHolder holder = highlightedObj.GetComponent<BasicHolder>();
            BasicPickup pickup = highlightedObj.GetComponent<BasicPickup>();

            if(holder != null)
            {
                // set down food and drop pan
                if (highlightedObj.CompareTag("Plate"))
                {
                    interactWithPlate(playerHoldingObj, highlightedObj);
                }
                else if(highlightedObj.CompareTag("Pan"))
                {
                    interactWithPan(playerHoldingObj, highlightedObj);
                }
                else if(highlightedObj.CompareTag("Submission Table"))
                {
                    interactWithSubmissionTable(player, playerHoldingObj, highlightedObj);
                }
                else if(highlightedObj.CompareTag("Burner"))
                {
                    interactWithBurner(playerHoldingObj, highlightedObj);
                }
                else
                {
                    playerHoldingObj.GetComponent<BasicPickup>().setDown(playerHoldingObj, highlightedObj);
                }
            }
            else if(pickup != null)
            {
                pickup.pickup(playerHoldingObj);
            }
        }
    }
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
    private void interactWithPan(GameObject playerHoldingObj, GameObject highlightedObj)
    {
        if(playerHoldingObj.CompareTag("Food"))
        {
            // if the pan is on the burner start cooking
            if(highlightedObj.GetComponent<BasicPickup>().Holder != null
                && highlightedObj.GetComponent<BasicPickup>().Holder.CompareTag("Burner"))
            {
                playerHoldingObj.GetComponent<Cook>().cook();
            }
            playerHoldingObj.GetComponent<BasicPickup>().setDown(playerHoldingObj, highlightedObj);
        }
    }
    private void interactWithPlate(GameObject playerHoldingObj, GameObject highlightedObj)
    {
        // if we are caring a pan set the food down and drop pan
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
}
