using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //DialogueBox관련 UI 변수
    public GameObject go_DialoguePanel;
    public TextMeshProUGUI NPCName;
    public TextMeshProUGUI DialogueText;
    public GameObject NPCCam;

    //ChoiceBox관련 UI변수
    public GameObject go_ChoiceSubjectPanel;
    public TextMeshProUGUI ChoiceSubjectText;
    public GameObject[] choiceBoxes;
    public TextMeshProUGUI[] ChoiceTexts;

    public MiniQuestBoxManager MQM;

    private Dialogue dlg; // 현재 대화
    private int index; // 다이얼로그 내 대화 인덱스
    private bool talking = false;
    private int choiceNum = 0; // 선택지의 갯수

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

        //패널 띄우기
        go_DialoguePanel.SetActive(true);
        talking = true;

        //초기, 이름, 대화텍스트 및 카메라 설정
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
            FindObjectOfType<DialogueTrigger>().onChoiceMode();// 선택모드 설정

            //선택지 창 출력
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
            FindObjectOfType<DialogueTrigger>().offChoiceMode();// 선택모드 해제
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
        //선택지창 지우기
        HiddenChoicePanel();

        //Choice에 맞는 Dlg변수 재설정
        dlg = dlg.ChoiceOptionList[SelectedNumber-1].dialogue;
        index = 0;
        DialogueText.text = dlg.DialogueTextList[index];
        FindObjectOfType<DialogueTrigger>().offChoiceMode();


    }
}
