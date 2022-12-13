using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private string activeMenu = StaticStrings.mainMenu;
    public string ActiveMenu { get=> activeMenu; set => activeMenu = value; }


    public void MainMenuEnabled()
    {
        ActiveMenu = StaticStrings.mainMenu;
    }

    public void EndScreenEnabled()
    {
        ActiveMenu = StaticStrings.endMenu;
    }

    public void InGameMenuEnabled()
    {
        ActiveMenu = StaticStrings.inGameMenu;
    }

}
