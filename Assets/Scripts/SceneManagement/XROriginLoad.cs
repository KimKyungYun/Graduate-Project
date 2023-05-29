using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XROriginLoad : MonoBehaviour
{
    private void Awake()
    {
        if(GameObject.Find("MainMenuXROrigin") != null)
        {
            Destroy(gameObject);
        }
    }
}
