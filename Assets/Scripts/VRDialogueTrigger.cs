using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRDialogueTrigger : MonoBehaviour
{
    public DialogueManager DM;

    public void OnSelectedEntered(SelectEnterEventArgs args)
    {
        Debug.Log(args.interactableObject + "가 선택되었다");

        GameObject NPCObject = args.interactableObject.transform.gameObject;
        if(NPCObject.GetComponentsInChildren<DialogueByObject>().Length != 0)
        {
            DialogueByObject dialogueByObject = getDialogue(NPCObject);
            Debug.Log(dialogueByObject);

            //DM.DisplayDialogue(dialogueByObject, NPCObject);
        }
    }

    private DialogueByObject getDialogue(GameObject NPC)
    {
        GamePhase presentGamePhase = GameManager.Instance.getPresentGamePhase();
        DialogueByObject[] dialogueList = NPC.gameObject.GetComponentsInChildren<DialogueByObject>();

        foreach (DialogueByObject dialogue in dialogueList)
        {
            if (presentGamePhase.phaseName.Equals(dialogue.gameObject.name))
            {   
                return dialogue;
            }
        }
        return null;
    }
}
