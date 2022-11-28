using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour, IPickup, Iinteractable
{
    private GameObject holder = null;
    public IHighlight highlight { get => gameObject.GetComponent<IHighlight>(); }
    public GameObject Holder { get => holder; set => holder = value; }

    private void Start()
    {
        ObjectManager.Instance.addInteractable(gameObject);
    }


    public void interact(GameObject player, GameObject highlightedObj, GameObject playerHoldingObj)
    {
        if (playerHoldingObj == null)
        {
            ObjectManager.Instance.pickupEgg(gameObject);
            pickup(player);
        }
        // if we are holding the pan and something else is highlighted
        else if (highlightedObj != null && (highlightedObj.CompareTag("Plate") || highlightedObj.CompareTag("Pan") || highlightedObj.CompareTag("Table")))
        {
            setDown(player, highlightedObj, playerHoldingObj);
        }
        else
        {
            drop(player);
        }
    }
    private void pickup(GameObject player)
    {
        pickupMove(player);
        if (this.holder != null)
        {
            IHold oldHolder = this.holder.GetComponent(typeof(IHold)) as IHold;
            oldHolder.CurrentlyHoldingObj = null;
        }

        this.holder = player;
    }
    private void pickupMove(GameObject player)
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;

        IHold holder = player.GetComponent(typeof(IHold)) as IHold;
        holder.CurrentlyHoldingObj = gameObject;

        FollowPosition followScript = gameObject.GetComponent<FollowPosition>();
        followScript.FollowObj = holder.HoldPosition;

    }

    private void setDown(GameObject player, GameObject highlightedObj, GameObject playerHoldingObj)
    {
        if (highlightedObj != null)
        {
            moveObj(player, highlightedObj, playerHoldingObj);

            IHold playerHolder = player.GetComponent(typeof(IHold)) as IHold;


            // if we are setting down on a pan that is on a burner
            if (highlightedObj.CompareTag("Pan"))
            {
                IPickup pickup = highlightedObj.GetComponent(typeof(IPickup)) as IPickup;
                if (pickup.Holder != null && pickup.Holder.CompareTag("Burner"))
                {
                    gameObject.GetComponent<Cook>().enableCookBar();
                    gameObject.GetComponent<Cook>().cook();
                }
            }

            // if we are placing on a plate then dont remove pan from player
            if (highlightedObj.CompareTag("Plate") && this.holder != null && this.holder.CompareTag("Pan"))
            {
            }
            else
            {
                playerHolder.CurrentlyHoldingObj = null;
            }
            this.holder = highlightedObj;


        }
    }
    private void moveObj(GameObject player, GameObject highlightedObj, GameObject playerHoldingObj)
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;

        IHold holder = highlightedObj.GetComponent(typeof(IHold)) as IHold;
        holder.CurrentlyHoldingObj = gameObject;

        FollowPosition followScript = gameObject.GetComponent<FollowPosition>();
        followScript.FollowObj = holder.HoldPosition;

    }

    private void drop(GameObject player)
    {
        dropFromPlayer(player);
    }

    private void dropFromPlayer(GameObject player)
    {
        gameObject.GetComponent<FollowPosition>().FollowObj = null;

        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Collider>().enabled = true;

        IHold holder = player.GetComponent(typeof(IHold)) as IHold;
        holder.CurrentlyHoldingObj = null;

        this.holder = null;
    }

}
