using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct QuestionData
{
    public int sol; // ���信 �ش��ϴ� ��ȣ
    public string t1; // ������ ��Ÿ���� �κ��� �ؽ�Ʈ
    public string t2;

    public string option0; // ����0
    public string option1; // ����1
    public string option2; // ����2
    public string option3; // ����3
    public string option4; // ����4
    public string option5; // ����5
    public string option6; // ����6
    public string option7; // ����7

    public QuestionData(int sol, string t1, string t2, 
        string option0, string option1, string option2, string option3, 
        string option4, string option5, string option6, string option7) // ������
    {
        this.sol = sol;
        this.t1 = t1;
        this.t2 = t2;
        this.option0 = option0;
        this.option1 = option1;
        this.option2 = option2;
        this.option3 = option3;
        this.option4 = option4;
        this.option5 = option5;
        this.option6 = option6;
        this.option7 = option7;
    }
}
