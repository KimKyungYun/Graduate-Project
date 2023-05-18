using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform filament;
    public GameObject printer;
    public TextMesh temp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        string ttt="no";
        if(Vector3.Distance(printer.transform.position, filament.transform.position)<=1.0f){
            ttt+=Vector3.Distance(printer.transform.position, filament.transform.position)+" " ;
        }
        temp.text= ttt;
    }
}
