using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burner : BasicInteractable, Iinteractable, IHold
{
    [SerializeField] private bool spawnObjOnStart = true;
    [SerializeField] private Transform[] holdPositions;
    [SerializeField] private bool isSubmissionTable;
    [SerializeField] private int submissionTableNum;
    private IHold holder;
    public GameObject HolderObj { get => gameObject; }

    public GameObject CurrentlyHoldingObj { get => holder.CurrentlyHoldingObj; set => holder.CurrentlyHoldingObj = value; }



    // Start is called before the first frame update
    public void Start()
    {
        holder = new BasicHolder(gameObject);
        ObjectManager.Instance.addInteractable(gameObject);

        Spawner spawner = gameObject.GetComponent<Spawner>();
        if (spawner != null && spawnObjOnStart)
            spawner.SpawnObj(this);


    }

    public void StartHolding(IHold oldHolder, IPickup pickup, Transform followTransform)
    { 
        if(pickup.PickupObj.CompareTag(StaticStrings.Pan))
        {
            IHold panHolder = pickup.PickupObj.GetComponent(typeof(IHold)) as IHold;

            if(panHolder.CurrentlyHoldingObj != null && panHolder.CurrentlyHoldingObj.CompareTag(StaticStrings.Food))
            {
                panHolder.CurrentlyHoldingObj.GetComponent<Cook>().cook();
            }
        }

        holder.StartHolding(oldHolder, pickup, holdPositions[0]);
    }

    public void StopHolding(IPickup pickup)
    {
        holder.StopHolding(pickup);
    }
}
