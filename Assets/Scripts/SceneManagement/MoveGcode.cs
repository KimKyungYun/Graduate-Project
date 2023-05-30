using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class MoveGcode : MonoBehaviour
{
    public GameObject printerFilament;

    public void GOGO(SelectEnterEventArgs args){
        Material material = printerFilament.GetComponent<MeshRenderer>().material;
        Color filamentClolor = material.color;

        if (filamentClolor.a !=0 )
        {
            SavedInfo.Instance.setUsedFilamentBeforeGcode(filamentClolor);
        }

        Vector3 originalPos = Camera.main.transform.root.position;
        SceneHandler.Instance.switchScifiToGcode(originalPos);
    }
}