using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEndingDiallogue : MonoBehaviour
{
    private bool DialogueAdded = false;

    void Update()
    {
        if (!DialogueAdded && GameManager.Instance.getPresentGamePhaseName().Equals("BadEndingFeedback"))
        {
            SavedGameInfo savedGameInfo = SavedGameInfo.Instance;

            gameObject.AddComponent<DialogueByObject>();
            DialogueByObject dialogueByObject= gameObject.GetComponent<DialogueByObject>();

            

            dialogueByObject.NPCName = "�������� ��";
            dialogueByObject.NPC_Height = 1.5f;
            

            Dialogue dialogue = new Dialogue();

            dialogue.choiceSubject = "";
            
            dialogue.DialogueTextList = new List<string>();
            dialogue.DialogueTextList.Add("�ڳ� Ȥ�� ī���׽þ� ����� �����͸� �̿����� �ʾҳ�?");

            //----Printer feedback----//
            if (savedGameInfo.SelectedPrinterType == PrinterType.CartesianSmall)
            {
                dialogue.DialogueTextList.Add("�̷� ī���׽þ� �������߿� ���� �����͸� �̿��߱�");
                dialogue.DialogueTextList.Add("������ <color=#ff7777>������ ���� ���� ��¹�</color>�� ������ ������ �����͸� �̿��϶�� ���������ٵ�");
            }

            else if(savedGameInfo.SelectedPrinterType == PrinterType.Delta)
            {
                dialogue.DialogueTextList.Add("�ڳ� ��Ÿ ����� �����͸� �̿��߱�");
                dialogue.DialogueTextList.Add("�и��� ������ <color=#ff7777>���� ��¹��� ����Ҷ� ī���׽þ� ����� ������</color>�� ����϶�� �˷��־����ٵ�");
            }


            //----Filament feedback----//
            if(savedGameInfo.SelectedFilamentType == FilamentType.Engineer)
            {
                dialogue.DialogueTextList.Add("�׷��� �ʶ��Ʈ�� �����Ͼ� �ʶ��Ʈ�� ����߱�");
                dialogue.DialogueTextList.Add("���ݰ� <color=#ff7777>���� ���� ���� ��¹��� ����Ҷ� �����Ͼ� �ʶ��Ʈ</color>�� ������ְ�");
                dialogue.DialogueTextList.Add("������ �����͸� �߸� �����ϴٴ�.. �ڳ� �Ǹ��� ũ��");
            }

            else if (savedGameInfo.SelectedFilamentType != FilamentType.Engineer)
            {
                string filament = savedGameInfo.SelectedFilamentType.ToString();

                dialogue.DialogueTextList.Add("���� �ڳ� �ʶ��Ʈ�� " + filament + "�ʶ��Ʈ�� ����߾�?");
                dialogue.DialogueTextList.Add("����.. �Ǹ��� ũ����.  <color=#ff7777>������ ���� ��¹��� ����Ҷ� �����Ͼ� �ʶ��Ʈ</color>�� ����߾����..");
            }

            dialogue.DialogueTextList.Add("�ϴ��� �����߳�.. ������ �̸� �����ðԳ�");

            dialogue.changePhase = "BadEnding";
            dialogueByObject.dialogue = dialogue;

            DialogueAdded = true;
        }
    }
}
