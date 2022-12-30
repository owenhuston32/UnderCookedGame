using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InteractableTagManager
{

    private IHold playerHolder;

    private List<string> defaultTags = new List<string>()
    {
        StaticStrings.Pan, StaticStrings.Plate, StaticStrings.Food, StaticStrings.FoodCrate
    };

    private List<string> foodTags = new List<string>()
    {
        StaticStrings.Pan, StaticStrings.Plate, StaticStrings.Table
    };

    private List<string> plateTags = new List<string>()
    {
        StaticStrings.Table
    };

    private List<string> emptyTags = new List<string>();

    private List<string> canInteractWithTags = new List<string>();

    public List<string> CanInteractWithTags { get => canInteractWithTags; }

    public InteractableTagManager(IHold playerHolder)
    {
        this.playerHolder = playerHolder;
        setTags(defaultTags);
    }

    public void updateInteractableTags()
    {
        if (playerHolder.CurrentlyHoldingObj == null)
        {
            setTags(defaultTags);
        }
        else if (playerHolder.CurrentlyHoldingObj.CompareTag(StaticStrings.Pan))
        {
            setPanTags();
        }
        else if (playerHolder.CurrentlyHoldingObj.CompareTag(StaticStrings.Food))
        {
            setTags(foodTags);
        }
        else if (playerHolder.CurrentlyHoldingObj.CompareTag(StaticStrings.Plate))
        {
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
            StaticStrings.Table, StaticStrings.Burner
        };

        IHold panHolder = playerHolder.CurrentlyHoldingObj.GetComponent(typeof(IHold)) as IHold;

        GameObject panHoldingObj = panHolder.CurrentlyHoldingObj;

        // if the pan is holding something then we can place it on a plate
        if (panHoldingObj != null)
        {
            if (panHoldingObj.CompareTag(StaticStrings.Food))
            {
                newList.Add(StaticStrings.Plate);
            }
        }
        canInteractWithTags = newList;
    }

    public void setDefaultTags()
    {
        setTags(defaultTags);
    }

    public bool canInteract(GameObject obj)
    {

        IPickup pickup = obj.GetComponent(typeof(IPickup)) as IPickup;
        IHold holder = obj.GetComponent(typeof(IHold)) as IHold;


        if (pickup != null && !pickup.CanPickup)
            return false;


        // if the obj is a holder such as a pan and it already has an object on top and we are carrying an obj
        // then we cant interact with the holder

        if (holder != null && holder.CurrentlyHoldingObj != null && playerHolder.CurrentlyHoldingObj != null)
        {
            return false;
        }

        return true;
    }
}
