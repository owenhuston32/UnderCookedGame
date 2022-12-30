using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction interactAction;
    private InputAction pauseAction;

    public void initialize(InputAction moveAction, InputAction shootAction, InputAction interactAction, InputAction pauseAction)
    {

        this.moveAction = moveAction;
        this.shootAction = shootAction;
        this.interactAction = interactAction;
        this.pauseAction = pauseAction;

        this.moveAction.performed += move;
        this.shootAction.performed += shoot;
        this.interactAction.performed += interact;
        this.pauseAction.performed += togglePause;


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

    private void togglePause(InputAction.CallbackContext callback)
    {
        PauseManager.Instance.togglePause();
    }
    

}
