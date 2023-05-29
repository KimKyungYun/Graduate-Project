using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavedInfo : MonoBehaviour
{
    static public SavedInfo Instance;

    private GameObject printer_filament;
    private GameObject line_top;
    private GameObject line_bottom;
    private GameObject print_object;

    private Color filamentColorBeforeGcode;

    private void Awake()
    {
        makeSingleTon();
    }

    private void Update()
    {
        if (printer_filament == null && SceneManager.GetActiveScene().name == "sci-fi")
        {
            initVariable();
            loadFialmentColor();
        }
    }

    public void loadFialmentColor()
    {
        if(  !(filamentColorBeforeGcode.r == 0 && filamentColorBeforeGcode.g == 0 && filamentColorBeforeGcode.b == 0)  )
        {
            Color loadColor = new Color(filamentColorBeforeGcode.r, filamentColorBeforeGcode.g, filamentColorBeforeGcode.b, 1);
            Color loadPrintedObjectColor = new Color(filamentColorBeforeGcode.r, filamentColorBeforeGcode.g, filamentColorBeforeGcode.b, 0); // gamma is 0 
            printer_filament.GetComponent<Renderer>().material.SetColor("_Color", loadColor);
            line_top.GetComponent<Renderer>().material.SetColor("_Color", loadColor);
            line_bottom.GetComponent<Renderer>().material.SetColor("_Color", loadColor);
            print_object.GetComponent<Renderer>().material.SetColor("_Color", loadPrintedObjectColor);
        } 

    }
    public void setUsedFilamentBeforeGcode(Color color)
    {
        filamentColorBeforeGcode = color;
    }

    private void initVariable()
    {
        printer_filament = GameObject.Find("SpoolFilament_LOD0");
        line_top = GameObject.Find("Filament2Spool");
        line_bottom = GameObject.Find("Filament2Head");
        print_object = GameObject.Find("Engine_left_2");
    }

    private void makeSingleTon()
    {
        if (Instance != null) Destroy(this.gameObject);
        else
        {
            Instance = this;
        }
    }
}