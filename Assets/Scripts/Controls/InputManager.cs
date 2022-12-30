using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Controls controls;
    [SerializeField] private Controller player1;
    [SerializeField] private Controller player2;

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

    public void EnableInGameControls()
    {
        controls.InGameControls.Enable();
    }

    public void DisableInGameControls()
    {
        controls.InGameControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        player1.initialize(controls.InGameControls.Player1Move, controls.InGameControls.Player1Shoot, controls.InGameControls.Player1Interact, controls.PersistentActions.Player1Pause);
        player2.initialize(controls.InGameControls.Player2Move, controls.InGameControls.Player2Shoot, controls.InGameControls.Player2Interact, controls.PersistentActions.Player2Pause);
        

        // disable in game controls on start
       DisableInGameControls();
    }

}
