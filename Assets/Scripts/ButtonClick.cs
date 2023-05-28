using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonClick : MonoBehaviour
{
    public GameObject display;
    public GameObject glass;
    public float degree=0.1f;
    

    // Update is called once per frame
    public void Push(SelectEnterEventArgs args)
    {
        transform.Translate(Vector3.down*degree);
        if(!display.GetComponent<AfterProcess>().onProgress){
            glass.GetComponent<AudioSource>().Play();
        }
    }
    public void Pull(SelectExitEventArgs args)
    {
        transform.Translate(Vector3.up*degree);
    }
}
