using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedGameInfo : MonoBehaviour
{
    static public SavedGameInfo Instance;


    private PrinterType selectedPrinterType;
    private FilamentType selectedFilamentType;
    private ColorType selectedColorType;

    private void Awake()
    {
        makeSingleTon();
    }
    private void Update()
    {
        Debug.Log(selectedPrinterType);
        Debug.Log(selectedFilamentType);
        Debug.Log(selectedColorType);
    }



    public PrinterType SelectedPrinterType { get => selectedPrinterType; set => selectedPrinterType = value; }
    public FilamentType SelectedFilamentType { get => selectedFilamentType; set => selectedFilamentType = value; }
    public ColorType SelectedColorType { get => selectedColorType; set => selectedColorType = value; }

    private void makeSingleTon()
    {
        if (Instance != null) Destroy(this.gameObject);
        else
        {
            Instance = this;
        }
    }
}
