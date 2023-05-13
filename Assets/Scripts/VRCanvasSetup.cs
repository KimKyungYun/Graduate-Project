using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VRCanvasSetup : MonoBehaviour
{
    static public VRCanvasSetup Instance;

    private void Awake()
    {
        makeSingleTon();
        Canvas canvas = gameObject.GetComponent<Canvas>();

        if (canvas.renderMode == RenderMode.WorldSpace)
        {
            downSizeCanvas();
            displayFrontOfPlayer();
        }
    }

    public void displayFrontOfPlayer()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        GameObject camera = GameObject.FindWithTag("MainCamera");
        Vector3 cameraVector = camera.transform.position;
        rectTransform.position = new Vector3(cameraVector.x, cameraVector.y, cameraVector.z + 4.0f);
    }

    private void downSizeCanvas()
    {
        GetComponent<RectTransform>().localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }


    public void placeOnObject()
    {

    }

    private void makeSingleTon()
    {
        if (Instance != null) Destroy(this.gameObject);
        else
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
    }
}
