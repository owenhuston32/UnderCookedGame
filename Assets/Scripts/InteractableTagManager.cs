using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractableTagManager : MonoBehaviour
{
    [SerializeField] GameObject currentPlayer;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    private BasicHolder currentPlayerHolder;
    private BasicHolder player1Holder;
    private BasicHolder player2Holder;

    private List<string> canInteractWithTags = new List<string>();

    public List<string> CanInteractWithTags { get => canInteractWithTags; }

    private void Start()
    {
        player1Holder = player1.GetComponent<BasicHolder>();
        player2Holder = player2.GetComponent<BasicHolder>();
        currentPlayerHolder = currentPlayer.GetComponent<BasicHolder>();
        setDefaultInterableTags();
    }
    public void updateInteractableTags()
    {
        if (currentPlayerHolder.CurrentlyHoldingObj == null)
        {
            Debug.Log("default");
            setDefaultInterableTags();
        }
        else if (currentPlayerHolder.CurrentlyHoldingObj.CompareTag("Pan"))
        {
            Debug.Log("Pan");
            setPanTags();
        }
        else if (currentPlayerHolder.CurrentlyHoldingObj.CompareTag("Food"))
        {
            Debug.Log("Food");
            setFoodTags();
        }
        else if (currentPlayerHolder.CurrentlyHoldingObj.CompareTag("Plate"))
        {
            Debug.Log("plate");
            setPlateTags();
        }
    }

    public void setDefaultInterableTags()
    {
        canInteractWithTags.Clear();
        canInteractWithTags.Add("Pan");
        canInteractWithTags.Add("Plate");
        canInteractWithTags.Add("Food");
    }
    // when we have a pan in hand these are the tags we can interact with
    public void setPanTags()
    {
        canInteractWithTags.Clear();
        canInteractWithTags.Add("Table");
        canInteractWithTags.Add("Burner");

        GameObject panHoldingObj = currentPlayerHolder.CurrentlyHoldingObj.GetComponent<BasicHolder>().CurrentlyHoldingObj;

        // if the pan is holding something then we can place it on a plate
        if (panHoldingObj != null)
        {
            if (panHoldingObj.CompareTag("Food"))
            {
                canInteractWithTags.Add("Plate");
            }
        }
    }
    public void setFoodTags()
    {
        canInteractWithTags.Clear();
        canInteractWithTags.Add("Pan");
        canInteractWithTags.Add("Table");
        canInteractWithTags.Add("Plate");
    }
    public void setPlateTags()
    {
        canInteractWithTags.Clear();
        canInteractWithTags.Add("Table");
        canInteractWithTags.Add("Submission Table");
    }

    public void setNoTags()
    {
        canInteractWithTags.Clear();
    }

    public bool canInteract(GameObject obj)
    {

        // we cant interact with an object that any player is holding
        if (player1Holder.CurrentlyHoldingObj != null && player1Holder.CurrentlyHoldingObj.Equals(obj)
            || player2Holder.CurrentlyHoldingObj != null && player2Holder.CurrentlyHoldingObj.Equals(obj))
        {
            return false;
        }


        // if the Food is on the pan/plate then we cant pick it up
        if (obj.CompareTag("Food"))
        {
            IPickup pickup = obj.GetComponent(typeof(IPickup)) as IPickup;
            if (pickup.Holder != null && (pickup.Holder.CompareTag("Pan") || pickup.Holder.CompareTag("Plate")))
                return false;
        }


        IHold holder = obj.GetComponent(typeof(IHold)) as IHold;

        // if the obj is a holder such as a pan and it already has an object on top and we are carrying an obj
        // then we cant interact with the holder
        if (holder != null  && currentPlayerHolder.CurrentlyHoldingObj != null)
        {
            if (holder.CurrentlyHoldingObj != null)
            {
                return false;
            }
        }
        return true;
    }
}
