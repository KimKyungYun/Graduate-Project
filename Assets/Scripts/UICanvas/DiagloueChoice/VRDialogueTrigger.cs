using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRDialogueTrigger : MonoBehaviour
{
    public DialogueManager DM;
    public AudioSource dialogueAudio;
    public void OnSelectedEntered(SelectEnterEventArgs args)
    {
        GameObject NPCObject = args.interactableObject.transform.gameObject;

        DialogueByObject dialogueByObject = getDialogue(NPCObject);

        if (dialogueByObject != null)
        {
            dialogueAudio.Play();
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