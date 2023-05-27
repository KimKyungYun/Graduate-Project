using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    static public EffectManager Instance;

    public GameObject MainCanvas;
    public GameObject EffectCanvas;
    public GameObject IrregularCircle;
    public GameObject questionComponents;
    public Text failText;

    private void Awake()
    {
        makeSingleTon();
    }

    private void Start()
    {
        IrregularCircle.SetActive(false);
        failText.enabled = false;
        EffectCanvas.SetActive(false);
    }

    public void showCorrectAnswerCircle()
    {
        StartCoroutine(_showCorrectAnswerCircle());
    }

    private IEnumerator _showCorrectAnswerCircle()
    {
        IrregularCircle.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        IrregularCircle.SetActive(false);
    }

    public void showFail()
    {
        showFailText();
        hideComponents();
    }

    public void deactivateMainCanvas()
    {
        MainCanvas.SetActive(false);
    }

    public void activeEffectCanvas()
    {
        EffectCanvas.SetActive(true);
    }



    private void showFailText()
    {
        failText.enabled=true;
    }

    private void hideComponents()
    {
        questionComponents.SetActive(false);
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
