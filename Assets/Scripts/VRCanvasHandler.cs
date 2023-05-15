using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VRCanvasHandler : MonoBehaviour
{
    static public VRCanvasHandler Instance;
    private void Awake()
    {
        makeSingleTon();
        downSizeCanvas();
        initCanvasPosition();
        //displayFrontOfPlayer(3.7f);
    }

    public void displayOnObject(GameObject displayObject, GameObject targetObject, float distanceY)
    {
        Vector3 target_pos = targetObject.transform.position;
        displayObject.GetComponent<RectTransform>().position = new Vector3(target_pos.x, target_pos.y + distanceY , target_pos.z);

        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        float cameraRotationY = mainCamera.transform.eulerAngles.y;

        displayObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, cameraRotationY, 0);
    }

    private void initCanvasPosition()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.position = Vector3.zero;
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
            Instance = this;
        }
    }





    //public void displayOnObject(GameObject gameObject, float distanceY)
    //{
    //    //게임오브젝트의 transform 좌표 얻어온 후에 Canvas의 좌표를 해당좌표의 약간 위로 이동

    //    Vector3 go_pos = gameObject.transform.position;
    //    GetComponent<RectTransform>().position = new Vector3(go_pos.x, go_pos.y + distanceY, go_pos.z);

    //    //XR Origin의 Rotation 값을 받아서 Canvas의 Rotation값에 할당
    //    GameObject mainCamera = GameObject.FindWithTag("MainCamera");
    //    float cameraRotationY = mainCamera.transform.eulerAngles.y;

    //    GetComponent<RectTransform>().eulerAngles = new Vector3(0, cameraRotationY, 0);
    //}





    //public void displayFrontOfPlayer(float distanceZ)
    //{
    //    RectTransform rectTransform = GetComponent<RectTransform>();
    //    GameObject camera = GameObject.FindWithTag("MainCamera");
    //    Vector3 cameraVector = camera.transform.position;
    //    rectTransform.position = new Vector3(cameraVector.x, cameraVector.y, cameraVector.z + distanceZ);
    //}
}
