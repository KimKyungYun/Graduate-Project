using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject go_QuestPanel;
    bool PanelOn = false;

    void Start()
    {
        go_QuestPanel.SetActive(false);
    }
    void Update()
    {
        
    }

    public void ShowPanel()
    {
        if(!PanelOn)
        {
            go_QuestPanel.SetActive(true);
            PanelOn = true;
        }
        else 
        { 
            go_QuestPanel.SetActive(false);
            PanelOn = false;
        }
    }
}
