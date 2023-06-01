using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ModularExtrusionsMachines;
using System;

public class PutFilament : MonoBehaviour
{
    public PrintingSet[] printingSetList;
    private Vector3 firstLocation;
    private Vector3 angle;

    void Start() {
        firstLocation=transform.position;
        angle=transform.eulerAngles;
    }   

   

    // Update is called once per frame
    public void ChangeColor(PrintingSet printingSet)
    {
        Color GrabbedFilamentColor = GetComponent<Renderer>().material.color;        
        Color printoutColor = new Color(GrabbedFilamentColor.r, GrabbedFilamentColor.g, GrabbedFilamentColor.b, 0);

        printingSet.changeColorExceptPrintout(GrabbedFilamentColor);
        printingSet.changePrintOutColor(printoutColor);
    }
    
    void Update()
    {
        foreach(var printingSet in printingSetList)
        {
            if(Vector3.Distance(printingSet.printer_filament.transform.position,transform.position)<=2.0f && !printingSet.isOperation())
            {
                if(GameManager.Instance.getPresentGamePhaseName().Equals("InsertFilament"))
                {
                    ChangeColor(printingSet);
                    initFilamentPos();
                };
            }
        }
        
        //Injection 
       if (SavedFilamentColor.Instance.PrintingSetList == null || SavedFilamentColor.Instance.PrintingSetList[0].printer == null)
        {
            SavedFilamentColor.Instance.PrintingSetList = printingSetList;
        }

    }
    public void Grab(HoverEnterEventArgs args){
        if(GameManager.Instance.getPresentGamePhaseName()=="InsertFilament"){
            gameObject.GetComponent<XRGrabInteractable>().enabled=true;
        }
        else{
            gameObject.GetComponent<XRGrabInteractable>().enabled=false;
        }
    }

    private void initFilamentPos()
    {
        transform.position = firstLocation;
        transform.eulerAngles = angle;
    }

}



[System.Serializable]
public class PrintingSet
{
    public PrinterType printerType;
    public GameObject printer;
    public GameObject printer_filament;
    public GameObject[] lines;
    public GameObject print_object;

    public void changeColorExceptPrintout(Color color)
    {
        printer_filament.GetComponent<Renderer>().material.SetColor("_Color", color);
        foreach(GameObject line in lines)
        {
            line.GetComponent<Renderer>().material.SetColor("_Color", color);

        }
    }

    public void changePrintOutColor(Color color)
    {
        print_object.GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    public bool isOperation()
    {
        if (printer.GetComponent<CartesianXYZ>() != null)
        {
            return printer.GetComponent<CartesianXYZ>().enabled;
        }

        else if (printer.GetComponent<DeltaXYZ>() != null)
        {
            return printer.GetComponent<DeltaXYZ>().enabled;
        }

        throw new ArgumentException("잘못된 프린터가 인자로 들어왔다.");
    }
}


