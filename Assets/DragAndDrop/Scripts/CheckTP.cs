using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DragAndDrop
{

    // 드래그 위치가 적당한지 판단하고 처리를 하는 클래스
    public class CheckTP : MonoBehaviour
    {
        
        public  Transform targetSlot; // 놓는 대상 슬롯
        public Transform[] St = new Transform[8]; // 보기 슬롯
        public  Vector3[] pos;
        public  Vector3 tPos;

        void Start()
        {
            pos = new Vector3[] { St[0].position, St[1].position, St[2].position, St[3].position, St[4].position, St[5].position, St[6].position, St[7].position };
            tPos = targetSlot.position;
        }

        void Update()
        { }

        public int checkP(int ans)
        // ans 는 보기 슬롯의 번호

        {
            Debug.Log("St[ans-1].position : " + St[ans - 1].position);
            Debug.Log("targetSlot.position : " + targetSlot.position);

            float distance = Vector3.Distance(St[ans-1].position, targetSlot.position); // 보기 슬롯와 대상 슬롯 사이의 거리를 구함

            Debug.Log("distance" + distance);

            if (distance <= 80.0f) // 거리가 1000 안쪽이라면 정상
            {
                Debug.Log("checkP 호출됨 T");
                return 1;
            }
            else
            {
                Debug.Log("checkP 호출됨 F");
    
                
                return 0;
            }
        }
    }


}