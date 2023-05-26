using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AfterProcess : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public Material target_material;

    public void FirstProcess(SelectEnterEventArgs args){
        target_material.SetColor("_Color",target.GetComponent<Renderer>().material.color);
        target.GetComponent<Renderer>().material=target_material;
    }

    public void ChangeMaterial(SelectEnterEventArgs args){
        target.GetComponent<Renderer>().material=target_material;
    }
}
