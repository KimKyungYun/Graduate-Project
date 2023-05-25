using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniQuestBoxManager : MonoBehaviour
{
    static public MiniQuestBoxManager Instance;

    public TextMeshProUGUI QuestName;
    public GameObject miniQuestBox;
    public AudioSource alarmAudio;

    private CanvasGroup canvasGroup;
    private bool visible = false;
    private bool updateEvent = false;
    void Awake()
    {
        makeSingleTon();
        canvasGroup= miniQuestBox.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.0f;
    }

    void Start()
    {
        GamePhase beginningPhase = GameManager.Instance.getPresentGamePhase();
        QuestName.text = beginningPhase.miniQuestText;
    }
    
    void Update()
    {
        VRCanvasHandler.Instance.displayFrontOfPlayer(miniQuestBox, 3f);

        if (ControllerInputManager.Instance.PrimaryButtonPressed() && !visible)
        {
            displayQuestBox();
        }

        else if (!ControllerInputManager.Instance.PrimaryButtonPressed() && !updateEvent)
        {
            if (visible || canvasGroup.alpha <= 1.0f)
                hiddenQuestBox();
        }

        if (updateEvent)
        {
            alarmAudio.enabled = true;
            displayQuestBox();
            if (canvasGroup.alpha == 1)
            {
                updateEvent = false;
                hiddenQuestBox();
                alarmAudio.enabled = false;
            }
        }
    }


    public void displayQuestBox()
    {
        fadein();
    }
    public void updateQuestText()
    {
        GamePhase presentPhase = GameManager.Instance.getPresentGamePhase();
        if (presentPhase.miniQuestText != "")
        {
            QuestName.text = GameManager.Instance.getPresentGamePhase().miniQuestText;
            updateEvent = true;
        }
    }

    public void hiddenQuestBox()
    {
        fadeout();
    }

    private void makeSingleTon()
    {
        if (Instance != null) Destroy(this.gameObject);
        else Instance = this;
    }

    private void fadein()
    {
        if (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
        }

        else if (canvasGroup.alpha == 1) { visible =true; }
    }

    private void fadeout()
    {
        if (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
        }

        else if (canvasGroup.alpha == 0) { visible = false; }

    }
}
