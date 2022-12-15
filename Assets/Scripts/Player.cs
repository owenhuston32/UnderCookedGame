using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour, IHold
{
    [SerializeField] private GameObject cantHoldImageObj;
    [SerializeField] private float reach = 2;
    [SerializeField] private Transform[] holdPositions;
    [SerializeField] private Animator anim;

    private PlayerAnimationManager animationManager; 
    private PlayerCollsionHandler collisionHandler = new PlayerCollsionHandler();
    private InteractableTagManager tagManager;
    private IHold holder;
    private HighlightManager highlightManager;

    public GameObject CurrentlyHoldingObj { get => holder.CurrentlyHoldingObj; set => holder.CurrentlyHoldingObj = value; }

    public GameObject HolderObj => gameObject;
    public InteractableTagManager TagManager { get => tagManager; }

    private void Start()
    {
        tagManager = new InteractableTagManager(this, cantHoldImageObj);
        highlightManager = new HighlightManager(this, reach);
        animationManager = new PlayerAnimationManager(anim);
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
        bool canInteract = checkCanInteract();

        if (canInteract)
        {
            InteractionManager.Instance.interact(gameObject, highlightManager.HighlightedObj, CurrentlyHoldingObj);
        }
        tagManager.updateInteractableTags();
    }

    // get the object that we can interact with (either the obj we are holding or the obj we are highlighting)
    private bool checkCanInteract()
    {
        if (CurrentlyHoldingObj != null || highlightManager.HighlightedObj != null)
        {
            return true;
        }
        return false;
    }

    public void StartHolding(IHold oldHolder, IPickup pickup, Transform followTransform)
    {
        animationManager.SetHoldingObjAnim(pickup.PickupObj.tag, true);

        if (pickup.PickupObj.CompareTag(StaticStrings.Food) || pickup.PickupObj.CompareTag(StaticStrings.Plate))
            holder.StartHolding(oldHolder, pickup, holdPositions[0]);
        else
            holder.StartHolding(oldHolder, pickup, holdPositions[1]);
    }

    public void StopHolding(IPickup pickup)
    {
        animationManager.SetHoldingObjAnim(pickup.PickupObj.tag, false);
        holder.StopHolding(pickup);
    }



    public void SetAnimBool(string clipName, bool val)
    {
        animationManager.SetAnimBool(clipName, val);
    }
    public void SetAnimTrigger(string animTrigger)
    {
        animationManager.SetAnimTrigger(animTrigger);
    }
}
