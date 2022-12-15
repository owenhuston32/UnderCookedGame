using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public UnityEvent StartGameEvent;

    private void Start()
    {
        CanvasManager.Instance.ActiveMenu = StaticStrings.mainMenu;
    }

    public void PlayPress()
    {
        StartGameEvent.Invoke();
    }



}
