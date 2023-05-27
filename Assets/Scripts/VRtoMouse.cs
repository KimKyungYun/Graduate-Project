using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRtoMouse : MonoBehaviour
{
    // Start is called before the first frame update
    public void Selected(SelectEnterEventArgs args){
        Input.GetMouseButtonDown(0);
    }
    public void SelectOut(SelectExitEventArgs args){
    }
}
