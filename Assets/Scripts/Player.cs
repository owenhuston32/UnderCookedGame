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

    public GameObject HolderObj => gameObject;
    public InteractableTagManager TagManager { get => tagManager; }

    private void Start()
    {
        tagManager = new InteractableTagManager(this);
        highlightManager = new HighlightManager(this, reach);
        holder = new BasicHolder(gameObject);
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
        Iinteractable interactable = getInteractable();

        if (interactable != null)
        {
            InteractionManager.Instance.interact(gameObject, highlightManager.HighlightedObj, CurrentlyHoldingObj);
        }
        tagManager.updateInteractableTags();
    }

    private Iinteractable getInteractable()
    {
        if (CurrentlyHoldingObj != null)
        {
            return CurrentlyHoldingObj.GetComponent(typeof(Iinteractable)) as Iinteractable;
        }
        else if(highlightManager.HighlightedObj != null)
        {
            return highlightManager.HighlightedObj.GetComponent(typeof(Iinteractable)) as Iinteractable;
        }
        return null;
    }

    public void StartHolding(IHold oldHolder, IPickup pickup, Transform followTransform)
    {
        holder.StartHolding(oldHolder, pickup, holdPositions[0]);
    }

    public void StopHolding(IPickup pickup)
    {
        holder.StopHolding(pickup);
    }
}
