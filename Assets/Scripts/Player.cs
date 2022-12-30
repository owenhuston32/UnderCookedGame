using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour, IHold
{
    [SerializeField] private float reach = 2;
    [SerializeField] private Transform[] holdPositions;
    [SerializeField] private Animator anim;

    private PlayerAnimationManager animationManager;
    private PlayerCollsionHandler collisionHandler = new PlayerCollsionHandler();
    private InteractableTagManager tagManager;
    private IHold holder;
    private HighlightManager highlightManager;
    private InteractionHandler interactionHandler;

    private Dictionary<String, Transform> objTypeToHoldingPos = new Dictionary<String, Transform>();

    public HighlightManager HighlightManager { get => highlightManager; }

    public GameObject CurrentlyHoldingObj { get => holder.CurrentlyHoldingObj; set => holder.CurrentlyHoldingObj = value; }

    public GameObject HolderObj => gameObject;
    public InteractableTagManager TagManager { get => tagManager; }

    private void Start()
    {
        interactionHandler = new InteractionHandler(this);
        tagManager = new InteractableTagManager(this);
        highlightManager = new HighlightManager(this, reach);
        animationManager = new PlayerAnimationManager(anim);
        holder = new BasicHolder(gameObject);

        InitializeHoldPositionsDictionary();
    }
    private void InitializeHoldPositionsDictionary()
    {
        objTypeToHoldingPos.Add(StaticStrings.Plate, holdPositions[0]);
        objTypeToHoldingPos.Add(StaticStrings.Food, holdPositions[0]);
        objTypeToHoldingPos.Add(StaticStrings.Pan, holdPositions[1]);
    }
    private void OnTriggerEnter(Collider other)
    {
        collisionHandler.OnTriggerEnter(gameObject, other);
    }
    private void OnCollisionEnter(Collision collision)
    {
        collisionHandler.OnCollisionEnter(gameObject, collision);
    }

    private void Update()
    {
        highlightManager.Update();
    }

    public void Interact()
    {
        interactionHandler.Interact();
    }

    public void StartHolding(IHold oldHolder, GameObject pickupObj, Transform followTransform)
    {
        animationManager.SetHoldingObjAnim(pickupObj.tag, true);

        Transform holdPosition = objTypeToHoldingPos.GetValueOrDefault(pickupObj.tag);
        
        holder.StartHolding(oldHolder, pickupObj, holdPosition);
    }

    public void StopHolding(GameObject pickupObj)
    {
        animationManager.SetHoldingObjAnim(pickupObj.tag, false);
        holder.StopHolding(pickupObj);
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
