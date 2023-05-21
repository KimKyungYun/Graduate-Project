using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    // SingleTon
    static public GameManager Instance;

    public UnityEvent GamePhaseChange;
    public QuestManager QM;
    private static GamePhase presentGamePhase;
    public List<GamePhase> gamePhaseList;

    private void Awake()
    {
        makeSingleTon();
        presentGamePhase = gamePhaseList[0];
        //setGamePhase(gamePhaseList[0].phaseName);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public GamePhase getPresentGamePhase()
    {
        return presentGamePhase;
    }

    public void setGamePhase(string gamePhase) 
    {
        foreach(GamePhase gp in gamePhaseList)
        {
            if (gp.phaseName.Equals(gamePhase))
            { 
                presentGamePhase = gp;
                GamePhaseChange.Invoke();
            }
        }
    }

    private void makeSingleTon()
    {
        if (Instance != null) Destroy(this.gameObject);
        else
        {
            //DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
    }
}