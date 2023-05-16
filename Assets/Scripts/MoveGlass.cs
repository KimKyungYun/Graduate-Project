using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveGlass : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject glass;
    public int degree=2;
    public bool isOpen=false;

    public void ButtonClick (SelectEnterEventArgs args){
        if(isOpen){
            isOpen=false;
        }
        else{
            isOpen=true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(glass.transform.position.y<degree&&!isOpen){
            glass.transform.Translate(Vector3.up * Time.deltaTime);
        }
        else if(glass.transform.position.y>=0&&isOpen){
            glass.transform.Translate(Vector3.down * Time.deltaTime);
        }
    }
}
