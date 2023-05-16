using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerInputManager : MonoBehaviour
{
    static public ControllerInputManager Instance;

    public XRController leftHand;
    public XRController rightHand;
    private InputHelpers.Button PrimaryButton = InputHelpers.Button.PrimaryButton;

    private void Awake()
    {
        makeSingleTon();
    }

    public bool PrimaryButtonPressed()
    {
        return (RightPrimaryButtonPressed() || LeftPrimaryButtonPressed());
    }

    public bool RightPrimaryButtonPressed()
    {
        bool pressed;
        rightHand.inputDevice.IsPressed(PrimaryButton, out pressed);
        return pressed;
    }

    public bool LeftPrimaryButtonPressed()
    {
        bool pressed;
        leftHand.inputDevice.IsPressed(PrimaryButton, out pressed);
        return pressed;
    }
    private void makeSingleTon()
    {
        if (Instance != null) Destroy(this.gameObject);
        else Instance = this;
    }
}
