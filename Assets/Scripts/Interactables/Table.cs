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
        ObjectManager.Instance.AddInteractable(gameObject);

        Spawner spawner = gameObject.GetComponent<Spawner>();
        if (spawner != null && spawnObjOnStart)
            spawner.SpawnObj(this);
            

    }

    public void StartHolding(IHold oldHolder, GameObject pickupObj, Transform followTransform)
    {
        holder.StartHolding(oldHolder, pickupObj, holdPositions[0]);

        if (IsSubmissionTable && pickupObj.CompareTag(StaticStrings.Plate))
        {
            IHold plateHolder = pickupObj.GetComponent(typeof(IHold)) as IHold;
            if (plateHolder != null && plateHolder.CurrentlyHoldingObj != null)
            {
                holder.StopHolding(pickupObj);

                ScoreManager.Instance.AddScore(SubmissionTableNum, plateHolder.CurrentlyHoldingObj);

                //remove food from scene
                ObjectManager.Instance.RemoveInteractable(plateHolder.CurrentlyHoldingObj);

                // remove plate from scene
                ObjectManager.Instance.RemoveInteractable(plateHolder.HolderObj);

                // respawn plate
                plateRespawnObj.GetComponent<Spawner>().WaitThenSpawn(plateRespawnObj.GetComponent(typeof(IHold)) as IHold);
            
            }
        }
    }

    public void StopHolding(GameObject pickupObj)
    {
        holder.StopHolding(pickupObj);
    }
}
