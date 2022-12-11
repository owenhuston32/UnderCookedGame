using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] EndScreen EndScreen;
    [SerializeField] Canvas PauseCanvas;


    public void ShowEndScreen()
    {
        EndScreen.ShowEndScreen();
    }

    public void SetPauseCanvas(bool val)
    {
        PauseCanvas.enabled = val;
    }


}
