using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonClick : MonoBehaviour
{
    public GameObject button;
    

    // Update is called once per frame
    public void Push(SelectEnterEventArgs args)
    {
        button.transform.Translate(Vector3.down*Time.deltaTime);
    }
    public void Pull(SelectExitEventArgs args)
    {
        button.transform.Translate(Vector3.up*Time.deltaTime);
    }
}
