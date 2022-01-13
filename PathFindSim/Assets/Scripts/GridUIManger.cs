using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GridUIManger : MonoBehaviour
{
    public GridLayoutGroup gridContainer;
    public Slider GridUISizeSlider;

    // Start is called before the first frame update
    void Start()
    {      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float SliderValueToGridSize(float value)
    {
        return value * 100 + 10;
    }

    public void OnGridUISizeSliderChanged()
    {
        gridContainer.cellSize = new Vector2(SliderValueToGridSize(GridUISizeSlider.value), SliderValueToGridSize(GridUISizeSlider.value));
    }
}
