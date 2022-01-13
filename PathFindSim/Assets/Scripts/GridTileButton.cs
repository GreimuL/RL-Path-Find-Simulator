using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridTileButton : MonoBehaviour
{
    bool currentState = true;
    public Image buttonImage;
    int x, y;

    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickGridButton()
    {
        currentState = !currentState;
        if (currentState)
        {
            buttonImage.color = new Color(1 , 1, 1,1);
            ManagerGroup.GetGridMapMgr().ToggleGridStat(x, y);
        }
        else
        {
            buttonImage.color = new Color(0, 0, 0, 0.1f);
            ManagerGroup.GetGridMapMgr().ToggleGridStat(x, y);
        }
    }
    public void SetPosition(int x,int y)
    {
        this.x = x;
        this.y = y;
    }
}
