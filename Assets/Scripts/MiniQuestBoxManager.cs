using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniQuestBoxManager : MonoBehaviour
{
    static public MiniQuestBoxManager Instance;

    public TextMeshProUGUI QuestName;
    public GameObject miniQuestBox;
    void Awake()
    {
        makeSingleTon();
        miniQuestBox.SetActive(false);
    }

    void Start()
    {
        GamePhase beginningPhase = GameManager.Instance.getPresentGamePhase();
        QuestName.text = beginningPhase.miniQuestText;
    }

    public void displayQuestBox()
    {
        miniQuestBox.SetActive(true);
        //여기에 이펙트효과를 넣기
    }
    public void updateQuestText()
    {
        miniQuestBox.SetActive(true);

        GamePhase presentPhase = GameManager.Instance.getPresentGamePhase();
        if (presentPhase.miniQuestText != "")
        {
            QuestName.text = GameManager.Instance.getPresentGamePhase().miniQuestText;
        }
    }

    private void makeSingleTon()
    {
        if (Instance != null) Destroy(this.gameObject);
        else
        {
            Instance = this;
        }
    }
}
