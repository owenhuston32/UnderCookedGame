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

    private List<string> defaultTags = new List<string>()
    {
        "Pan", "Plate", "Food", "FoodCrate"
    };

    private List<string> foodTags = new List<string>()
    {
        "Pan", "Table", "Plate"
    };

    private List<string> plateTags = new List<string>()
    {
        "Table"
    };

    private List<string> emptyTags = new List<string>();

    private List<string> canInteractWithTags = new List<string>();

    public List<string> CanInteractWithTags { get => canInteractWithTags; }

    private void Start()
    {


        player1Holder = player1.GetComponent<Player>();
        player2Holder = player2.GetComponent<Player>();
        currentPlayerHolder = currentPlayer.GetComponent<Player>();
        setTags(defaultTags);
    }
    public void updateInteractableTags()
    {
        if (currentPlayerHolder.CurrentlyHoldingObj == null)
        {
            Debug.Log("default");
            setTags(defaultTags);
        }
        else if (currentPlayerHolder.CurrentlyHoldingObj.CompareTag("Pan"))
        {
            Debug.Log("Pan");
            setPanTags();
        }
        else if (currentPlayerHolder.CurrentlyHoldingObj.CompareTag("Food"))
        {
            Debug.Log("Food");
            setTags(foodTags);
        }
        else if (currentPlayerHolder.CurrentlyHoldingObj.CompareTag("Plate"))
        {
            Debug.Log("plate");
            setTags(plateTags);
        }
    }

    private void setTags(List<string> newTagList)
    {
        canInteractWithTags = newTagList;
    }

    // when we have a pan in hand these are the tags we can interact with
    public void setPanTags()
    {
        List<string> newList = new List<string>()
        {
            "Table", "Burner"
        };

        IHold panHolder = currentPlayerHolder.CurrentlyHoldingObj.GetComponent(typeof(IHold)) as IHold;

        GameObject panHoldingObj = panHolder.CurrentlyHoldingObj;

        // if the pan is holding something then we can place it on a plate
        if (panHoldingObj != null)
        {
            if (panHoldingObj.CompareTag("Food"))
            {
                newList.Add("Plate");
            }
        }
        canInteractWithTags = newList;
    }

    public void setNoTags()
    {
        canInteractWithTags = emptyTags;
    }

    public bool canInteract(GameObject obj)
    {

        IPickup pickup = obj.GetComponent(typeof(IPickup)) as IPickup;
        IHold holder = obj.GetComponent(typeof(IHold)) as IHold;


        // we cant interact with an object that any other player is holding
        if (player1Holder.CurrentlyHoldingObj != null && player1Holder.CurrentlyHoldingObj.Equals(obj)
            || player2Holder.CurrentlyHoldingObj != null && player2Holder.CurrentlyHoldingObj.Equals(obj))
        {
            return false;
        }

        if (pickup != null && !pickup.CanPickup)
            return false;


        // if the obj is a holder such as a pan and it already has an object on top and we are carrying an obj
        // then we cant interact with the holder

        // or if the object is a spawner and it is already carrying something then we can't interact with it
        if (holder != null && holder.CurrentlyHoldingObj != null)
        {
            if(currentPlayerHolder.CurrentlyHoldingObj != null)
                return false;

            if (holder.HolderObj.GetComponent<Spawner>() != null)
                return false;
        }

        return true;
    }
}
