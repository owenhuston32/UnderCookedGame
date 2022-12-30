using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler
{
    private Player player;
    public InteractionHandler(Player player)
    {
        this.player = player;
    }

    public void interact()
    {
        bool canInteract = CanInteract();

        if (canInteract)
        {
            InteractionManager.Instance.interact(player.gameObject, player.HighlightManager.HighlightedObj, player.CurrentlyHoldingObj);
        }
        player.TagManager.updateInteractableTags();
    }

    // get the object that we can interact with (either the obj we are holding or the obj we are highlighting)
    private bool CanInteract()
    {
        if (player.CurrentlyHoldingObj != null || player.HighlightManager.HighlightedObj != null)
        {
            return true;
        }
        return false;
    }



}
