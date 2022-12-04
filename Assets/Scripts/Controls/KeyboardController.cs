using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardController : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction interactAction;

    public void initialize(InputAction moveAction, InputAction shootAction, InputAction interactAction)
    {

        this.moveAction = moveAction;
        this.shootAction = shootAction;
        this.interactAction = interactAction;

        this.moveAction.performed += move;
        this.shootAction.performed += shoot;
        this.interactAction.performed += interact;


        this.moveAction.canceled += stopMove;

    }

    private void move(InputAction.CallbackContext callback)
    {
        Vector2 inputVector = callback.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        moveDirection.Normalize();

        gameObject.GetComponent<Move>().move(moveDirection);

    }
    private void stopMove(InputAction.CallbackContext callback)
    {
        gameObject.GetComponent<Move>().stopMove();
    }

    private void shoot(InputAction.CallbackContext callback)
    {
        gameObject.GetComponent<Attack>().attackPress();
    }

    private void interact(InputAction.CallbackContext callback)
    {
        gameObject.GetComponent<Player>().interact();
    }
    

}
