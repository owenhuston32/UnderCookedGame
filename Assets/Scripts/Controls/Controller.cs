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

    public void Initialize(InputAction moveAction, InputAction shootAction, InputAction interactAction, InputAction pauseAction)
    {

        this.moveAction = moveAction;
        this.shootAction = shootAction;
        this.interactAction = interactAction;
        this.pauseAction = pauseAction;

        this.moveAction.performed += Move;
        this.shootAction.performed += Shoot;
        this.interactAction.performed += Interact;
        this.pauseAction.performed += TogglePause;


        this.moveAction.canceled += StopMove;

    }

    private void Move(InputAction.CallbackContext callback)
    {
        Vector2 inputVector = callback.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        moveDirection.Normalize();

        gameObject.GetComponent<Move>().StartMove(moveDirection);

    }
    private void StopMove(InputAction.CallbackContext callback)
    {
        gameObject.GetComponent<Move>().StopMove();
    }

    private void Shoot(InputAction.CallbackContext callback)
    {
        gameObject.GetComponent<Attack>().AttackPress();
    }

    private void Interact(InputAction.CallbackContext callback)
    {
        gameObject.GetComponent<Player>().Interact();
    }

    private void TogglePause(InputAction.CallbackContext callback)
    {
        PauseManager.Instance.TogglePause();
    }
    

}
