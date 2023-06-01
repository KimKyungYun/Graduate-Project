using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class MoveGcode : MonoBehaviour
{
    public GameObject cartesianBigFilament;
    public GameObject deltaFilament;
    public GameObject cartesianSmallFilament;

    public void GOGO(SelectEnterEventArgs args){
        if(GameManager.Instance.getPresentGamePhaseName().Equals("GcodeDetail"))
        {
            saveColorProcess();
            Vector3 originalPos = Camera.main.transform.root.position;
            SceneHandler.Instance.switchScifiToGcode(originalPos);
        }
    }

    private void saveColorProcess()
    {
        Color cartesianBigColor = cartesianBigFilament.GetComponent<MeshRenderer>().material.color;
        Color deltaColor = deltaFilament.GetComponent<MeshRenderer>().material.color;
        Color cartesianSmallColor = cartesianSmallFilament.GetComponentInParent<MeshRenderer>().material.color;

        SavedFilamentColor savedFilamentColor = SavedFilamentColor.Instance;
        savedFilamentColor.clearColorInfo();

        if (cartesianBigColor.a != 0) { savedFilamentColor.saveColor(PrinterType.CartesianBig, cartesianBigColor); }
        if (deltaColor.a != 0) { savedFilamentColor.saveColor(PrinterType.Delta, deltaColor); }
        if (cartesianSmallColor.a != 0) { savedFilamentColor.saveColor(PrinterType.CartesianSmall, cartesianSmallColor); }

    }
}