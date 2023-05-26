using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]

public class ProgressBarCircle : MonoBehaviour {
    [Header("Title Setting")]
    public string Title;
    public Color TitleColor;
    public Font TitleFont;


    [Header("Bar Setting")]
    public Color BarColor;
    public Color BarBackGroundColor;
    public Color MaskColor;
    public Sprite BarBackGroundSprite;
    [Range(1f, 100f)]
    public int Alert = 20;
    public Color BarAlertColor;
    public float gage;

    [Header("Sound Alert")]
    public AudioClip sound;
    public bool repeat = false;
    public float RepearRate = 1f;

    private Image bar, barBackground,Mask;
    private float nextPlay;
    private AudioSource audiosource;
    private Text txtTitle;
    private float barValue;
    public float BarValue
    {
        get { return barValue; }

        set
        {
            value = Mathf.Clamp(value, 0, 100);
            barValue = value;
            UpdateValue(barValue);

        }
    }

    private void Awake()
    {
        txtTitle = transform.Find("Text").GetComponent<Text>();
        barBackground = transform.Find("BarBackgroundCircle").GetComponent<Image>();
        bar = transform.Find("BarCircle").GetComponent<Image>();
        audiosource = GetComponent<AudioSource>();
        Mask= transform.Find("Mask").GetComponent<Image>();
    }

    private void Start()
    {
        Time.timeScale = 1f;

        txtTitle.text = Title;
        txtTitle.color = TitleColor;
        txtTitle.font = TitleFont;

        gage = (float)100.0; // 게이지

        bar.color = BarColor;
        Mask.color = MaskColor;
        barBackground.color = BarBackGroundColor;
        barBackground.sprite = BarBackGroundSprite;

        UpdateValue(100);
        txtTitle.color = new Color(0f, 0.5f, 0.5f, 1.0f);

    }

    public int UpdateValue(float val)
    {
        if (val == -1)
        {
            gage -= (float)25.0;

            if (gage == 0) // 실패했을때
            {
                Debug.Log("실패");
                return 0;
            }

            bar.fillAmount = -(gage / 100) + 1f;
            txtTitle.text = Title + " " + gage + "%";

            Color bgColor = new Color(1.0f, 0f, 0f, 1f - gage / 100f);
            //Color backgroundColor = new Color(1.0f, 0f, 0f, 1.0f);
            barBackground.color = bgColor;
            txtTitle.color = bgColor;

            return 1;
        }
        else
        {
            bar.fillAmount = -(val / 100) + 1f;

            txtTitle.text = Title + " " + val + "%";

            Color backgroundColor = new Color(1.0f, 0f, 0f, 1f - val / 100f);
            //Color backgroundColor = new Color(1.0f, 0f, 0f, 1.0f);
            barBackground.color = backgroundColor;
            txtTitle.color = backgroundColor;

            return 1;
        }
    }


    private void Update()
    {
        if (!Application.isPlaying)
        {
            // 에디터에서 실행 중인 경우에만 실행
            return;
        }

    }

}
