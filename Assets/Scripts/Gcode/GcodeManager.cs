using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;


public class GcodeManager : MonoBehaviour
{
    static public GcodeManager Instance;

    private GameObject SelectedOption;
    private Vector3 OriginalPos;
    private QuestionData[] questionData = new QuestionData[8];
    private int questionNum = 0;

    public Text questionText;
    public Text subAnswer;
    public Text[] OptionText = new Text[8]; 

    private void Awake()
    {
        makeSingleTon();
    }

    private void Start()
    {
        questionData[0] = new QuestionData(3, "노즐의 온도를 210도로 설정하세요!", "S210", "G0", "G28", "M104", "M140", "M2", "M107", "T0", "T2");
        questionData[1] = new QuestionData(7, "노즐의 온도가 210도에 도달할 때까지 기다리세요.", "S210", "G0", "G1", "G2", "G3", "M2", "M107", "M109", "T2");
        questionData[2] = new QuestionData(6, "열을 식힐 쿨러의 속도를 최대로 설정하세요!", "S255", "G0", "G1", "G2", "M2", "M104", "M106", "M107", "T2");
        questionData[3] = new QuestionData(6, "원을 그리기 위해 절대 좌표 모드를 활성화 하세요.", "", "G0", "G1", "G2", "G3", "G4", "G90", "G91", "G92");
        questionData[4] = new QuestionData(1, "반지름 30CM 인 원을 그리기 위해 원점에서 Y축으로 30CM 이동하세요.", "X0 Y300", "G0", "G1", "G2", "G3", "M2", "M107", "T0", "T2");
        questionData[5] = new QuestionData(3, "이제 원을 그리세요!", "X0 Y300 I0 J-300", "G0", "G1", "G2", "G3", "G4", "G20", "T0", "T2");
        questionData[6] = new QuestionData(6, "작업이 끝났습니다. 원점으로 이동하세요.", "", "G0", "G1", "G2", "G3", "G20", "G28", "T0", "T2");
        questionData[7] = new QuestionData(6, "쿨러를 끄세요.", "", "G0", "G28", "M104", "M140", "M2", "M107", "T0", "T2");

        initQuestion();
    }
    
    private void initQuestion()
    {
        QuestionData question = questionData[questionNum];

        questionText.text = question.t1;
        subAnswer.text = question.t2;

        OptionText[0].text = question.option0;
        OptionText[1].text = question.option1;
        OptionText[2].text = question.option2;
        OptionText[3].text = question.option3;
        OptionText[4].text = question.option4;
        OptionText[5].text = question.option5;
        OptionText[6].text = question.option6;
        OptionText[7].text = question.option7;

        SelectedOption = null;
    }
    public void presentSlotToOriginal()
    {
        SelectedOption.GetComponent<RectTransform>().position = OriginalPos;
    }

    //선택지 오브젝트 이름으로 문자열 파싱하고 checkAnswer로 넘겨줌.
    public void checkAnswerBridge()
    {
        if(SelectedOption != null)
        {
            string objectName = SelectedOption.name;

            char optionNum = objectName[objectName.Length - 1];
            int solutionNum = questionData[questionNum].sol;

            checkAnswer(solutionNum, optionNum - '0');
        }
    }

    private void checkAnswer(int solution, int choicedAnswer)
    {
        EffectManager effectManager = EffectManager.Instance;
        SoundManager soundManager = SoundManager.Instance;
        
        presentSlotToOriginal();

        if (solution == choicedAnswer)
        {
            // All Question Solved
            if (questionNum == questionData.Length-1)
            {
                soundManager.playAllClearSound();
                effectManager.deactivateMainCanvas();
                effectManager.activeEffectCanvas();
                return;
            }

            soundManager.playCorrectAnswerSound();
            effectManager.showCorrectAnswerCircle();

            questionNum++;
            initQuestion();
        }

        else // 틀렷다는 사운드 + HP바 깎이게
        {
            ProgressBarCircle progressBar = FindObjectOfType<ProgressBarCircle>();
            
            if (progressBar.UpdateValue(-1) == 0) // progressBar의 Health가 0일때
            {
                effectManager.showFail();
                Camera.main.GetComponent<AudioSource>().enabled= false;
                soundManager.playFailSound();
            }

            soundManager.playWrongAnswerSound();
        }
    }

    public void setSelectedOption(GameObject option) {  SelectedOption = option;}
    public void setOriginalPos(Vector3 pos) { OriginalPos = pos;}
    public GameObject GetSelectedOption() { return SelectedOption; }

    private void makeSingleTon()
    {
        if (Instance != null) Destroy(this.gameObject);
        else
        {
            Instance = this;
        }
    }
}
