using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static public SoundManager Instance;

    public AudioClip correctAnswerSound;
    public AudioClip wrongAnswerSound;
    public AudioClip failSound;
    public AudioClip AllClearSound;

    private AudioSource audioSource;
    private void Awake()
    {
        makeSingleTon();
        audioSource = GetComponent<AudioSource>();

    }
    public void playCorrectAnswerSound()
    {
        audioSource.PlayOneShot(correctAnswerSound);
    }

    public void playWrongAnswerSound ()
    {
        audioSource.PlayOneShot(wrongAnswerSound);
    }

    public void playFailSound()
    {
        audioSource.PlayOneShot(failSound);
    }

    public void playAllClearSound()
    {
        GameObject origin = GameObject.Find("GcodeOrigin");
        origin.GetComponentInChildren<AudioSource>().enabled = false;

        //Camera.main.GetComponent<AudioSource>().enabled= false;
        audioSource.PlayOneShot(AllClearSound);
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