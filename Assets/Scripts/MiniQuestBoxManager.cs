using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniQuestBoxManager : MonoBehaviour
{
    public Quest[] Quests;    
    public TextMeshProUGUI QuestName;

    private int QuestIndex;
    void Start()
    {
        QuestIndex = 0;
        QuestName.text = Quests[QuestIndex].QuestName;
    }

    // Update is called once per frame
    void Update()
    {
        QuestName.text = Quests[QuestIndex].QuestName;
    }

    public void QuestIndexPlus()
    {
        QuestIndex++;
        QuestName.text = Quests[QuestIndex].QuestName;
    }
}
