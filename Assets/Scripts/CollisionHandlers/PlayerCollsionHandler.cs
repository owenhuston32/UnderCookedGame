using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollsionHandler
{

    public void onTriggerEnter(GameObject player, Collider collider)
    {
        if (collider.CompareTag(StaticStrings.Pan))
        {
            IHold playerHolder = player.GetComponent(typeof(IHold)) as IHold;

            GameObject panParent = collider.transform.parent.gameObject;
            // if we arent currently holding this obj or we arent carrying this pan
            // (other.transform.parent.parent.gameObject is the obj that the player holds)
            if (playerHolder.CurrentlyHoldingObj == null || !playerHolder.CurrentlyHoldingObj.Equals(panParent))
            {
                player.GetComponent<Move>().stunMovement(2);
            }
        }
    }
    public void onCollisionEnter(GameObject player, Collision collision)
    {
        if (collision.gameObject.CompareTag(StaticStrings.Projectile))
        {
            player.GetComponent<Move>().stunMovement(1);
        }
    }

}
