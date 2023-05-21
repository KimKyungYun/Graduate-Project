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
            presentMachine.GetComponent<Fade>().visible= true;
        }
        else
        {
            presentMachine.GetComponent<CartesianXYZ>().enabled = true;
        }

        gameObject.SetActive(false);
    }

    public void controlOff()
    {
        gameObject.SetActive(false);
    }

    public void setPresentMachine(GameObject machine)
    {
        presentMachine = machine;
    }
}

