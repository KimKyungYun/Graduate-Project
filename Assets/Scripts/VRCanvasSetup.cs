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
        downSizeCanvas();
        displayFrontOfPlayer();
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
        float distanceY = 3.7f;
        //���ӿ�����Ʈ�� transform ��ǥ ���� �Ŀ� Canvas�� ��ǥ�� �ش���ǥ�� �ణ ���� �̵�

        Vector3 go_pos = gameObject.transform.position;
        GetComponent<RectTransform>().position = new Vector3(go_pos.x, go_pos.y + distanceY, go_pos.z);

        //XR Origin�� Rotation ���� �޾Ƽ� Canvas�� Rotation���� �Ҵ�
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        float cameraRotationY = mainCamera.transform.eulerAngles.y;

        GetComponent<RectTransform>().eulerAngles = new Vector3(0, cameraRotationY, 0);
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
