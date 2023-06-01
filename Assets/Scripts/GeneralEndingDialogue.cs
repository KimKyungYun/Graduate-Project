using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralEndingDialogue : MonoBehaviour
{
    private bool DialogueAdded = false;


    void Update()
    {
        if (!DialogueAdded && GameManager.Instance.getPresentGamePhaseName().Equals("GeneralEndingFeedback"))
        {
            SavedGameInfo savedGameInfo = SavedGameInfo.Instance;

            gameObject.AddComponent<DialogueByObject>();
            DialogueByObject dialogueByObject = gameObject.GetComponent<DialogueByObject>();

            dialogueByObject.NPCName = "연구소장 폴";
            dialogueByObject.NPC_Height = 1.5f;


            Dialogue dialogue = new Dialogue();

            dialogue.choiceSubject = "";

            dialogue.DialogueTextList = new List<string>();
            
            dialogue.DialogueTextList.Add("카르테시안 프린터를 선택해서 올바르게 출력했군");
            dialogue.DialogueTextList.Add("앞으로도 <color=#ff7777>대형 출력물을 출력할때는 카르테시안 프린터</color>로 출력해주게");

            //----Filament feedback----//
            if (savedGameInfo.SelectedFilamentType == FilamentType.Engineer)
            {
                dialogue.DialogueTextList.Add("필라멘트는 엔지니어 필라멘트를 사용했군");
                dialogue.DialogueTextList.Add("주로 엔진과 같이<color=#ff7777>강도 높은 출력물을 출력할땐 엔지니어 필라멘트</color>를 사용하는게 맞네");
                dialogue.DialogueTextList.Add("올바르게 잘 선택해 주었어");

                dialogue.DialogueTextList.Add("처음이라 실수가 있을 수 있겠지만 출력한 결과를 보니 처음치고 아주 잘 해주었네");
                dialogue.DialogueTextList.Add("고생 많았네. 나중에 다른 요구가 있을때 자네가 맡아서 진행해도 문제가 없을 것 같군");

                dialogue.changePhase = "TheEnd";

                dialogueByObject.dialogue = dialogue;

                DialogueAdded = true;
                return;
            }

            else if (savedGameInfo.SelectedFilamentType == FilamentType.ABS)
            {
                dialogue.DialogueTextList.Add("그런데 자네 필라멘트로 ABS 필라멘트를 사용했어?");
                dialogue.DialogueTextList.Add("<color=#ff7777>ABS필라멘트로 큰 물건을 출력하면 휘거나 균열이 생길수 있어서 절대 사용하면 안되네</color>");
                dialogue.DialogueTextList.Add("허허.. 실망이 크구만.  <color=#ff7777>강도가 높은 출력물을 출력할땐 엔지니어 필라멘트</color>를 사용해야 하는데..");
                dialogue.DialogueTextList.Add("일단은 수고했네.. 오늘은 이만 가보시게나");
            }

            else if (savedGameInfo.SelectedFilamentType == FilamentType.PLA)
            {
                dialogue.DialogueTextList.Add("그런데 자네 필라멘트로 PLA 필라멘트를 사용했어?");
                dialogue.DialogueTextList.Add("<color=#ff7777>PLA필라멘트도 ABS보다 단단한건 맞지만 공업용으로 사용하기엔 무리가있네</color>");
                dialogue.DialogueTextList.Add("허허.. 실망이 크구만.  <color=#ff7777>강도가 높은 출력물을 출력할땐 엔지니어 필라멘트</color>를 사용해야 하는데..");
                dialogue.DialogueTextList.Add("일단은 수고했네.. 오늘은 이만 가보시게나");
            }

            else if (savedGameInfo.SelectedFilamentType == FilamentType.Flexible)
            {
                dialogue.DialogueTextList.Add("그런데 자네 필라멘트로 플렉시블 필라멘트를 사용했어?");
                dialogue.DialogueTextList.Add("플렉시블 필라멘트를 공업용과는 완전히 거리가 먼데....");
                dialogue.DialogueTextList.Add("허허.. 실망이 크구만.  <color=#ff7777>강도가 높은 출력물을 출력할땐 엔지니어 필라멘트</color>를 사용해야 하는데..");
                dialogue.DialogueTextList.Add("일단은 수고했네.. 오늘은 이만 가보시게나");
            }

            dialogue.changePhase = "BadEnding";
            dialogueByObject.dialogue = dialogue;
            DialogueAdded = true;
        }
    }
}
