using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void Awake()
    {
        destroy_DontDestroy();
    }


    private void destroy_DontDestroy()
    {
        DontDestroy[] dontDestroys = FindObjectsOfType<DontDestroy>();

        foreach (DontDestroy dontDestory in dontDestroys)
        {
            Destroy(dontDestory.gameObject);
        }
    }
}
