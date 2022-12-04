using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField] float eggStunTime = 1f;
    [SerializeField] float panStunTime = 3f;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2; 
    [SerializeField] float reach = 2;

    private InteractableTagManager tagManager;
    private GameObject highlightedObj = null;
    private GameObject prevHighlightedObj = null;
    private BasicHolder holder;
    private void Start()
    {
        tagManager = GetComponent<InteractableTagManager>();
        tagManager.setDefaultInterableTags();
        holder = GetComponent<BasicHolder>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pan"))
        {
            // if we arent currently holding this obj
            // other.transform.parent.parent.gameObject is the obj that the player holds
            if(holder.CurrentlyHoldingObj == null || !holder.CurrentlyHoldingObj.Equals(other.transform.parent.parent.gameObject))
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

        if(holder.CurrentlyHoldingObj != null)
        {
            interactable = holder.CurrentlyHoldingObj.GetComponent<BasicInteractable>();
        }
        else
        {
            if(highlightedObj != null)
                interactable = highlightedObj.GetComponent<BasicInteractable>();
        }

        if (interactable != null)
        {
            interactable.interact(gameObject, highlightedObj, holder.CurrentlyHoldingObj);
        }
        highlightedObj = null;
        tagManager.updateInteractableTags();
    }
}
