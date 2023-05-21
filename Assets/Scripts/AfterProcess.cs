using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AfterProcess : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject heart;
    public Material heart_material;

    public void ChangeMaterial(SelectEnterEventArgs args){
        heart.GetComponent<SkinnedMeshRenderer> ().material=heart_material;
    }
}
