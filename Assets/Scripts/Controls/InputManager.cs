using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Controls controls;
    [SerializeField] private KeyboardController player1;
    [SerializeField] private KeyboardController player2;
    [SerializeField] private MakeyMakeyController player1MakeyController;
    [SerializeField] private MakeyMakeyController player2MakeyController;
    [SerializeField] private bool keyboardController = false;

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (keyboardController)
        {
            player1.initialize(controls.Movement.Player1Move, controls.Movement.Player1Shoot, controls.Movement.Player1Interact);
            player2.initialize(controls.Movement.Player2Move, controls.Movement.Player2Shoot, controls.Movement.Player2Interact);
        }
        else
        {
            player1MakeyController.initialize(controls.Movement.Player1Move, controls.Movement.Player1Shoot, controls.Movement.Player1Interact);
            player2MakeyController.initialize(controls.Movement.Player2Move, controls.Movement.Player2Shoot, controls.Movement.Player2Interact);
        }
    }

}
