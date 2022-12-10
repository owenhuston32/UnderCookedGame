using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour, IHold
{ 
    [SerializeField] float reach = 2;
    [SerializeField] Transform[] holdPositions;


    private PlayerCollsionHandler collisionHandler = new PlayerCollsionHandler();
    private InteractableTagManager tagManager;
    private IHold holder;
    private HighlightManager highlightManager;

    public GameObject CurrentlyHoldingObj { get => holder.CurrentlyHoldingObj; set => holder.CurrentlyHoldingObj = value; }
    public Transform[] HoldPositions { get => holder.HoldPositions; }

    public GameObject HolderObj => gameObject;
    public InteractableTagManager TagManager { get => tagManager; }

    private void Start()
    {
        tagManager = new InteractableTagManager(this);
        highlightManager = new HighlightManager(this, reach);
        holder = new BasicHolder(gameObject, holdPositions);
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
        highlightManager.Update();
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
            if(highlightManager.HighlightedObj != null)
                interactable = highlightManager.HighlightedObj.GetComponent(typeof(Iinteractable)) as Iinteractable;

        }

        if (interactable != null)
        {
            InteractionManager.Instance.interact(gameObject, highlightManager.HighlightedObj, CurrentlyHoldingObj);
        }
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
