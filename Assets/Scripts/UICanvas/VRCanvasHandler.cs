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
    }

    //로테이션을 카메라에 맞춤
    public void displayOnObject(GameObject displayObject, GameObject targetObject, float distanceY)
    {
        Vector3 target_pos = targetObject.transform.position;
        displayObject.GetComponent<RectTransform>().position = new Vector3(target_pos.x, target_pos.y + distanceY , target_pos.z);

        float cameraRotationY = Camera.main.transform.eulerAngles.y;

        displayObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, cameraRotationY, 0);
    }

    //로테이션을 오브젝트에 맞춤
    public void displayOnObjectRotation(GameObject displayObject, GameObject targetObject, float distanceY)
    {
        Vector3 target_pos = targetObject.transform.position;
        displayObject.GetComponent<RectTransform>().position = new Vector3(target_pos.x, target_pos.y + distanceY, target_pos.z);

        float targetRotationY = targetObject.transform.eulerAngles.y;

        displayObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, targetRotationY + 180f, 0);
    }


    public void displayFrontOfPlayer(GameObject displayObject, float distance)
    {
        float cameraRotationY = Camera.main.transform.eulerAngles.y;
        displayObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, cameraRotationY, 0);
        displayObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;

        //조정값 ( x  : -1.0f, y : -0.75f )
        displayObject.transform.position += Camera.main.transform.right * -1.0f  + Camera.main.transform.up * -0.75f;
    }

    public void displayFrontOfPlayer(GameObject displayObject, float distance, float adjustX, float adjustY)
    {
        float cameraRotationY = Camera.main.transform.eulerAngles.y;
        displayObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, cameraRotationY, 0);
        displayObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;

        //조정값
        displayObject.transform.position += Camera.main.transform.right * adjustX + Camera.main.transform.up * adjustY;
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
}
