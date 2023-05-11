using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public QuestManager QM;
    private static GamePhase presentGamePhase;


    void Start()
    {
        presentGamePhase = GamePhase.Beginning;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) || OVRInput.GetDown(OVRInput.Button.One))
        {
            QM.ShowPanel();
        }
    }

    public static GamePhase getPresentGamePhase()
    {
        return presentGamePhase;
    }

    public static void setGamePhase(GamePhase gamePhase)
    {
        presentGamePhase = gamePhase;
    }

    public static void setGamePhase(string gamePhase) 
    {
        presentGamePhase = (GamePhase)Enum.Parse(typeof(GamePhase), gamePhase);
        Debug.Log(presentGamePhase);
    }

}
