using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
 
    private Player player;
    private InteractableTagManager tagManager;
    private GameObject objInHand;
    private Melee melee;
    private Shoot shoot;

    private void Start()
    {
        player = GetComponent<Player>();
        tagManager = player.TagManager;

        shoot = new Shoot(player);
        melee = new Melee(player);
    }

    public void AttackPress()
    {
        IHold playerHolder = player.GetComponent(typeof(IHold)) as IHold;

        objInHand = playerHolder.CurrentlyHoldingObj;

        if (objInHand != null && objInHand.CompareTag(StaticStrings.Food))
        {
            shoot.StartShoot(objInHand);
            tagManager.SetDefaultTags();
        }
        else if(objInHand != null && objInHand.CompareTag(StaticStrings.Pan))
        {
            melee.StartMelee(objInHand);
        }
    }
}
