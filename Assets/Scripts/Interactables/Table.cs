using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : BasicInteractable, Iinteractable, IHold
{
    [SerializeField] private GameObject plateRespawnObj;
    [SerializeField] private bool spawnObjOnStart = true;
    [SerializeField] private Transform[] holdPositions;
    [SerializeField] private bool isSubmissionTable;
    [SerializeField] private int submissionTableNum;
    private IHold holder;
    public GameObject HolderObj { get => gameObject; }

    public GameObject CurrentlyHoldingObj { get => holder.CurrentlyHoldingObj; set => holder.CurrentlyHoldingObj = value; }

    public bool IsSubmissionTable { get => isSubmissionTable; }
    public int SubmissionTableNum { get => submissionTableNum; }


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
        holder.StartHolding(oldHolder, pickup, holdPositions[0]);

        if (IsSubmissionTable && pickup.PickupObj.CompareTag(StaticStrings.Plate))
        {
            IHold plateHolder = pickup.PickupObj.GetComponent(typeof(IHold)) as IHold;
            if (plateHolder != null && plateHolder.CurrentlyHoldingObj != null)
            {
                holder.StopHolding(pickup);

                ScoreManager.Instance.AddScore(SubmissionTableNum, plateHolder.CurrentlyHoldingObj);

                //remove food from scene
                ObjectManager.Instance.removeInteractable(plateHolder.CurrentlyHoldingObj);

                // remove plate from scene
                ObjectManager.Instance.removeInteractable(plateHolder.HolderObj);

                // respawn plate
                plateRespawnObj.GetComponent<Spawner>().WaitThenSpawn(plateRespawnObj.GetComponent(typeof(IHold)) as IHold);
            }
        }
    }

    public void StopHolding(IPickup pickup)
    {
        holder.StopHolding(pickup);
    }
}
