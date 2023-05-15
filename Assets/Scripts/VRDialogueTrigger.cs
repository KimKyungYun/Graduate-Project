using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRDialogueTrigger : MonoBehaviour
{
    public DialogueManager DM;
    
    public void OnSelectedEntered(SelectEnterEventArgs args)
    {
        GameObject NPCObject = args.interactableObject.transform.gameObject;
        GameObject camera = GameObject.FindWithTag("MainCamera");

        DialogueByObject dialogueByObject = getDialogue(NPCObject);

        if (dialogueByObject != null && Vector3.Distance(NPCObject.transform.position, camera.transform.position) < 8.5f)
        {
            DM.DisplayDialogue(dialogueByObject, NPCObject);
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
