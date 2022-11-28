using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.WSA;

public class Player : MonoBehaviour, IHold
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] float reach = 2;
    private GameObject currentObjectInHand = null;
    private List<string> canInteractWithTags = new List<string>();
    private GameObject highlightedObj = null;
    private GameObject prevHighlightedObj = null;
    public GameObject CurrentlyHoldingObj { get => currentObjectInHand; set => currentObjectInHand = value; }
    [SerializeField] private Transform holdPosition;
    public Transform HoldPosition { get => holdPosition; set => holdPosition = value; }
    public GameObject CurrentObjectInHand { get => currentObjectInHand; set => currentObjectInHand = value; }
    private void Start()
    {
        setDefaultInterableTags();
    }
    public void updateInteractableTags()
    {
        if(currentObjectInHand == null)
        {
            setDefaultInterableTags();
        }
        else if(currentObjectInHand.CompareTag("Pan"))
        {
            setPanTags();
        }
        else if(currentObjectInHand.CompareTag("Egg"))
        {
            setEggTags();
        }
        else if(currentObjectInHand.CompareTag("Plate"))
        {
            setPlateTags();
        }
    }

    private void setDefaultInterableTags()
    {
        canInteractWithTags.Clear();
        canInteractWithTags.Add("Pan");
        canInteractWithTags.Add("Plate");
        canInteractWithTags.Add("Egg");
    }
    // when we have a pan in hand these are the tags we can interact with
    private void setPanTags()
    {
        canInteractWithTags.Clear();
        canInteractWithTags.Add("Table");
        canInteractWithTags.Add("Burner");

        GameObject objectHolding = ((IHold)currentObjectInHand.GetComponent<IHold>()).CurrentlyHoldingObj;

        // if the pan is holding something then we can place it on a plate
        if (objectHolding != null)
        {
            // if the food on the pan is fully cooked then we can place it on the plate
            if(objectHolding.CompareTag("Egg"))
            {
                canInteractWithTags.Add("Plate");
            }
        }
    }
    private void setEggTags()
    {
        canInteractWithTags.Clear();
        canInteractWithTags.Add("Pan");
        canInteractWithTags.Add("Table");
        canInteractWithTags.Add("Plate");
    }
    private void setPlateTags()
    {
        canInteractWithTags.Clear();
        canInteractWithTags.Add("Table");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            gameObject.SetActive(false);
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
            if (canInteract(interactable))
            {

                if (canInteractWithTags.Contains(interactable.tag))
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

    private bool canInteract(GameObject obj)
    {


        IHold player1Holder = player1.GetComponent(typeof(IHold)) as IHold;
        IHold player2Holder = player2.GetComponent(typeof(IHold)) as IHold;

        if (player1Holder.CurrentlyHoldingObj != null && player1Holder.CurrentlyHoldingObj.Equals(obj)
            || player2Holder.CurrentlyHoldingObj != null && player2Holder.CurrentlyHoldingObj.Equals(obj))
        {
            return false;
        }


        // if the egg is on the pan then we cant pick it up
        if (obj.CompareTag("Egg"))
        {
            IPickup pickup = obj.GetComponent(typeof(IPickup)) as IPickup;
            if (pickup.Holder != null && (pickup.Holder.CompareTag("Pan") || pickup.Holder.CompareTag("Plate")))
                return false;
        }  


        // if the table/burner has something on it and we are already holding something
        // then we cant interact with table
        if((obj.CompareTag("Table") || obj.CompareTag("Burner") || obj.CompareTag("Pan") || obj.CompareTag("Plate")) && currentObjectInHand != null)
        {
            IHold holder = obj.GetComponent(typeof(IHold)) as IHold;
            if (holder.CurrentlyHoldingObj != null)
            {
                return false;
            }
        }
        return true;
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
        updateInteractableTags();
    }
}
