using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Color color = GetComponent<Image>().color;
        color.a = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
