using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SecondButtonClick : MonoBehaviour
{
    public GameObject display;
    public GameObject glass;
    public float degree=0.1f;
    

    // Update is called once per frame
    public void Push(SelectEnterEventArgs args)
    {
        transform.Translate(Vector3.down*degree);
        if(!display.GetComponent<SecondProcess>().isClick){
            glass.GetComponent<AudioSource>().Play();
        }
    }
    public void Pull(SelectExitEventArgs args)
    {
        transform.Translate(Vector3.up*degree);
    }
}
