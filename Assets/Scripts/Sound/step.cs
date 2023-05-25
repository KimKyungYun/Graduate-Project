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
            Debug.Log("LeftJoystic¡∂¿€¡ﬂ");
            footstepsSound.enabled = true;
        }
        else
        {
            footstepsSound.enabled = false;
        }
    }
}
