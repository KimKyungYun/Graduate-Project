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

    private Dialogue dialogue; // 현재 대화
    private int index; // 다이얼로그 내 대화 인덱스
    private bool talking = false;
    private bool isChoiceMode = false;
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
        if (isChoiceMode) { return; }
        if (talking)
        {
            UpdateDialogueText();
            return;
        }

        //패널 띄우기 + 초기, 이름, 대화텍스트 및 카메라 설정
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
            //FindObjectOfType<DialogueTrigger>().onChoiceMode();// 선택모드 설정

            //선택지 창 출력
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
            //FindObjectOfType<DialogueTrigger>().offChoiceMode();// 선택모드 해제
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
        //선택지창 지우기
        HiddenChoicePanel();

        //Choice에 맞는 diagloue변수 재설정
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
        Debug.Log("NPC캠의 포지션 : " +NPCCam.transform.position);
        NPCCam.transform.LookAt(NPCObj.transform.position + NPC_Height_Vector);
    }
}
