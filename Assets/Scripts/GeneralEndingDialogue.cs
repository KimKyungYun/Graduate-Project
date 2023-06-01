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

            dialogueByObject.NPCName = "�������� ��";
            dialogueByObject.NPC_Height = 1.5f;


            Dialogue dialogue = new Dialogue();

            dialogue.choiceSubject = "";

            dialogue.DialogueTextList = new List<string>();
            
            dialogue.DialogueTextList.Add("ī���׽þ� �����͸� �����ؼ� �ùٸ��� ����߱�");
            dialogue.DialogueTextList.Add("�����ε� <color=#ff7777>���� ��¹��� ����Ҷ��� ī���׽þ� ������</color>�� ������ְ�");

            //----Filament feedback----//
            if (savedGameInfo.SelectedFilamentType == FilamentType.Engineer)
            {
                dialogue.DialogueTextList.Add("�ʶ��Ʈ�� �����Ͼ� �ʶ��Ʈ�� ����߱�");
                dialogue.DialogueTextList.Add("�ַ� ������ ����<color=#ff7777>���� ���� ��¹��� ����Ҷ� �����Ͼ� �ʶ��Ʈ</color>�� ����ϴ°� �³�");
                dialogue.DialogueTextList.Add("�ùٸ��� �� ������ �־���");

                dialogue.DialogueTextList.Add("ó���̶� �Ǽ��� ���� �� �ְ����� ����� ����� ���� ó��ġ�� ���� �� ���־���");
                dialogue.DialogueTextList.Add("��� ���ҳ�. ���߿� �ٸ� �䱸�� ������ �ڳװ� �þƼ� �����ص� ������ ���� �� ����");

                dialogue.changePhase = "TheEnd";

                dialogueByObject.dialogue = dialogue;

                DialogueAdded = true;
                return;
            }

            else if (savedGameInfo.SelectedFilamentType == FilamentType.ABS)
            {
                dialogue.DialogueTextList.Add("�׷��� �ڳ� �ʶ��Ʈ�� ABS �ʶ��Ʈ�� ����߾�?");
                dialogue.DialogueTextList.Add("<color=#ff7777>ABS�ʶ��Ʈ�� ū ������ ����ϸ� �ְų� �տ��� ����� �־ ���� ����ϸ� �ȵǳ�</color>");
                dialogue.DialogueTextList.Add("����.. �Ǹ��� ũ����.  <color=#ff7777>������ ���� ��¹��� ����Ҷ� �����Ͼ� �ʶ��Ʈ</color>�� ����ؾ� �ϴµ�..");
                dialogue.DialogueTextList.Add("�ϴ��� �����߳�.. ������ �̸� �����ðԳ�");
            }

            else if (savedGameInfo.SelectedFilamentType == FilamentType.PLA)
            {
                dialogue.DialogueTextList.Add("�׷��� �ڳ� �ʶ��Ʈ�� PLA �ʶ��Ʈ�� ����߾�?");
                dialogue.DialogueTextList.Add("<color=#ff7777>PLA�ʶ��Ʈ�� ABS���� �ܴ��Ѱ� ������ ���������� ����ϱ⿣ �������ֳ�</color>");
                dialogue.DialogueTextList.Add("����.. �Ǹ��� ũ����.  <color=#ff7777>������ ���� ��¹��� ����Ҷ� �����Ͼ� �ʶ��Ʈ</color>�� ����ؾ� �ϴµ�..");
                dialogue.DialogueTextList.Add("�ϴ��� �����߳�.. ������ �̸� �����ðԳ�");
            }

            else if (savedGameInfo.SelectedFilamentType == FilamentType.Flexible)
            {
                dialogue.DialogueTextList.Add("�׷��� �ڳ� �ʶ��Ʈ�� �÷��ú� �ʶ��Ʈ�� ����߾�?");
                dialogue.DialogueTextList.Add("�÷��ú� �ʶ��Ʈ�� ��������� ������ �Ÿ��� �յ�....");
                dialogue.DialogueTextList.Add("����.. �Ǹ��� ũ����.  <color=#ff7777>������ ���� ��¹��� ����Ҷ� �����Ͼ� �ʶ��Ʈ</color>�� ����ؾ� �ϴµ�..");
                dialogue.DialogueTextList.Add("�ϴ��� �����߳�.. ������ �̸� �����ðԳ�");
            }

            dialogue.changePhase = "BadEnding";
            dialogueByObject.dialogue = dialogue;
            DialogueAdded = true;
        }
    }
}
