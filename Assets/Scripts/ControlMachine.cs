using ModularExtrusionsMachines;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class ControlMachine : MonoBehaviour
{
    private GameObject presentMachine;
    

    public void controlOn()
    {
        if (presentMachine.name.Equals("Delta"))
        {
            presentMachine.GetComponent<DeltaXYZ>().enabled = true;
        }
        else
        {
            presentMachine.GetComponent<CartesianXYZ>().enabled = true;
        }

        if (presentMachine.name.Equals("LaserCutter"))
        {
            gameObject.transform.position = new Vector3(1000.0f, 1000.0f, 1000.0f);
            return;
        }

        if (GameManager.Instance.getPresentGamePhaseName().Equals("GcodeClear"))
        {
            presentMachine.GetComponent<Fade>().visible = true;


            savePrinterType(presentMachine.name);
            saveFilamentType(presentMachine.name);



            GameManager.Instance.setGamePhase("BeforeMeetLuther");
        }

        gameObject.transform.position=new Vector3(1000.0f,1000.0f,1000.0f);
    }

    public void controlOff()
    {
        if (presentMachine.name.Equals("Delta"))
        {
            presentMachine.GetComponent<DeltaXYZ>().enabled = false;
        }
        else
        {
            presentMachine.GetComponent<CartesianXYZ>().enabled = false;

        }
        gameObject.transform.position=new Vector3(1000.0f,1000.0f,1000.0f);
    }

    public void setPresentMachine(GameObject machine)
    {
        presentMachine = machine;
    }

    private void savePrinterType(string printerName)
    {
        SavedGameInfo savedGameInfo = SavedGameInfo.Instance;

        switch (printerName)
        {
            case "MonoTower":
                savedGameInfo.SelectedPrinterType = PrinterType.CartesianSmall;
                break;
            case "Classic Printer":
                savedGameInfo.SelectedPrinterType = PrinterType.CartesianBig;
                break;
            case "Delta":
                savedGameInfo.SelectedPrinterType = PrinterType.Delta;
                break;
        }
    }

    private void saveFilamentType(string printerName)
    {
        SavedGameInfo savedGameInfo = SavedGameInfo.Instance;

        GameObject selectedPrinter = GameObject.Find(printerName);

        GameObject selectedFilament = selectedPrinter.GetNamedChild("SpoolFilament_LOD0");


        Color color = selectedFilament.GetComponent<Renderer>().material.color;
       
        if ( color.r >0.99f)
        {
            savedGameInfo.SelectedFilamentType = FilamentType.ABS;
        }

        else if(color.r > 0.95f)
        {
            savedGameInfo.SelectedFilamentType = FilamentType.Flexible;
        }

        else if (color.r > 0.58f)
        {
            savedGameInfo.SelectedFilamentType = FilamentType.Engineer;
        }

        else
        {
            savedGameInfo.SelectedFilamentType = FilamentType.PLA;
        }

    }
}

