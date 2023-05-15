using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    // SingleTon
    static public GameManager Instance;

    private void Awake()
    {
        makeSingleTon();
        presentGamePhase = gamePhaseList[0];
    }

    public QuestManager QM;
    private static GamePhase presentGamePhase;
    public List<GamePhase> gamePhaseList;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || OVRInput.GetDown(OVRInput.Button.One))
        {
            QM.ShowPanel();
            Debug.Log("OVR Input ¿€µø");
        }
    }
    public GamePhase getPresentGamePhase()
    {
        return presentGamePhase;
    }

    public void setGamePhase(string gamePhase) 
    {
        foreach(GamePhase gp in gamePhaseList)
        {
            if (gp.phaseName.Equals(gamePhase)) { presentGamePhase = gp;  }
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