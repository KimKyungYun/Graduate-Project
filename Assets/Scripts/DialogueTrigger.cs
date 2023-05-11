using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * PlayerObject에 삽입해야되는 스크립트
 * Collider의 Trigger 이벤트를 통해 DialogueManager와 상호작용 하며 대화가 나타나도록 함.
 * 그밖에 선택지를 고르는 등 사용자의 조작에 따른 대화 이벤트를 처리
 */
public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager DM;
    DialogueByObject dialogueByObject; // NPC에 붙어있는 Dialogue스크립트
    GameObject go_NPCobj;
    private bool isCollideWithDialogueObj = false;
    private bool isChoiceMode = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollideWithDialogueObj && !isChoiceMode &&Input.GetKeyDown(KeyCode.B) )
        {
            DM.DisplayDialogue(dialogueByObject, go_NPCobj);
        }

        if (isChoiceMode && Input.GetKeyDown(KeyCode.Z))
        {
            DM.GetDialogueByChoice(1);
        }

        if (isChoiceMode && Input.GetKeyDown(KeyCode.X))
        {
            DM.GetDialogueByChoice(2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        dialogueByObject = getDialogue(other);

        if (dialogueByObject != null)
        {
            go_NPCobj = other.gameObject;
            isCollideWithDialogueObj = true;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        dialogueByObject = null;
        DM.HiddenDialogue();
        DM.HiddenChoicePanel();
        isChoiceMode = false;
        isCollideWithDialogueObj = false;
    }

    public void onChoiceMode()
    {
        isChoiceMode = true;
    }

    public void offChoiceMode()
    {
        isChoiceMode = false;
    }

    private DialogueByObject getDialogue(Collider other)
    {
        GamePhase presentGamePhase = GameManager.getPresentGamePhase();        
        DialogueByObject[] dialogueList = other.gameObject.GetComponentsInChildren<DialogueByObject>();
        
        foreach(DialogueByObject dialogue in dialogueList)
        {
            Debug.Log("가지고있는 대화스크립트별 Phase 출력 :" +  dialogue.name.ToString());
        }


        foreach (DialogueByObject dialogue in dialogueList)
        {
            //Debug.Log("대화 스크립트의 게임오브젝트 이름" + dialogue.name.ToString());
            

            if (presentGamePhase.ToString().Equals(dialogue.gameObject.name.ToString()))
            {
                return dialogue;
            }
        }
        return null;
    }
}
