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

    private Dialogue dlg; // ���� ��ȭ
    private int index; // ���̾�α� �� ��ȭ �ε���
    private bool talking = false;
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
        if (talking)
        {
            UpdateDialogueText();
            return;
        }

        //�г� ����
        go_DialoguePanel.SetActive(true);
        talking = true;

        //�ʱ�, �̸�, ��ȭ�ؽ�Ʈ �� ī�޶� ����
        Debug.Log(dialogueByObject.NPCName);

        NPCName.text = dialogueByObject.NPCName;
        dlg = dialogueByObject.dialogue;
        float NPC_Height = dialogueByObject.NPC_Height;

        Vector3 NPC_Height_Vector = new Vector3(0, NPC_Height, 0);
        NPCCam.transform.position = NPCObj.transform.position + NPCObj.transform.rotation * Vector3.forward * 0.75f + NPC_Height_Vector;
        NPCCam.transform.LookAt(NPCObj.transform.position + NPC_Height_Vector);
        DialogueText.text = dlg.DialogueTextList[0];
    }

    public void UpdateDialogueText()
    {
        index++;
        if (index == dlg.DialogueTextList.Length-1 && dlg.SpecialTag)
        {
            MQM.QuestIndexPlus();
        }

        if(!dlg.changePhase.Equals(""))
        {
            GameManager.setGamePhase(dlg.changePhase);
        }

        if (index == dlg.DialogueTextList.Length-1 && dlg.forceSelection)
        {
            FindObjectOfType<DialogueTrigger>().onChoiceMode();// ���ø�� ����

            //������ â ���
            go_ChoiceSubjectPanel.SetActive(true);
            choiceNum = dlg.ChoiceOptionList.Length;
            ChoiceSubjectText.text = dlg.choiceSubject;
            for(int i = 0; i < choiceNum; i++)
            {
                choiceBoxes[i].SetActive(true);
                ChoiceTexts[i].text = dlg.ChoiceOptionList[i].ChoiceText;
            }


            //for (int i = 0; i < dlg.ChoiceOptionList.Length; i++)
            //{
            //    Debug.Log(dlg.ChoiceOptionList[i].ChoiceText);
            //}
        }

        if (index >= dlg.DialogueTextList.Length)
        {
            HiddenDialogue();
            FindObjectOfType<DialogueTrigger>().offChoiceMode();// ���ø�� ����
        }

        DialogueText.text = dlg.DialogueTextList[index];
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

        //Choice�� �´� Dlg���� �缳��
        dlg = dlg.ChoiceOptionList[SelectedNumber-1].dialogue;
        index = 0;
        DialogueText.text = dlg.DialogueTextList[index];
        FindObjectOfType<DialogueTrigger>().offChoiceMode();


    }
}
