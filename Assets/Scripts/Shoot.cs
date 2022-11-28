using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    Player player;
    [SerializeField] float speed = 4f;
    [SerializeField] float despawnWaitTime = 1f;
    private GameObject objInHand;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void shootPress()
    {
        IHold playerHolder = player.GetComponent(typeof(IHold)) as IHold;

        objInHand = playerHolder.CurrentlyHoldingObj;

        if (objInHand != null && objInHand.CompareTag("Egg"))
        {
            shoot();
            playerHolder.CurrentlyHoldingObj = null;
        }
        else if(objInHand != null && objInHand.CompareTag("Pan"))
        {
            melee();
        }
    }
    private IEnumerator waitThenDespawn(float despawnWaitTime)
    {
        yield return new WaitForSeconds(despawnWaitTime);

        ObjectManager.Instance.removeInteractable(objInHand);
    }

    private void shoot()
    {
        FollowPosition followScript = objInHand.GetComponent<FollowPosition>();
        followScript.FollowObj = null;

        objInHand.GetComponent<Cook>().disableCookBar();
        objInHand.GetComponent<IHighlight>().RemoveHighlight();
        objInHand.GetComponent<Rigidbody>().useGravity = false;
        objInHand.GetComponent<Rigidbody>().AddForce(speed * objInHand.transform.forward, ForceMode.Impulse);
        objInHand.GetComponent<Collider>().enabled = true;

        objInHand.tag = "Projectile";

        player.updateInteractableTags();

        StartCoroutine(waitThenDespawn(despawnWaitTime));
    }
    private void melee()
    {
        // if pan has something on it remove it
        IHold panHolder = objInHand.GetComponent(typeof(IHold)) as IHold;

        if(panHolder.CurrentlyHoldingObj != null)
        {
            panHolder.CurrentlyHoldingObj.SetActive(false);
            panHolder.CurrentlyHoldingObj = null;
        }

        objInHand.GetComponentInChildren<Animator>().Play("melee");

        Debug.Log("Melee");
    }
}
