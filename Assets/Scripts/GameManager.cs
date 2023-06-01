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
        //presentGamePhase = gamePhaseList[0];
        setGamePhase(gamePhaseList[0].phaseName);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            setGamePhase("BadEndingFeedback");
        }

        if(Input.GetKeyDown(KeyCode.G)) 
        {
            setGamePhase("BeforeMerge");
        }

        if(presentGamePhase.phaseName.Equals("EndingBridge"))
        {
            if ( SavedGameInfo.Instance.SelectedPrinterType != PrinterType.CartesianBig )
            {
                setGamePhase("BeforeBadEnding");
            }

            else
            {

                setGamePhase("MergeEngine");
            }
        }
    }

    public GamePhase getPresentGamePhase()
    {
        return presentGamePhase;
    }

    public string getPresentGamePhaseName()
    {
        return presentGamePhase.phaseName;
    }
    
    public void setGamePhase(string gamePhase) 
    {
        foreach(GamePhase gp in gamePhaseList)
        {
            if (gp.phaseName.Equals(gamePhase))
            { 
                presentGamePhase = gp;
                if (gp.miniQuestText != "") GamePhaseChange.Invoke();
                return;
            }
        }

        throw new ArgumentException("해당 gamePhase의 이름이 존재하지 않습니다.");
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