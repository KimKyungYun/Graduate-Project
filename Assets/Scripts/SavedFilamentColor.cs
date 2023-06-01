using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavedFilamentColor : MonoBehaviour
{
    static public SavedFilamentColor Instance;

    private PrintingSet[] printingSetList; // Injection By PutFilament
    private Dictionary<PrinterType, Color> colorMapper = new Dictionary<PrinterType, Color>();
    private bool colorLoaded = true; // When Gcode Scene is Loaded, colorLoaded become false. is Injected By GcodeManager (Awake func)


    private void Awake()
    {
        makeSingleTon();
    }

    private void Update()
    {
        if (!colorLoaded  && SceneManager.GetActiveScene().name == "sci-fi")
        {
            if (printingSetList[0].printer != null) // When printingSetList is perfectly injected
            {
                Debug.Log("loadFilamentColor() 호출");
                loadFilamentColor();
                colorLoaded = true;
            }
        }
    }


    public void loadFilamentColor()
    {
        foreach (var pair in colorMapper)
        {
            PrinterType printerType = pair.Key;
            Color color = pair.Value;
            Color invisibleColor = new Color(color.r, color.g, color.b, 0);
            Debug.Log("printerType : " + pair.Key + "Color : " + pair.Value);
            PrintingSet printingSet = getPrintingSetByType(printerType);
            Debug.Log(printingSet.printer.name);
            printingSet.changeColorExceptPrintout(color);
            printingSet.changePrintOutColor(invisibleColor);
        }
    }

    private PrintingSet getPrintingSetByType(PrinterType printerType)
    {
        foreach (PrintingSet printingSet in printingSetList)
        {
            if (printingSet.printerType == printerType)
            {
                return printingSet;
            }
        }

        throw new ArgumentException("해당 프린터 타입에 맞는 PrintingSet을 찾을 수 없음");
    }

    public void saveColor(PrinterType printerType, Color color)
    {
        colorMapper.Add(printerType, color);
    }

    public void clearColorInfo()
    {
        colorMapper.Clear();
    }

    private void makeSingleTon()
    {
        if (Instance != null) Destroy(this.gameObject);
        else
        {
            Instance = this;
        }
    }

    public PrintingSet[] PrintingSetList { get => printingSetList; set => printingSetList = value; }
    public void setColorLoaded(bool status) { colorLoaded = status; }

}