using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DialogueByObject : MonoBehaviour
{
    [Header("SettingField")]
    public string NPCName = "";
    public float NPC_Height = 1.5f;

    [Header("Dialogue")]

    public Dialogue dialogue;

    void Awake()
    {
        if (this.transform.parent.parent.GetComponent<XRSimpleInteractable>() == null)
        {
            transform.parent.parent.AddComponent<XRSimpleInteractable>();
        }
    }
}
