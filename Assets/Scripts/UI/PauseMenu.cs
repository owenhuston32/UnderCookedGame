using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{

    public void SetPauseMenu(bool val)
    {
        gameObject.GetComponent<Canvas>().enabled = val;
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
