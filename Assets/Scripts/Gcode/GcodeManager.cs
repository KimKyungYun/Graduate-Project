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
        questionData[0] = new QuestionData(3, "������ �µ��� 210���� �����ϼ���!", "S210", "G0", "G28", "M104", "M140", "M2", "M107", "T0", "T2");
        questionData[1] = new QuestionData(7, "������ �µ��� 210���� ������ ������ ��ٸ�����.", "S210", "G0", "G1", "G2", "G3", "M2", "M107", "M109", "T2");
        questionData[2] = new QuestionData(6, "���� ���� ���� �ӵ��� �ִ�� �����ϼ���!", "S255", "G0", "G1", "G2", "M2", "M104", "M106", "M107", "T2");
        questionData[3] = new QuestionData(6, "���� �׸��� ���� ���� ��ǥ ��带 Ȱ��ȭ �ϼ���.", "", "G0", "G1", "G2", "G3", "G4", "G90", "G91", "G92");
        questionData[4] = new QuestionData(1, "������ 30CM �� ���� �׸��� ���� �������� Y������ 30CM �̵��ϼ���.", "X0 Y300", "G0", "G1", "G2", "G3", "M2", "M107", "T0", "T2");
        questionData[5] = new QuestionData(3, "���� ���� �׸�����!", "X0 Y300 I0 J-300", "G0", "G1", "G2", "G3", "G4", "G20", "T0", "T2");
        questionData[6] = new QuestionData(6, "�۾��� �������ϴ�. �������� �̵��ϼ���.", "", "G0", "G1", "G2", "G3", "G20", "G28", "T0", "T2");
        questionData[7] = new QuestionData(6, "�𷯸� ������.", "", "G0", "G28", "M104", "M140", "M2", "M107", "T0", "T2");

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

    //������ ������Ʈ �̸����� ���ڿ� �Ľ��ϰ� checkAnswer�� �Ѱ���.
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

        else // Ʋ�Ǵٴ� ���� + HP�� ���̰�
        {
            ProgressBarCircle progressBar = FindObjectOfType<ProgressBarCircle>();
            
            if (progressBar.UpdateValue(-1) == 0) // progressBar�� Health�� 0�϶�
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
