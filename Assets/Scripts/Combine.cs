using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Combine : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bottom;
    public GameObject print_target;

    // Update is called once per frame
    void Update()
    {
        if(print_target.GetComponent<Renderer>().material.color.a==1){
            print_target.GetComponent<XRGrabInteractable>().enabled=true;
        }
        else{
            gameObject.transform.position=new Vector3(bottom.transform.position.x,gameObject.transform.position.y ,bottom.transform.position.z);

        }
        
    }
}
