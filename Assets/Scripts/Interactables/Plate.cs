using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : BasicInteractable, IPickup, IHold, Iinteractable
{
    [SerializeField] Transform holdPosition;
    private IHold basicHolder;
    private IPickup basicPickup;
    public bool CanPickup { get => basicPickup.CanPickup; set => basicPickup.CanPickup = value; }
    public GameObject PickupObj { get => basicPickup.PickupObj; }
    public GameObject HolderObj { get => gameObject; }
    public void Initialize()
    {
        basicHolder = new BasicHolder(gameObject, holdPosition);
        basicPickup = new BasicPickup(gameObject);
        ObjectManager.Instance.addInteractable(gameObject);
    }

    public IHold PickupHolder { get => basicPickup.PickupHolder; set => basicPickup.PickupHolder = value; }
    public GameObject CurrentlyHoldingObj { get => basicHolder.CurrentlyHoldingObj; set => basicHolder.CurrentlyHoldingObj = value; }
    public Transform HoldPosition { get => basicHolder.HoldPosition; set => basicHolder.HoldPosition = value; }

    public void pickup(IHold newHolder)
    {
        basicPickup.pickup(newHolder);
    }

    public void setDown(GameObject obj, IHold newHolder, GameObject playerHoldingObj)
    {

        Debug.Log(obj + " : " + newHolder + " : " + playerHoldingObj);

        Table table = newHolder.HolderObj.GetComponent<Table>();
        if (table != null && table.IsSubmissionTable && CurrentlyHoldingObj != null)
        {
            ScoreManager.Instance.AddScore(table.SubmissionTableNum, CurrentlyHoldingObj);


            // remove plate from player
            PickupHolder.CurrentlyHoldingObj = null;

            //remove food from scene
            ObjectManager.Instance.removeInteractable(CurrentlyHoldingObj);

            // remove plate from scene
            ObjectManager.Instance.removeInteractable(gameObject);

            //respawn after a few seconds
            table.GetComponentInChildren<Spawner>().WaitThenSpawn();
        }
        else
        {
            basicPickup.setDown(obj, newHolder, playerHoldingObj);
        }
    }
    public void drop()
    {
        basicPickup.drop();
    }

}
