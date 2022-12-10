using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour, IHold
{
    [SerializeField] float eggStunTime = 1f;
    [SerializeField] float panStunTime = 3f;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2; 
    [SerializeField] float reach = 2;
    [SerializeField] Transform[] holdPositions;


    private PlayerCollsionHandler collisionHandler = new PlayerCollsionHandler();
    private InteractableTagManager tagManager;
    private GameObject highlightedObj = null;
    private GameObject prevHighlightedObj = null;
    private IHold holder;

    public GameObject CurrentlyHoldingObj { get => holder.CurrentlyHoldingObj; set => holder.CurrentlyHoldingObj = value; }
    public Transform[] HoldPositions { get => holder.HoldPositions; }

    public GameObject HolderObj => gameObject;

    private void Start()
    {
        holder = new BasicHolder(gameObject, holdPositions);
        tagManager = GetComponent<InteractableTagManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        collisionHandler.onTriggerEnter(gameObject, other);
    }
    private void OnCollisionEnter(Collision collision)
    {
        collisionHandler.onCollisionEnter(gameObject, collision);
    }

    private void Update()
    {
        highlightedObj = null;

        if (prevHighlightedObj != null)
            prevHighlightedObj.GetComponent<IHighlight>().RemoveHighlight();

        GameObject closestInteractable = getClosestInteractable();
        if (closestInteractable != null)
        {

            highlightedObj = closestInteractable;
            prevHighlightedObj = highlightedObj;
            closestInteractable.GetComponent<IHighlight>().HighlightMaterial();
        }

    }
    private GameObject getClosestInteractable()
    {
        float closestObjDistance = float.MaxValue;
        GameObject closestInteractable = null;

        foreach (GameObject interactable in ObjectManager.Instance.Interactables)
        {
            if (tagManager.canInteract(interactable))
            {

                if (tagManager.CanInteractWithTags.Contains(interactable.tag))
                {
                    float distance = Vector3.Distance(interactable.transform.position, gameObject.transform.position);
                    if (distance <= reach)
                    {
                        if (distance < closestObjDistance)
                        {
                            closestObjDistance = distance;
                            closestInteractable = interactable;
                        }
                    }
                }
            }
        }
        return closestInteractable;
    }

 
    public void interact()
    {
        Iinteractable interactable = null;

        if(CurrentlyHoldingObj != null)
        {
            interactable = CurrentlyHoldingObj.GetComponent(typeof(Iinteractable)) as Iinteractable;
        }
        else
        {
            if(highlightedObj != null)
                interactable = highlightedObj.GetComponent(typeof(Iinteractable)) as Iinteractable;

        }

        if (interactable != null)
        {
            InteractionManager.Instance.interact(gameObject, highlightedObj, CurrentlyHoldingObj);
        }
        highlightedObj = null;
        tagManager.updateInteractableTags();
    }

    public void StartHolding(IHold oldHolder, IPickup pickup)
    {
        holder.StartHolding(oldHolder, pickup);
    }

    public void StopHolding(IPickup pickup)
    {
        holder.StopHolding(pickup);
    }
}
