using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        deleteDontDestroy();
    }

    // Update is called once per frame
    private void deleteDontDestroy()
    {
        DontDestroy[] dontDestroys = FindObjectsOfType<DontDestroy>();

        foreach(DontDestroy dontDestory in dontDestroys)
        {
            dontDestory.gameObject.GetComponent<DontDestroy>().enabled=false;
        }
    }
}
