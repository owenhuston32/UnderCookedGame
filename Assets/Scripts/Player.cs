using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour, IHold
{
    [SerializeField] float eggStunTime = 1f;
    [SerializeField] float panStunTime = 3f;
    InteractableTagManager tagManager;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2; 
    [SerializeField] float reach = 2;
    private GameObject currentObjectInHand = null;
    private GameObject highlightedObj = null;
    private GameObject prevHighlightedObj = null;
    public GameObject CurrentlyHoldingObj { get => currentObjectInHand; set => currentObjectInHand = value; }
    [SerializeField] private Transform holdPosition;
    public Transform HoldPosition { get => holdPosition; set => holdPosition = value; }
    public GameObject CurrentObjectInHand { get => currentObjectInHand; set => currentObjectInHand = value; }
    private void Start()
    {
        tagManager = GetComponent<InteractableTagManager>();
        tagManager.setDefaultInterableTags();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pan"))
        {
            // if we arent currently holding this obj
            // other.transform.parent.parent.gameObject is the obj that the player holds
            if(currentObjectInHand == null || !currentObjectInHand.Equals(other.transform.parent.parent.gameObject))
            {
                gameObject.GetComponent<Move>().stunMovement(panStunTime);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            gameObject.GetComponent<Move>().stunMovement(eggStunTime);
        }
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

        if(currentObjectInHand != null)
        {
            interactable = currentObjectInHand.GetComponent(typeof(Iinteractable)) as Iinteractable;
        }
        else
        {
            if(highlightedObj != null)
                interactable = highlightedObj.GetComponent(typeof(Iinteractable)) as Iinteractable;
        }

        if (interactable != null)
        {
            interactable.interact(gameObject, highlightedObj, currentObjectInHand);
        }
        highlightedObj = null;
        tagManager.updateInteractableTags();
    }
}
