using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerInputManager : MonoBehaviour
{
    public XRController leftHand;
    public XRController rightHand;
    private InputHelpers.Button PrimaryButton = InputHelpers.Button.PrimaryButton;

    void Update()
    {
        if (PrimaryButtonPressed())
        {
            Debug.Log("Hello - " + PrimaryButton);

            MiniQuestBoxManager.Instance.displayQuestBox();
        }
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


}
