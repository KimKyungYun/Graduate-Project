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

        if (haveDialogueData(NPCObject) && Vector3.Distance(NPCObject.transform.position, camera.transform.position) < 8.5f)
        {
            DialogueByObject dialogueByObject = getDialogue(NPCObject);
            Debug.Log(dialogueByObject);

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

    public bool haveDialogueData(GameObject NPCObject)
    {
        return (NPCObject.GetComponentsInChildren<DialogueByObject>().Length != 0);
    }
}
