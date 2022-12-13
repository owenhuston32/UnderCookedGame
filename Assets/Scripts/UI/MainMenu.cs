using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public UnityEvent StartGameEvent;


    public void PlayPress()
    {
        StartGameEvent.Invoke();
    }



}
