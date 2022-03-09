using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ValuePlane : MonoBehaviour
{

    public TMP_Text planeText;

    public void SetPlaneValue(float value)
    {
        planeText.text = string.Format("{0:0.00}", value);
    }
}
