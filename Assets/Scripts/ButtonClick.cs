using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonClick : MonoBehaviour
{
    public GameObject button;
    public float degree=0.1f;
    

    // Update is called once per frame
    public void Push(SelectEnterEventArgs args)
    {
        button.transform.Translate(Vector3.down*degree);
    }
    public void Pull(SelectExitEventArgs args)
    {
        button.transform.Translate(Vector3.up*degree);
    }
}
