using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //DialogueBox���� UI ����
    public GameObject go_DialoguePanel;
    public TextMeshProUGUI NPCName;
    public TextMeshProUGUI DialogueText;
    public GameObject NPCCam;

    //ChoiceBox���� UI����
    public GameObject go_ChoiceSubjectPanel;
    public TextMeshProUGUI ChoiceSubjectText;
    public GameObject[] choiceBoxes;
    public TextMeshProUGUI[] ChoiceTexts;

    public MiniQuestBoxManager MQM;

    private Dialogue dialogue; // ���� ��ȭ
    private int index; // ���̾�α� �� ��ȭ �ε���
    private bool talking = false;
    private bool isChoiceMode = false;
    private int choiceNum = 0; // �������� ����

    void Start()
    {
        //NPCName = GameObject.Find("NPCName").GetComponent<TextMeshProUGUI>();

        index = 0;
        go_DialoguePanel.SetActive(false);
        go_ChoiceSubjectPanel.SetActive(false);
        for (int i = 0; i < choiceBoxes.Length; i++)
        {
            choiceBoxes[i].SetActive(false);
        }
    }


    public void DisplayDialogue(DialogueByObject dialogueByObject, GameObject NPCObj)
    {
        if (isChoiceMode) { return; }
        if (talking)
        {
            UpdateDialogueText();
            return;
        }

        //�г� ���� + �ʱ�, �̸�, ��ȭ�ؽ�Ʈ �� ī�޶� ����
        go_DialoguePanel.SetActive(true);
        NPCName.text = dialogueByObject.NPCName;
        dialogue = dialogueByObject.dialogue;
        getNpcCam(NPCObj, dialogueByObject.NPC_Height);

        VRCanvasSetup.Instance.displayOnObject(NPCObj);

        talking = true;
        DialogueText.text = dialogue.DialogueTextList[0];
    }

    public void UpdateDialogueText()
    {
        index++;

        if (dialogue.changePhase!="")
        {
            GameManager.Instance.setGamePhase(dialogue.changePhase);
        }

        if (index == dialogue.DialogueTextList.Length-1 && dialogue.changePhase != "")
        {
            MQM.updateQuestText();
        }

        if (index == dialogue.DialogueTextList.Length-1 && dialogue.forceSelection)
        {
            //FindObjectOfType<DialogueTrigger>().onChoiceMode();// ���ø�� ����

            //������ â ���
            go_ChoiceSubjectPanel.SetActive(true);
            choiceNum = dialogue.ChoiceOptionList.Length;
            ChoiceSubjectText.text = dialogue.choiceSubject;
            for(int i = 0; i < choiceNum; i++)
            {
                choiceBoxes[i].SetActive(true);
                ChoiceTexts[i].text = dialogue.ChoiceOptionList[i].ChoiceText;
            }
            isChoiceMode= true;
        }



        if (index >= dialogue.DialogueTextList.Length)
        {
            HiddenDialogue();
            //FindObjectOfType<DialogueTrigger>().offChoiceMode();// ���ø�� ����
        }

        DialogueText.text = dialogue.DialogueTextList[index];
    }

    public void HiddenDialogue()
    {
        index = 0;
        go_DialoguePanel.SetActive(false);
        talking = false;
    }

    public void HiddenChoicePanel() 
    {
        go_ChoiceSubjectPanel.SetActive(false);
        for (int i = 0; i < choiceNum; i++)
        {
            choiceBoxes[i].SetActive(false);
        }
    }

    public void GetDialogueByChoice(int SelectedNumber)
    {
        //������â �����
        HiddenChoicePanel();

        //Choice�� �´� diagloue���� �缳��
        dialogue = dialogue.ChoiceOptionList[SelectedNumber].dialogue;
        index = 0;
        DialogueText.text = dialogue.DialogueTextList[index];
        //FindObjectOfType<DialogueTrigger>().offChoiceMode();
        isChoiceMode = false;
    }

    private void getNpcCam(GameObject NPCObj, float NPC_Height)
    {
        Debug.Log("NPCObject L " + NPCObj);
        Vector3 NPC_Height_Vector = new Vector3(0, NPC_Height, 0);
        NPCCam.transform.position = NPCObj.transform.position + NPCObj.transform.rotation * Vector3.forward * 0.75f + NPC_Height_Vector;
        Debug.Log("NPCķ�� ������ : " +NPCCam.transform.position);
        NPCCam.transform.LookAt(NPCObj.transform.position + NPC_Height_Vector);
    }
}
