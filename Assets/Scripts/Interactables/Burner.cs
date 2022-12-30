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
        ObjectManager.Instance.AddInteractable(gameObject);

        Spawner spawner = gameObject.GetComponent<Spawner>();
        if (spawner != null && spawnObjOnStart)
            spawner.SpawnObj(this);


    }

    public void StartHolding(IHold oldHolder, GameObject pickupObj, Transform followTransform)
    { 
        if(pickupObj.CompareTag(StaticStrings.Pan))
        {
            IHold panHolder = pickupObj.GetComponent(typeof(IHold)) as IHold;

            // start cooking if we start holding food
            if(panHolder.CurrentlyHoldingObj != null && panHolder.CurrentlyHoldingObj.CompareTag(StaticStrings.Food))
            {
                panHolder.CurrentlyHoldingObj.GetComponent<Cook>().StartCooking();
            }
        }

        holder.StartHolding(oldHolder, pickupObj, holdPositions[0]);
    }

    public void StopHolding(GameObject pickupObj)
    {
        holder.StopHolding(pickupObj);
    }
}
