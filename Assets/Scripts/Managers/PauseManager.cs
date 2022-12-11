using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }
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

    public UnityEvent Pause;
    public UnityEvent UnPause;

    private bool isPaused = false;
    


    public void togglePause()
    {
        if(isPaused)
        {
            UnPause.Invoke();
        }
        else
        {
            Pause.Invoke();
        }


        isPaused = !isPaused;
    }


}
