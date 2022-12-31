using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private bool invincible = false;
    private float stunTime = 0;
    private bool isMoving = false;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float speed = 20f;
    private Rigidbody rb;
    private bool canMove = true;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isMoving && canMove)
        {
            player.SetAnimBool(StaticStrings.isWalking, true);
            transform.forward = moveDirection;
            rb.AddForce(transform.forward * speed);
        }
        else
        {
            player.SetAnimBool(StaticStrings.isWalking, false);
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void StartMove(Vector3 moveDirection)
    {
        this.moveDirection = moveDirection;
        isMoving = true;

    }

    public void StopMove()
    {
        isMoving = false;
    }

    public void StunMovement(float stunTime)
    {
        if(!invincible)
        {
            this.stunTime = stunTime;

            if (canMove)
                StartCoroutine(StunningMovement(stunTime));

            invincible = true;
        }
    }
    private IEnumerator StunningMovement(float stunTime)
    {
        canMove = false;
        gameObject.GetComponent<IHighlight>().HighlightMaterial();

        while(this.stunTime > 0)
        {
            this.stunTime -= stunTime;
            yield return new WaitForSeconds(stunTime);
        }
        gameObject.GetComponent<IHighlight>().RemoveHighlight();
        canMove = true;
        StartCoroutine(BlinkingHighlight());
    }

    private IEnumerator BlinkingHighlight()
    {
        for(int i = 0; i < 6; i++)
        {
            gameObject.GetComponent<IHighlight>().HighlightMaterial();
            yield return new WaitForSeconds(.2f);
            gameObject.GetComponent<IHighlight>().RemoveHighlight();
            yield return new WaitForSeconds(.2f);
        }

        invincible = false;

    }
}
