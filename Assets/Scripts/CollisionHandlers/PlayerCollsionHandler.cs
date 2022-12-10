using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class PlayerCollsionHandler
{

    public void onTriggerEnter(GameObject obj, Collider collider)
    {
        if (collider.CompareTag("Pan"))
        {
            IHold playerHolder = obj.GetComponent(typeof(IHold)) as IHold;

            // if we arent currently holding this obj
            // other.transform.parent.parent.gameObject is the obj that the player holds
            if (playerHolder.CurrentlyHoldingObj == null || !playerHolder.CurrentlyHoldingObj.Equals(collider.transform.parent.parent.gameObject))
            {
                obj.GetComponent<Move>().stunMovement(1);
            }
        }
    }
    public void onCollisionEnter(GameObject obj, Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            obj.GetComponent<Move>().stunMovement(.5f);
        }
    }


}
