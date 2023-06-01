using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GamePhase
{
    public string phaseName;

    [TextArea(2, 2)]
    public string miniQuestText;
}