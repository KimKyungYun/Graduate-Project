using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject questBox;

    void Start()
    {
        questBox.SetActive(false);
    }
    void Update()
    {
        VRCanvasHandler.Instance.displayFrontOfPlayer(questBox, 4f, 0, 0);

        if ( ControllerInputManager.Instance.RightSecondaryButtonPressed())
        {
            ShowPanel();
        }

        else
        {
            offPanel();
        }
    }

    public void ShowPanel()
    {
        questBox.SetActive(true);
    }

    public void offPanel()
    {
        questBox.SetActive(false);
    }
}
