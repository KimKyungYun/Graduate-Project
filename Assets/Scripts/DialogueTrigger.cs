using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * PlayerObject�� �����ؾߵǴ� ��ũ��Ʈ
 * Collider�� Trigger �̺�Ʈ�� ���� DialogueManager�� ��ȣ�ۿ� �ϸ� ��ȭ�� ��Ÿ������ ��.
 * �׹ۿ� �������� ���� �� ������� ���ۿ� ���� ��ȭ �̺�Ʈ�� ó��
 */
public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager DM;
    DialogueByObject dialogueByObject; // NPC�� �پ��ִ� Dialogue��ũ��Ʈ
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
            Debug.Log("�������ִ� ��ȭ��ũ��Ʈ�� Phase ��� :" +  dialogue.name.ToString());
        }


        foreach (DialogueByObject dialogue in dialogueList)
        {
            //Debug.Log("��ȭ ��ũ��Ʈ�� ���ӿ�����Ʈ �̸�" + dialogue.name.ToString());
            

            if (presentGamePhase.ToString().Equals(dialogue.gameObject.name.ToString()))
            {
                return dialogue;
            }
        }
        return null;
    }
}
