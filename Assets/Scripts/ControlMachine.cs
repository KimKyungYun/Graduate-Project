using ModularExtrusionsMachines;
using System.Collections;
using System.Collections.Generic;
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
            presentMachine.GetComponent<Fade>().visible= true;

        }

        gameObject.SetActive(false);
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
        gameObject.SetActive(false);
    }

    public void setPresentMachine(GameObject machine)
    {
        presentMachine = machine;
    }
}

