using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEndingDiallogue : MonoBehaviour
{
    private DialogueByObject dialogueByObject;
    private bool DialogueAdded = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!DialogueAdded && GameManager.Instance.getPresentGamePhase().Equals("BeforeBadEnding"))
        {
            dialogueByObject = new DialogueByObject();

            dialogueByObject.NPCName = "연구소장 폴";
            dialogueByObject.NPC_Height = 1.5f;

            Dialogue dialogue = new Dialogue();


            gameObject.AddComponent<DialogueByObject>();
        }
    }
}
