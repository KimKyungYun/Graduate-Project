using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutFilament : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject printer;
    public GameObject line;
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
        printer.GetComponent<Renderer>().material.SetColor("_Color", newColor);
        line.GetComponent<Renderer>().material.SetColor("_Color", newColor);
        print_object.GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", objectColor);
    }
    
    void Update()
    {
        if(Vector3.Distance(printer.transform.position, transform.position)<=1.0f)
        {
            ChangeAlpha();
            transform.position=firstLocation;
            transform.eulerAngles=angle;
        }

    }
}
//gameobject.SetActive 활성 비활성

