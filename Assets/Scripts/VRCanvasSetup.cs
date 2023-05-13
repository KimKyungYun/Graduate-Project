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
        if (isVrMode())
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

    public void displayOnObject(GameObject gameObject)
    {
        float distanceY = 2.0f;
        //게임오브젝트의 transform 좌표 얻어온 후에 Canvas의 좌표를 해당좌표의 약간 위로 이동

        Vector3 go_pos = gameObject.transform.position;
        GetComponent<RectTransform>().position = new Vector3(go_pos.x, go_pos.y + distanceY, go_pos.z);
    }

    public bool isVrMode()
    {
        Canvas canvas = gameObject.GetComponent<Canvas>();
        return (canvas.renderMode == RenderMode.WorldSpace);
    }
    private void downSizeCanvas()
    {
        GetComponent<RectTransform>().localScale = new Vector3(0.01f, 0.01f, 0.01f);
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
