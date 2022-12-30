using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCrate : BasicInteractable, Iinteractable, IHold
{
    [SerializeField] private Animator anim;
    [SerializeField] private Transform[] holdPositions;
    private IHold holder;
    public GameObject HolderObj { get => gameObject; }

    public GameObject CurrentlyHoldingObj { get => holder.CurrentlyHoldingObj; set => holder.CurrentlyHoldingObj = value; }


    // Start is called before the first frame update
    public void Start()
    {
        holder = new BasicHolder(gameObject);
        ObjectManager.Instance.AddInteractable(gameObject);

    }

    public void SpawnObj(IHold holder)
    {
        anim.SetTrigger(StaticStrings.crateOpenTrigger);
        GetComponent<Spawner>().SpawnObj(holder);

    }

    public void StartHolding(IHold oldHolder, GameObject pickupObj, Transform followTransform)
    {

    }

    public void StopHolding(GameObject pickupObj)
    {

    }
}
