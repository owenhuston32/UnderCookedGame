using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractableTagManager : MonoBehaviour
{
    [SerializeField] GameObject currentPlayer;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    private IHold currentPlayerHolder;
    private IHold player1Holder;
    private IHold player2Holder;

    private List<string> canInteractWithTags = new List<string>();

    public List<string> CanInteractWithTags { get => canInteractWithTags; }

    private void Start()
    {
        player1Holder = player1.GetComponent(typeof(IHold)) as IHold;
        player2Holder = player2.GetComponent(typeof(IHold)) as IHold;
        currentPlayerHolder = currentPlayer.GetComponent(typeof(IHold)) as IHold;
        setDefaultInterableTags();
    }
    public void updateInteractableTags()
    {
        if (currentPlayerHolder.CurrentlyHoldingObj == null)
        {
            setDefaultInterableTags();
        }
        else if (currentPlayerHolder.CurrentlyHoldingObj.CompareTag("Pan"))
        {
            setPanTags();
        }
        else if (currentPlayerHolder.CurrentlyHoldingObj.CompareTag("Egg"))
        {
            setEggTags();
        }
        else if (currentPlayerHolder.CurrentlyHoldingObj.CompareTag("Plate"))
        {
            setPlateTags();
        }
    }

    public void setDefaultInterableTags()
    {
        canInteractWithTags.Clear();
        canInteractWithTags.Add("Pan");
        canInteractWithTags.Add("Plate");
        canInteractWithTags.Add("Egg");
    }
    // when we have a pan in hand these are the tags we can interact with
    public void setPanTags()
    {
        canInteractWithTags.Clear();
        canInteractWithTags.Add("Table");
        canInteractWithTags.Add("Burner");

        GameObject panHolder = ((IHold)currentPlayerHolder.CurrentlyHoldingObj.GetComponent<IHold>()).CurrentlyHoldingObj;

        // if the pan is holding something then we can place it on a plate
        if (panHolder != null)
        {
            // if the food on the pan is fully cooked then we can place it on the plate
            if (panHolder.CompareTag("Egg"))
            {
                canInteractWithTags.Add("Plate");
            }
        }
    }
    public void setEggTags()
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


        // if the egg is on the pan/plate then we cant pick it up
        if (obj.CompareTag("Egg"))
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
