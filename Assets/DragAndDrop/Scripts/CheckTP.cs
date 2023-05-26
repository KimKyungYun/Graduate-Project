using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DragAndDrop
{

    // �巡�� ��ġ�� �������� �Ǵ��ϰ� ó���� �ϴ� Ŭ����
    public class CheckTP : MonoBehaviour
    {
        
        public  Transform targetSlot; // ���� ��� ����
        public Transform[] St = new Transform[8]; // ���� ����
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
        // ans �� ���� ������ ��ȣ

        {
            Debug.Log("St[ans-1].position : " + St[ans - 1].position);
            Debug.Log("targetSlot.position : " + targetSlot.position);

            float distance = Vector3.Distance(St[ans-1].position, targetSlot.position); // ���� ���Կ� ��� ���� ������ �Ÿ��� ����

            Debug.Log("distance" + distance);

            if (distance <= 80.0f) // �Ÿ��� 1000 �����̶�� ����
            {
                Debug.Log("checkP ȣ��� T");
                return 1;
            }
            else
            {
                Debug.Log("checkP ȣ��� F");
    
                
                return 0;
            }
        }
    }


}