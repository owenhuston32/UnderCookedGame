using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot 
{
    private float projectileSpeed = 0;
    private float despawnWaitTime = 0;
    public Shoot(float projectileSpeed, float despawnWaitTime)
    {
        this.projectileSpeed = projectileSpeed;
        this.despawnWaitTime = despawnWaitTime;
    }

    public void startShoot(GameObject objInHand)
    {
        FollowPosition followScript = objInHand.GetComponent<FollowPosition>();
        followScript.stopFollowing();

        objInHand.GetComponent<Cook>().disableCookBar();
        objInHand.GetComponent<IHighlight>().RemoveHighlight();
        objInHand.GetComponent<Rigidbody>().useGravity = false;
        objInHand.GetComponent<Rigidbody>().AddForce(projectileSpeed * objInHand.transform.forward, ForceMode.Impulse);
        objInHand.GetComponent<Collider>().enabled = true;

        objInHand.tag = StaticStrings.Projectile;

        ObjectManager.Instance.removeInteractableAfterSeconds(objInHand, despawnWaitTime);
    }

}
