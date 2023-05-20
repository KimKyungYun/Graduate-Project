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
    void Start()
    {
        image = fadeBox.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
    }

    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.K))
        {
            SwitchSceneToScifi();
        }
    }

    public void SwitchSceneToScifi()
    {
        StartCoroutine(_SwitchSceneToScifi());
    }
    public IEnumerator _SwitchSceneToScifi()
    {
        VRCanvasHandler.Instance.displayFrontOfPlayer(fadeBox, 0.5f);
        StartCoroutine(fade(0, 1, fadeTime));
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("sci-fi");
        yield return new WaitForSeconds(1.0f);

        Vector3 respwanPosition = new Vector3(-10.4f, 1.284f, -15f);
        XRPlayer.transform.position = respwanPosition;
        VRCanvasHandler.Instance.displayFrontOfPlayer(fadeBox, 0.5f);
        StartCoroutine(fade(1, 0, fadeTime));
    }


    //public void SwitchSceneToScifi()
    //{
    //    VRCanvasHandler.Instance.displayFrontOfPlayer(fadeBox, 1f);
    //    StartCoroutine(fade(0, 1, fadeTime));



    //    //SceneManager.LoadScene("sci-fi");
    //    //Vector3 respwanPosition = new Vector3(-10.4f, 0.1f, -15f);
    //    //XRPlayer.transform.position = respwanPosition;
    //}



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
