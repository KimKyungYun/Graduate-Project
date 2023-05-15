using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public GameObject XRPlayer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchSceneToScifi()
    {
        SceneManager.LoadScene("sci-fi");

        Vector3 respwanPosition = new Vector3(-10.4f, 0.1f, -15f);
        XRPlayer.transform.position = respwanPosition;
    }

    //public void SwitchScene(SelectEnterEventArgs args)
    //{
    //    Scene scene = SceneManager.GetActiveScene();
    //    if (scene.name == "Office")
    //    {
    //        SceneManager.LoadScene("sci-fi");
    //    }
    //    else if (scene.name == "sci-fi")
    //    {
    //        SceneManager.LoadScene("Office");
    //    }
    //}
}
