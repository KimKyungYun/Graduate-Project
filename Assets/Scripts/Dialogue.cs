using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue
{
    [TextArea (2,5)]
    public string[] DialogueTextList;
    public bool SpecialTag; // �ش� Dialogue�� ���� �����Ȳ�̳� Ư�� �̺�Ʈ�� Ʈ���ŷ� �ۿ��ϸ� Trueüũ
    public string changePhase;
    public bool forceSelection;
    public string choiceSubject;
    public ChoiceOption [] ChoiceOptionList;
}
