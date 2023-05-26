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

        public int sol; // 정답에 해당하는 번호
        public string t1; // 문제가 나타나는 부분의 텍스트
        public string t2;
        public string m1; // 보기1
        public string m2; // 보기2
        public string m3; // 보기3
        public string m4; // 보기4
        public string m5; // 보기5
        public string m6; // 보기6
        public string m7; // 보기7
        public string m8; // 보기8

        public Data(int sol,string t1, string t2, string m1, string m2, string m3, string m4, string m5, string m6, string m7, string m8) // 생성자
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
        public Camera mainCamera; // 카메라 배경음
        public AudioClip newMusicClip; // 성공시 변경할 음악 클립
        public AudioClip newMusicClip2; // 실패시 변경할 음악 클립
        private AudioSource audioSource2; // 카메라의 Audio Source 컴포넌트
        public static int cnt; // 틀린 횟수 저장
        public Text fail; // 실패시 띄울 텍스트

        // 참조할 텍스트 객체를 저장할 변수
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
        

        // 데이터 생성
        void Start()
        {
            Qdata = new Data[8];

            Qdata[0] = new Data(3, "노즐의 온도를 210도로 설정하세요!", "S210", "G0", "G28", "M104", "M140", "M2", "M107", "T0", "T2");
            Qdata[1] = new Data(7, "노즐의 온도가 210도에 도달할 때까지 기다리세요.", "S210", "G0", "G1", "G2", "G3", "M2", "M107", "M109", "T2");
            Qdata[2] = new Data(6, "열을 식힐 쿨러의 속도를 최대로 설정하세요!", "S255", "G0", "G1", "G2", "M2", "M104", "M106", "M107","T2");
            Qdata[3] = new Data(6, "원을 그리기 위해 절대 좌표 모드를 활성화 하세요.", "", "G0", "G1", "G2", "G3", "G4", "G90", "G91", "G92");
            Qdata[4] = new Data(1, "반지름 30CM 인 원을 그리기 위해 원점에서 Y축으로 30CM 이동하세요.", "X0 Y300", "G0", "G1", "G2", "G3", "M2", "M107", "T0", "T2");
            Qdata[5] = new Data(3, "이제 원을 그리세요!", "X0 Y300 I0 J-300", "G0", "G1", "G2", "G3", "G4", "G20", "T0", "T2");
            Qdata[6] = new Data(6, "작업이 끝났습니다. 원점으로 이동하세요.", "", "G0", "G1", "G2", "G3", "G20", "G28", "T0", "T2");
            Qdata[7] = new Data(6, "쿨러를 끄세요.", "", "G0", "G28", "M104", "M140", "M2", "M107", "T0", "T2");


            IrregularCircleGUI.SetActive(false);
            fail.enabled = false;

            audioSource = gameObject.AddComponent<AudioSource>();

            // 카메라의 Audio Source 컴포넌트를 가져옴
            audioSource2 = mainCamera.GetComponent<AudioSource>();
        }


        // Update is called once per frame
        void Update()
        {
            

        }

        public int CheckA(int answer, int num) // num 은 문제의 번호
        {
            if (Qdata[num].sol == answer)
            {
                return 1;
            }

            return 0;
        }


        // 여기에 종료시 씬 이동 코드 작성
        public void backS()
        {
            // 씬 이동 코드

            Debug.Log("종료함수 들어옴");

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


                if (++cnt == 4) // 기회가 없을때 실패처리
                {
                    // 비활성화
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
                    audioSource2.loop = false; // 반복 재생 비활성화
                    audioSource2.Play();

                    Invoke("backS", 3.0f); // 3초뒤에 작업실로 씬이동

                }

                return 0;
            }
            
            if(num == 8)
            {// 퀴즈가 끝났을 때 처리

                // 성공 화면 출력 및 사운드 변경
                audioSource2.clip = newMusicClip;
                audioSource2.Play();

                cav.SetActive(false); 

                Invoke("backS", 5.0f); // 5초뒤에 작업실로 씬이동

                return 0;
            }
            
            // o 표시 띄우기 & 유지
            IrregularCircleGUI.SetActive(true);

            Invoke("OffL", 1.0f); // 1초뒤에 사라짐

            audioSource.PlayOneShot(soundEffect);

            // 다음 문제로 셋팅
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

            Debug.Log("텍스트 설정까지는 옴");

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