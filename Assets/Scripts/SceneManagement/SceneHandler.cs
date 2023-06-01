using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using Unity.XR.CoreUtils;

public class SceneHandler : MonoBehaviour
{
    static public SceneHandler Instance;

    public GameObject XRPlayer;
    public GameObject fadeBox;

    [Range(0.01f, 10f)]
    public float fadeTime;

    private Image image;
    private bool SceneLoading = false;
    private Vector3 posBeforeSceneLoad;

    private void Awake()
    {
        makeSingleTon();
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Start()
    {
        image = fadeBox.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
    }

    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.M) )
        {
            SwitchSceneToScifi();
        }


        if (SceneLoading)
        {
            VRCanvasHandler.Instance.displayFrontOfPlayer(fadeBox, 0.5f);
        }
    }
    public void SwitchSceneToScifi()
    {
        StartCoroutine(_SwitchSceneToScifi());
    }
    public IEnumerator _SwitchSceneToScifi()
    {
        SceneLoading = true;
        ActivateController(false);

        StartCoroutine(fade(0, 1, fadeTime));
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("sci-fi");
        yield return new WaitForSeconds(1.0f);

        //기존 리스폰 위치 위치
        Vector3 respwanPosition = new Vector3(-10.4f, 1.284f, -15f);

        //디버깅용 리스폰
        //Vector3 respwanPosition = new Vector3(0.32f, -1.078f, -2.58f);
        XRPlayer.transform.position = respwanPosition;
        StartCoroutine(fade(1, 0, fadeTime));

        ActivateController(true);
        yield return new WaitForSeconds(0.3f);
        SceneLoading = false;

        yield return new WaitForSeconds(5.0f);
        fadeBox.SetActive(false);
    }

    public void SwitchGcodeToScifi()
    {
        StartCoroutine(_SwitchGcodeToScifi());
    }

    private IEnumerator _SwitchGcodeToScifi()
    {
        Vector3 originalPos = posBeforeSceneLoad;
        Vector3 respawnPos = new Vector3(originalPos.x, originalPos.y + 0.5f, originalPos.z);
        XRPlayer.transform.position = respawnPos;

        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("sci-fi");
        yield return new WaitForSeconds(1.0f);

        ActivateController(true);
    }

    public void switchScifiToGcode(Vector3 originalPos)
    {
        posBeforeSceneLoad = originalPos;
        ActivateController(false);
        SceneManager.LoadScene("MyGcode");
    }

    private IEnumerator fade(float start, float end, float fadTeime)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1.00f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;
        }
    }
    public void ActivateController(bool status)
    {
        XRPlayer.GetComponent<DeviceBasedContinuousMoveProvider>().enabled = status;
        XRPlayer.GetComponent<DeviceBasedSnapTurnProvider>().enabled = status;

        GameObject[] xRController = GameObject.FindGameObjectsWithTag("GameController");
        foreach (GameObject gameObject in xRController)
        {
            gameObject.GetComponent<XRController>().enabled = status;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ActivateController(true);
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