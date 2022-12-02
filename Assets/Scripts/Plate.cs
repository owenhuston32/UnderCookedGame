using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Plate : MonoBehaviour, IPickup, IHold, Iinteractable
{
    Spawner plateSpawner;
    private GameObject holder = null;
    private GameObject currentHoldingObj = null;

    public GameObject CurrentlyHoldingObj { get => currentHoldingObj; set => currentHoldingObj = value; }
    public GameObject Holder { get => holder; set => holder = value; }
    [SerializeField] private Transform holdPosition;
    public Transform HoldPosition { get => holdPosition; set => holdPosition = value; }

    private void Start()
    {
        ObjectManager.Instance.addInteractable(gameObject);
        plateSpawner = GameObject.Find("Plate Spawner").GetComponent<Spawner>();

    }

    public void interact(GameObject player, GameObject highlightedObj, GameObject playerHoldingObj)
    {
        if (playerHoldingObj == null)
        {
            pickup(player);
        }
        // if we are holding the plate and something else is highlighted
        else if (playerHoldingObj.Equals(gameObject) && highlightedObj != null)
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

    public void setDown(GameObject player, GameObject highlightedObj, GameObject playerHoldingObj)
    {
        // if we are highlighting a plate place the food on the plate instead of the pan
        if (highlightedObj.CompareTag("Plate"))
        {
            Iinteractable interactable = currentHoldingObj.GetComponent(typeof(Iinteractable)) as Iinteractable;

            interactable.interact(player, highlightedObj, playerHoldingObj);
            currentHoldingObj = null;
        }
        else if(highlightedObj.CompareTag("Table"))
        {
            // if this is a submission table
            if(highlightedObj.GetComponent<Table>().IsSubmissionTable && currentHoldingObj != null )
            {
                ScoreManager.Instance.AddScore(player, this.currentHoldingObj);

                plateSpawner.removeObj(gameObject);
                currentHoldingObj.SetActive(false);
                this.holder = null;
                this.currentHoldingObj = null;

                player.GetComponent<Player>().CurrentlyHoldingObj = null;
                gameObject.SetActive(false);
            }
            else
            {
                moveObj(player, highlightedObj, playerHoldingObj);
                this.holder = highlightedObj;
            }
        }
    }
    private void moveObj(GameObject player, GameObject highlightedObj, GameObject playerHoldingObj)
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;

        // remove from player
        IHold holder = player.GetComponent(typeof(IHold)) as IHold;
        holder.CurrentlyHoldingObj = null;

        // add to new holder
        IHold newHolder = highlightedObj.GetComponent(typeof(IHold)) as IHold;
        newHolder.CurrentlyHoldingObj = gameObject;

        FollowPosition followScript = gameObject.GetComponent<FollowPosition>();
        followScript.FollowObj = newHolder.HoldPosition;

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
