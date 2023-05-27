using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOption : MonoBehaviour
{
    public Transform answerSlot;

    public void moveToAnswerSlot()
    {
        GcodeManager gcodeManager = GcodeManager.Instance;

        if (gcodeManager.GetSelectedOption() != null)
        {
            gcodeManager.presentSlotToOriginal();
        }


        gcodeManager.setSelectedOption(gameObject);
        gcodeManager.setOriginalPos(gameObject.transform.position);

        GetComponent<RectTransform>().position = answerSlot.position;
    }
}
