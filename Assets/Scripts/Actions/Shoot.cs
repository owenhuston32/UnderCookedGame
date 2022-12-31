using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot 
{
    private float projectileSpeed = 20f;
    private float despawnWaitTime = 3f;
    private Player player;
    public Shoot(Player player)
    {
        this.player = player;
    }

    public void StartShoot(GameObject objInHand)
    {
        objInHand.tag = StaticStrings.Projectile;

        objInHand.transform.parent = null;

        player.SetAnimBool(StaticStrings.isHoldingInFront, false);

        objInHand.GetComponent<Cook>().DisableCookBar();
        objInHand.GetComponent<IHighlight>().RemoveHighlight();
        objInHand.GetComponent<Rigidbody>().isKinematic = false;
        objInHand.GetComponent<Rigidbody>().useGravity = false;
        objInHand.GetComponent<Rigidbody>().AddForce(projectileSpeed * objInHand.transform.forward, ForceMode.Impulse);
        objInHand.GetComponent<Collider>().enabled = true;

        player.CurrentlyHoldingObj = null;

        ObjectManager.Instance.RemoveInteractableAfterSeconds(objInHand, despawnWaitTime);
    }

}
