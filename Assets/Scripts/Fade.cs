using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public GameObject fadeObject;
    public bool visible=false;
    public float speed=0.5f;
    
    void Update()
    {
        if(visible){
            ChangeAlpha(fadeObject.GetComponent<Renderer>().material, Time.deltaTime*speed);
        }
        else{
            ChangeAlpha(fadeObject.GetComponent<Renderer>().material, -Time.deltaTime*speed);
        }
    }

    void ChangeAlpha(Material mat, float alphaVal)
    {
        Color oldColor = mat.color;
        float alpha=oldColor.a+alphaVal<1?oldColor.a+alphaVal<=0?0:oldColor.a+alphaVal:1;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alpha);
        mat.SetColor("_Color", newColor);
    }
}