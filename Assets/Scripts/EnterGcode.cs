using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGcode : MonoBehaviour
{
    private GameObject controlUI;

    void Start()
    {
        controlUI = FindObjectOfType<ControlMachine>().gameObject;
    }

    public void showControlPanel()
    {
        VRCanvasHandler.Instance.displayOnObjectRotation(controlUI, gameObject, 1.3f);
        controlUI.SetActive(true);

        controlUI.GetComponent<ControlMachine>().setPresentMachine(transform.root.gameObject);
    }
}
