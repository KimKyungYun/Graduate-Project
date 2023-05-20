using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterController : MonoBehaviour
{
    private GameObject controlUI;

    void Start()
    {
        controlUI = FindObjectOfType<ControlMachine>().gameObject;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    VRCanvasHandler.Instance.displayOnObjectRotation(controlUI, gameObject, 1.3f);
        //    controlUI.SetActive(true);

        //    controlUI.GetComponent<ControlMachine>().setPresentMachine(transform.root.gameObject);
        //}
    }

    public void showControlPanel()
    {
        VRCanvasHandler.Instance.displayOnObjectRotation(controlUI, gameObject, 1.3f);
        controlUI.SetActive(true);

        controlUI.GetComponent<ControlMachine>().setPresentMachine(transform.root.gameObject);
    }
}
