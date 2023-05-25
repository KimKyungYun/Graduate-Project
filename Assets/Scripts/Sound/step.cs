using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class step : MonoBehaviour
{
    public AudioSource footstepsSound;

    void Update()
    {
        if (ControllerInputManager.Instance.LeftJoystickControlled())
        {
            footstepsSound.enabled = true;
        }
        else
        {
            footstepsSound.enabled = false;
        }
    }
}
