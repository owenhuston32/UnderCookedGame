using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float speed = 20f;
    private Rigidbody rb;
    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isMoving && canMove)
        {
            transform.forward = moveDirection;
            rb.AddForce(transform.forward * speed);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void move(Vector3 moveDirection)
    {
        this.moveDirection = moveDirection;
        isMoving = true;

    }

    public void stopMove()
    {
        isMoving = false;
    }

    public void stunMovement(float stunTime)
    {
        StartCoroutine(stunningMovement(stunTime));
    }
    private IEnumerator stunningMovement(float stunTime)
    {
        canMove = false;
        gameObject.GetComponent<IHighlight>().HighlightMaterial();
        yield return new WaitForSeconds(stunTime);
        gameObject.GetComponent<IHighlight>().RemoveHighlight();
        canMove = true;
    }

}
