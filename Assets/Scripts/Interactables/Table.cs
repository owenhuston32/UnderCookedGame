using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : BasicInteractable, Iinteractable, IHold
{
    [SerializeField] private bool spawnObjOnStart = true;
    [SerializeField] private Transform holdPosition;
    [SerializeField] private bool isSubmissionTable;
    [SerializeField] private int submissionTableNum;
    private IHold holder;
    public GameObject HolderObj { get => gameObject; }

    public GameObject CurrentlyHoldingObj { get => holder.CurrentlyHoldingObj; set => holder.CurrentlyHoldingObj = value; }

    public Transform HoldPosition { get => holdPosition; set => holdPosition = value; }

    public bool IsSubmissionTable { get => isSubmissionTable; }
    public int SubmissionTableNum { get => submissionTableNum; }


    // Start is called before the first frame update
    public void Start()
    {
        holder = new BasicHolder(gameObject, holdPosition);
        ObjectManager.Instance.addInteractable(gameObject);

        Spawner spawner = gameObject.GetComponent<Spawner>();
        if (spawner != null && spawnObjOnStart)
            spawner.SpawnObj(this);
            

    }


}
