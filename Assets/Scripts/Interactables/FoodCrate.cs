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

    public Transform[] HoldPositions { get => holdPositions; }


    // Start is called before the first frame update
    public void Start()
    {
        holder = new BasicHolder(gameObject, holdPositions);
        ObjectManager.Instance.addInteractable(gameObject);

    }

    public void SpawnObj(IHold holder)
    {
        anim.Play(StaticStrings.OpenFoodCrateAnim);

        GameObject food = GetComponent<Spawner>().SpawnObj(holder);

        IPickup pickup = food.GetComponent(typeof(IPickup)) as IPickup;

        pickup.pickup(holder);

    }

    public void StartHolding(IHold oldHolder, IPickup pickup)
    {

    }

    public void StopHolding(IPickup pickup)
    {

    }
}
