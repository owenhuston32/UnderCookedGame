using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee 
{
    Player player;
    public Melee(Player player)
    {
        this.player = player;
    }

    public void startMelee(GameObject objInHand)
    {
        // if pan has something on it remove it
        IHold panHolder = objInHand.GetComponent(typeof(IHold)) as IHold;

        if (panHolder.CurrentlyHoldingObj != null)
        {
            panHolder.CurrentlyHoldingObj.SetActive(false);
            panHolder.CurrentlyHoldingObj = null;
        }

        objInHand.GetComponentInChildren<Animator>().SetTrigger(StaticStrings.panMelee);
        player.SetAnimTrigger(StaticStrings.meleeTrigger);
    }
}
