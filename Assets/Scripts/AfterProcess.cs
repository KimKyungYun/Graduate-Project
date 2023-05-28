using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AfterProcess : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public Material target_material;
    public GameObject door;
    public GameObject door_button;
    public int delay_time=5;
// &&GameManager.Instance.getPresentGamePhaseName()=="AfterProcess"
    public void FirstProcess(SelectEnterEventArgs args){
        if(door.transform.position.y<0.5f){
            target_material.SetColor("_Color",target.GetComponent<Renderer>().material.color);
            target.GetComponent<Renderer>().material=target_material;
            BlockOpen();
            Invoke("FreeOpen", delay_time);
        }
    }
// &&GameManager.Instance.getPresentGamePhaseName()=="AfterProcess"
    public void ChangeMaterial(SelectEnterEventArgs args){
        if(door.transform.position.y<0.5f){
            target.GetComponent<Renderer>().material=target_material;
            BlockOpen();
            Invoke("FreeOpen", delay_time);
        }

    }
    public void BlockOpen(){
        door_button.GetComponent<MoveGlass>().enabled=false;
    }
    public void FreeOpen(){
        door_button.GetComponent<MoveGlass>().enabled=true;
    }
}
