using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ActionType = Slime.ActionType;
public class ValuePlane : MonoBehaviour
{

    public TMP_Text planeText;

    public void SetPlaneValue(float value)
    {
        planeText.text = string.Format("{0:0.00}", value);
    }

    public void SetPlaneDirection(ActionType action)
    {
        switch (action)
        {
            case ActionType.LEFT:
                planeText.text = "��";
                break;
            case ActionType.UP:
                planeText.text = "��";
                break;
            case ActionType.RIGHT:
                planeText.text = "��";
                break;
            case ActionType.DOWN:
                planeText.text = "��";
                break;
            case ActionType.CENTER:
                planeText.text = "��";
                break;
        }
    }
}
