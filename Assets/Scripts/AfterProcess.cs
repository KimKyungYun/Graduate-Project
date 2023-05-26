using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AfterProcess : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public Material target_material;

    public void ChangeMaterial(SelectEnterEventArgs args){
        target.GetComponent<SkinnedMeshRenderer> ().material=target_material;
    }
}
