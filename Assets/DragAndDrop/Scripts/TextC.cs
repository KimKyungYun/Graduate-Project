using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace DragAndDrop
{
    

    public struct Data
    {

        public int sol; // ���信 �ش��ϴ� ��ȣ
        public string t1; // ������ ��Ÿ���� �κ��� �ؽ�Ʈ
        public string t2;
        public string m1; // ����1
        public string m2; // ����2
        public string m3; // ����3
        public string m4; // ����4
        public string m5; // ����5
        public string m6; // ����6
        public string m7; // ����7
        public string m8; // ����8

        public Data(int sol,string t1, string t2, string m1, string m2, string m3, string m4, string m5, string m6, string m7, string m8) // ������
        {
            this.sol = sol;
            this.t1 = t1;
            this.t2 = t2;
            this.m1 = m1;
            this.m2 = m2;
            this.m3 = m3;
            this.m4 = m4;
            this.m5 = m5;
            this.m6 = m6;
            this.m7 = m7;
            this.m8 = m8;
        }
        
    }

    public class TextC : MonoBehaviour
    {
        public Data[] Qdata; 
        public GameObject IrregularCircleGUI;
        public GameObject Inv;
        public GameObject Bar;
        public GameObject SS;
        public GameObject cav;
        public AudioClip soundEffect;  // Assign your sound effect in the Inspector
        public AudioClip soundEffect2;
        private AudioSource audioSource;
        public Camera mainCamera; // ī�޶� �����
        public AudioClip newMusicClip; // ������ ������ ���� Ŭ��
        public AudioClip newMusicClip2; // ���н� ������ ���� Ŭ��
        private AudioSource audioSource2; // ī�޶��� Audio Source ������Ʈ
        public static int cnt; // Ʋ�� Ƚ�� ����
        public Text fail; // ���н� ��� �ؽ�Ʈ

        // ������ �ؽ�Ʈ ��ü�� ������ ����
        public Text myt1;
        public Text myt2;
        public Text mym1;
        public Text mym2;
        public Text mym3;
        public Text mym4;
        public Text mym5;
        public Text mym6;
        public Text mym7;
        public Text mym8;
        

        // ������ ����
        void Start()
        {
            Qdata = new Data[8];

            Qdata[0] = new Data(3, "������ �µ��� 210���� �����ϼ���!", "S210", "G0", "G28", "M104", "M140", "M2", "M107", "T0", "T2");
            Qdata[1] = new Data(7, "������ �µ��� 210���� ������ ������ ��ٸ�����.", "S210", "G0", "G1", "G2", "G3", "M2", "M107", "M109", "T2");
            Qdata[2] = new Data(6, "���� ���� ���� �ӵ��� �ִ�� �����ϼ���!", "S255", "G0", "G1", "G2", "M2", "M104", "M106", "M107","T2");
            Qdata[3] = new Data(6, "���� �׸��� ���� ���� ��ǥ ��带 Ȱ��ȭ �ϼ���.", "", "G0", "G1", "G2", "G3", "G4", "G90", "G91", "G92");
            Qdata[4] = new Data(1, "������ 30CM �� ���� �׸��� ���� �������� Y������ 30CM �̵��ϼ���.", "X0 Y300", "G0", "G1", "G2", "G3", "M2", "M107", "T0", "T2");
            Qdata[5] = new Data(3, "���� ���� �׸�����!", "X0 Y300 I0 J-300", "G0", "G1", "G2", "G3", "G4", "G20", "T0", "T2");
            Qdata[6] = new Data(6, "�۾��� �������ϴ�. �������� �̵��ϼ���.", "", "G0", "G1", "G2", "G3", "G20", "G28", "T0", "T2");
            Qdata[7] = new Data(6, "�𷯸� ������.", "", "G0", "G28", "M104", "M140", "M2", "M107", "T0", "T2");


            IrregularCircleGUI.SetActive(false);
            fail.enabled = false;

            audioSource = gameObject.AddComponent<AudioSource>();

            // ī�޶��� Audio Source ������Ʈ�� ������
            audioSource2 = mainCamera.GetComponent<AudioSource>();
        }


        // Update is called once per frame
        void Update()
        {
            

        }

        public int CheckA(int answer, int num) // num �� ������ ��ȣ
        {
            if (Qdata[num].sol == answer)
            {
                return 1;
            }

            return 0;
        }


        // ���⿡ ����� �� �̵� �ڵ� �ۼ�
        public void backS()
        {
            // �� �̵� �ڵ�

            Debug.Log("�����Լ� ����");

            Application.Quit();
        }

        public void OffL()
        {
            IrregularCircleGUI.SetActive(false);
        }
        
        public int SetNextText(int num) 
        {
            if(num == -1)
            {
                audioSource.PlayOneShot(soundEffect2);


                if (++cnt == 4) // ��ȸ�� ������ ����ó��
                {
                    // ��Ȱ��ȭ
                    myt1.enabled = false;
                    myt2.enabled = false;
                    mym1.enabled = false;
                    mym2.enabled = false;
                    mym3.enabled = false;
                    mym4.enabled = false;
                    mym5.enabled = false;
                    mym6.enabled = false;
                    mym7.enabled = false;
                    mym8.enabled = false;

                    Inv.SetActive(false);
                    Bar.SetActive(false);
                    SS.SetActive(false);

                    fail.enabled = true;

                    audioSource2.clip = newMusicClip2;
                    audioSource2.loop = false; // �ݺ� ��� ��Ȱ��ȭ
                    audioSource2.Play();

                    Invoke("backS", 3.0f); // 3�ʵڿ� �۾��Ƿ� ���̵�

                }

                return 0;
            }
            
            if(num == 8)
            {// ��� ������ �� ó��

                // ���� ȭ�� ��� �� ���� ����
                audioSource2.clip = newMusicClip;
                audioSource2.Play();

                cav.SetActive(false); 

                Invoke("backS", 5.0f); // 5�ʵڿ� �۾��Ƿ� ���̵�

                return 0;
            }
            
            // o ǥ�� ���� & ����
            IrregularCircleGUI.SetActive(true);

            Invoke("OffL", 1.0f); // 1�ʵڿ� �����

            audioSource.PlayOneShot(soundEffect);

            // ���� ������ ����
            myt1.text = Qdata[num].t1;
            myt2.text = Qdata[num].t2;
            mym1.text = Qdata[num].m1;
            mym2.text = Qdata[num].m2;
            mym3.text = Qdata[num].m3;
            mym4.text = Qdata[num].m4;
            mym5.text = Qdata[num].m5;
            mym6.text = Qdata[num].m6;
            mym7.text = Qdata[num].m7;
            mym8.text = Qdata[num].m8;

            Debug.Log("�ؽ�Ʈ ���������� ��");

            return 1;
        }
        

        /*
        public bool IsEnd(int num2)
        {
            if(num2 = 5)
            {
                return 1;
            }

            return 0;
        }

        */
    }



}