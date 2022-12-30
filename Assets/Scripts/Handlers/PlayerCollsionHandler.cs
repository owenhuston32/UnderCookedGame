using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollsionHandler
{
    private Dictionary<string, float> collisionTagToStunTime = new Dictionary<string, float>
    {
        [StaticStrings.Pan] = 2,
        [StaticStrings.Food] = 1
    };


    public void OnTriggerEnter(GameObject player, Collider collider)
    {
        if (collider.CompareTag(StaticStrings.Pan))
        {
            IHold playerHolder = player.GetComponent(typeof(IHold)) as IHold;

            GameObject panParent = collider.transform.parent.gameObject;
            // if we arent currently holding this obj or we arent carrying this pan
            // (other.transform.parent.parent.gameObject is the obj that the player holds)
            if (playerHolder.CurrentlyHoldingObj == null || !playerHolder.CurrentlyHoldingObj.Equals(panParent))
            {
                player.GetComponent<Move>().StunMovement(collisionTagToStunTime.GetValueOrDefault(StaticStrings.Pan));
            }
        }
    }
    public void OnCollisionEnter(GameObject player, Collision collision)
    {
        if (collision.gameObject.CompareTag(StaticStrings.Projectile))
        {
            player.GetComponent<Move>().StunMovement(collisionTagToStunTime.GetValueOrDefault(StaticStrings.Food));
        }
    }

}
