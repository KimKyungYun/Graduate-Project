using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class AfterProcessTrigger : MonoBehaviour
{
    public GameObject[] target_triggers;
    public GameObject display;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
  
        if (Vector3.Distance(transform.position, target_triggers[0].transform.position) <= 2.0f
            || Vector3.Distance(transform.position, target_triggers[1].transform.position) <= 2.0f
            || Vector3.Distance(transform.position, target_triggers[2].transform.position) <= 2.0f)
        {
            display.GetComponent<XRSimpleInteractable>().enabled = true;
        }
        else
        {
            display.GetComponent<XRSimpleInteractable>().enabled = false;
        }


    }
}
