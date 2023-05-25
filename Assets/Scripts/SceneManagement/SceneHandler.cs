using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class SceneHandler : MonoBehaviour
{
    public GameObject XRPlayer;
    public GameObject fadeBox;

    [Range(0.01f, 10f)]
    public float fadeTime;

    private Image image;
    private bool SceneLoading = false;
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
        GameObject[] xRController = GameObject.FindGameObjectsWithTag("GameController");
        //컨트롤러 비활성화
        foreach (GameObject gameObject in xRController)
        {
            gameObject.GetComponent<XRController>().enabled = false;
        }
        
        StartCoroutine(fade(0, 1, fadeTime));
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("sci-fi");
        yield return new WaitForSeconds(1.0f);

        //기존 리스폰 위치 위치 
        //Vector3 respwanPosition = new Vector3(-10.4f, 1.284f, -15f

        //디버깅용 리스폰
        Vector3 respwanPosition = new Vector3(0.32f, -1.078f, -2.58f);
        XRPlayer.transform.position = respwanPosition;
        StartCoroutine(fade(1, 0, fadeTime));

        //컨트롤러 활성화
        foreach (GameObject gameObject in xRController)
        {
            gameObject.GetComponent<XRController>().enabled = true;
        }
        SceneLoading = false;
    }

    private IEnumerator fade(float start, float end, float fadeTime)
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

}
