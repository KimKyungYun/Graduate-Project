using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue
{
    [TextArea (2,5)]
    public string[] DialogueTextList;
    public bool SpecialTag; // 해당 Dialogue가 게임 진행상황이나 특정 이벤트의 트리거로 작용하면 True체크
    public string changePhase;
    public bool forceSelection;
    public string choiceSubject;
    public ChoiceOption [] ChoiceOptionList;
}
