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
    private InputHelpers.Button SecondaryButton = InputHelpers.Button.SecondaryButton;

    private void Awake()
    {
        makeSingleTon();
    }

    public bool PrimaryButtonPressed()
    {
        return (RightPrimaryButtonPressed() || LeftPrimaryButtonPressed());
    }

    public bool SecondaryButtonPressed()
    {
        return (RightSecondaryButtonPressed() || LeftSecondaryButtonPressed());
    }

    public bool LeftJoystickControlled()
    {
        Vector2 vector2 = new Vector2(0, 0);


        InputHelpers.Axis2D leftJoystick = InputHelpers.Axis2D.PrimaryAxis2D;

        leftHand.inputDevice.TryReadAxis2DValue(leftJoystick, out vector2);


        return !vector2.Equals(Vector2.zero);
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
    public bool RightSecondaryButtonPressed()
    {
        bool pressed;
        rightHand.inputDevice.IsPressed(SecondaryButton, out pressed);
        return pressed;
    }
    public bool LeftSecondaryButtonPressed()
    {
        bool pressed;
        leftHand.inputDevice.IsPressed(SecondaryButton, out pressed);
        return pressed;
    }

    private void makeSingleTon()
    {
        if (Instance != null) Destroy(this.gameObject);
        else Instance = this;
    }
}
