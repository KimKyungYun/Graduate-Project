using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpCanvasToVR : MonoBehaviour
{
    private void Awake()
    {
        Canvas canvas = gameObject.GetComponent<Canvas>();

        if (canvas.renderMode == RenderMode.WorldSpace)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            GameObject camera = GameObject.FindWithTag("MainCamera");

            rectTransform.position = Vector3.zero;
            rectTransform.localScale = new Vector3(0.01f, 0.01f, 0.01f);


        }

    }
}
