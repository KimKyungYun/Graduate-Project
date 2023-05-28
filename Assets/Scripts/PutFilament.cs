using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ModularExtrusionsMachines;

public class PutFilament : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject printer;
    public GameObject printer_filament;
    public GameObject line_top;
    public GameObject line_bottom;
    public GameObject print_object;
    private Vector3 firstLocation;
    private Vector3 angle;

    void Start() {
        firstLocation=transform.position;
        angle=transform.eulerAngles;
    }   

   

    // Update is called once per frame
    public void ChangeAlpha(){
        Color oldColor = GetComponent<Renderer>().material.color;        
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 1);
        Color objectColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0);
        printer_filament.GetComponent<Renderer>().material.SetColor("_Color", newColor);
        line_top.GetComponent<Renderer>().material.SetColor("_Color", newColor);
        line_bottom.GetComponent<Renderer>().material.SetColor("_Color", newColor);
        print_object.GetComponent<Renderer>().material.SetColor("_Color", objectColor);
    }
    
    void Update()
    {
        if(Vector3.Distance(printer_filament.transform.position, transform.position)<=2.0f&&printer.GetComponent<CartesianXYZ>().enabled == false)
        {
            ChangeAlpha();
            transform.position=firstLocation;
            transform.eulerAngles=angle;
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
}
//gameobject.SetActive 활성 비활성

