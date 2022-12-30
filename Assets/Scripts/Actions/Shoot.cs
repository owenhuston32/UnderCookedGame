using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot 
{
    private float projectileSpeed = 0;
    private float despawnWaitTime = 0;
    private Player player;
    public Shoot(Player player, float projectileSpeed, float despawnWaitTime)
    {
        this.player = player;
        this.projectileSpeed = projectileSpeed;
        this.despawnWaitTime = despawnWaitTime;
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


        ObjectManager.Instance.RemoveInteractableAfterSeconds(objInHand, despawnWaitTime);
    }

}
