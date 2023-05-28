using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SecondProcess : MonoBehaviour
{
    public GameObject display1;
    public GameObject display2;
    public GameObject display3;
    public GameObject display4;
    public bool isClick=false;

    void Update(){
        isClick=display1.GetComponent<AfterProcess>().onProgress||display2.GetComponent<AfterProcess>().onProgress||display3.GetComponent<AfterProcess>().onProgress||display4.GetComponent<AfterProcess>().onProgress;
    }
}
