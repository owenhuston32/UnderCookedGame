using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float despawnWaitTime = 3f;
    private Player player;
    private InteractableTagManager tagManager;
    private GameObject objInHand;
    private Melee melee;
    private Shoot shoot;

    private void Start()
    {
        player = GetComponent<Player>();
        tagManager = player.TagManager;

        shoot = new Shoot(player, projectileSpeed, despawnWaitTime);
        melee = new Melee(player);
    }

    public void attackPress()
    {
        IHold playerHolder = player.GetComponent(typeof(IHold)) as IHold;

        objInHand = playerHolder.CurrentlyHoldingObj;

        if (objInHand != null && objInHand.CompareTag(StaticStrings.Food))
        {
            shoot.startShoot(objInHand);
            tagManager.setDefaultTags();
        }
        else if(objInHand != null && objInHand.CompareTag(StaticStrings.Pan))
        {
            melee.startMelee(objInHand);
        }
    }
}
