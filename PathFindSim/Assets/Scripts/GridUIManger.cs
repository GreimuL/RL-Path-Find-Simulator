using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GridUIManger : MonoBehaviour
{
    public GridLayoutGroup gridContainer;
    public Slider GridUISizeSlider;
    public TMP_Text epsText;
    public TMP_Text progressText;
    public GameObject pausePanel;

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

    public void SetEpsText(float value)
    {
        epsText.text = "EPS: " + string.Format("{0:0.00}", value);
    }
    public void ActivatePausePanel()
    {
        pausePanel.SetActive(true);
    }
    public void DeActivatePausePanel()
    {
        pausePanel.SetActive(false);
    }
    public void SetProgressText(int progressCount,int maxCount)
    {
        progressText.text = progressCount + " / " + maxCount;
    }

}
