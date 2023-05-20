using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue
{
    [TextArea (2,5)]
    public string[] DialogueTextList;
    public string changePhase;
    public string choiceSubject;
    public ChoiceOption [] ChoiceOptionList;
}
