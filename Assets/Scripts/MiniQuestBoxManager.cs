using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniQuestBoxManager : MonoBehaviour
{
    public TextMeshProUGUI QuestName;
    void Start()
    {
        GamePhase beginningPhase = GameManager.Instance.getPresentGamePhase();
        QuestName.text = beginningPhase.miniQuestText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateQuestText()
    {
        QuestName.text = GameManager.Instance.getPresentGamePhase().miniQuestText;
    }
}
