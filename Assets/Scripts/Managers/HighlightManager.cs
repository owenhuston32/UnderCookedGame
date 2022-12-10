using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightManager
{
    private InteractableTagManager tagManager;
    private GameObject highlightedObj = null;
    private GameObject prevHighlightedObj = null;
    private float reach;
    private Player player;

    public GameObject HighlightedObj { get => highlightedObj; }


    public HighlightManager(Player player, float reach)
    {
        this.player = player;
        this.reach = reach;
        tagManager = player.TagManager;
    }

    public void Update()
    {

        if (prevHighlightedObj != null)
            prevHighlightedObj.GetComponent<IHighlight>().RemoveHighlight();

        GameObject closestInteractable = getClosestInteractable();
        if (closestInteractable != null)
        {
            highlightedObj = closestInteractable;
            prevHighlightedObj = highlightedObj;
            closestInteractable.GetComponent<IHighlight>().HighlightMaterial();
        }

    }

    private GameObject getClosestInteractable()
    {
        float closestObjDistance = float.MaxValue;
        GameObject closestInteractable = null;

        foreach (GameObject interactable in ObjectManager.Instance.Interactables)
        {
            if (tagManager.canInteract(interactable))
            {

                if (tagManager.CanInteractWithTags.Contains(interactable.tag))
                {
                    float distance = Vector3.Distance(interactable.transform.position, player.transform.position);
                    if (distance <= reach)
                    {
                        if (distance < closestObjDistance)
                        {
                            closestObjDistance = distance;
                            closestInteractable = interactable;
                        }
                    }
                }
            }
        }
        return closestInteractable;
    }



}
