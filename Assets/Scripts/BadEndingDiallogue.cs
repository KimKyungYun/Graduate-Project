using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEndingDiallogue : MonoBehaviour
{
    private bool DialogueAdded = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!DialogueAdded && GameManager.Instance.getPresentGamePhaseName().Equals("BadEndingFeedback"))
        {
            SavedGameInfo savedGameInfo = SavedGameInfo.Instance;

            gameObject.AddComponent<DialogueByObject>();
            DialogueByObject dialogueByObject= gameObject.GetComponent<DialogueByObject>();

            

            dialogueByObject.NPCName = "연구소장 폴";
            dialogueByObject.NPC_Height = 1.5f;
            

            Dialogue dialogue = new Dialogue();

            dialogue.choiceSubject = "";
            
            dialogue.DialogueTextList = new List<string>();
            dialogue.DialogueTextList.Add("자네 혹시 카르테시안 방식의 프린터를 이용하지 않았나?");

            //----Printer feedback----//
            if (savedGameInfo.SelectedPrinterType == PrinterType.CartesianSmall)
            {
                dialogue.DialogueTextList.Add("이런 카르테시안 프린터중에 작은 프린터를 이용했군");
                dialogue.DialogueTextList.Add("릴리가 엔진과 같은 대형 출력물을 뽑을땐 오른쪽 프린터를 이용하라고 말해줬을텐데");
            }

            else if(savedGameInfo.SelectedPrinterType == PrinterType.Delta)
            {
                dialogue.DialogueTextList.Add("자네 델타 방식의 프린터를 이용했군");
                dialogue.DialogueTextList.Add("분명히 릴리가 대형 출력물을 출력할땐 카르테시안 방식의 프린터를 사용하라고 알려주었을텐데");
            }


            //----Filament feedback----//
            if(savedGameInfo.SelectedFilamentType == FilamentType.Engineer)
            {
                dialogue.DialogueTextList.Add("그래도 필라멘트는 엔지니어 필라멘트를 사용했군");
                dialogue.DialogueTextList.Add("하지만 프린터를 잘못 선택하다니.. 자네 실망이 크군");
            }

            else if (savedGameInfo.SelectedFilamentType != FilamentType.Engineer)
            {
                string filament = savedGameInfo.SelectedFilamentType.ToString();

                dialogue.DialogueTextList.Add("뭐야 자네 필라멘트도 " + filament + "필라멘트를 사용했어?");
                dialogue.DialogueTextList.Add("허허.. 실망이 크구만.  강도가 높은 출력물을 출력할땐 엔지니어 필라멘트를 사용했어야지..");
            }

            dialogue.DialogueTextList.Add("일단은 수고했네.. 오늘은 이만 가보시게나");

            dialogue.changePhase = "BadEnding";
            dialogueByObject.dialogue = dialogue;

            




            DialogueAdded = true;
        }
    }
}
