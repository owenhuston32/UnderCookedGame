using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class PlayerCollsionHandler
{

    public void onTriggerEnter(GameObject obj, Collider collider)
    {
        if (collider.CompareTag(StaticStrings.Pan))
        {
            IHold playerHolder = obj.GetComponent(typeof(IHold)) as IHold;

            GameObject panParent = collider.transform.parent.parent.gameObject;
            // if we arent currently holding this obj or we arent carrying this pan
            // (other.transform.parent.parent.gameObject is the obj that the player holds)
            if (playerHolder.CurrentlyHoldingObj == null || !playerHolder.CurrentlyHoldingObj.Equals(panParent))
            {
                obj.GetComponent<Move>().stunMovement(1);
            }
        }
    }
    public void onCollisionEnter(GameObject obj, Collision collision)
    {
        if (collision.gameObject.CompareTag(StaticStrings.Projectile))
        {
            obj.GetComponent<Move>().stunMovement(.5f);
        }
    }


}
