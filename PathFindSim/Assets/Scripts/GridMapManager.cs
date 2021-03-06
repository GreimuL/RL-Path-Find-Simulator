using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ActionType = Slime.ActionType;

public class GridMapManager : MonoBehaviour
{
    int defaultSize = 5;
    int gridSizeX = 0;
    int gridSizeY = 0;
    public int gridCellSize = 50;
    private GridMap gridMap;
    private GameObject[,] gridButtonArray;
    public GridLayoutGroup gridContainer;
    public GameObject gridInstance;
    public TMP_InputField gridSizeXText;
    public TMP_InputField gridSizeYText;
    public TMP_InputField epsInputField;
    public Slime slime;

    public GridRenderer gridRenderer;

    void Start()
    {

    }

    void Update()
    {
        
    }
    public void CreateGridMap()
    {
        if(!int.TryParse(gridSizeXText.text, out gridSizeX))
        {
            gridSizeX = defaultSize;
        }
        if(!int.TryParse(gridSizeYText.text, out gridSizeY))
        {
            gridSizeY = defaultSize;
        }

        gridMap = new GridMap(gridSizeX, gridSizeY);
        slime.ActionTableInit();
        gridButtonArray = new GameObject[gridSizeX, gridSizeY];
        gridContainer.constraintCount = gridSizeX;
        gridContainer.cellSize = new Vector2(gridCellSize, gridCellSize);
        ShowGrid();
        gridRenderer.init();
    }
    public void ShowGrid()
    {
        bool[,] currentGridStat = gridMap.GetGridStat();

        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                GameObject button = Instantiate(gridInstance, gridContainer.transform);
                button.GetComponent<GridTileButton>().SetPosition(i, j);
                gridButtonArray[i, j] = button;
            }
        }
    }


    public GridMap GetGridMap()
    {
        return gridMap;
    }

    public void ToggleGridStat(int x, int y)
    {
        gridMap.ToggleGridStat(x,y);
    }

    public void UpdateGridValue(float[,] actionValueTable)
    {
        gridRenderer.UpdateGridValue(actionValueTable);
    }
    public void UpdateGridDirection(int i,int j, ActionType action)
    {
        gridRenderer.UpdateGridDirection(i, j, action);
    }

    public System.Tuple<int,int> GetCurrentState()
    {
        return gridMap.GetCurrentState();
    }
    public void MoveSlime(ActionType action)
    {
        gridMap.MoveSlime(action);
    }
    public bool IsFinish()
    {
        return gridMap.IsFinish();
    }
    public void ResetPosition()
    {
        gridMap.ResetPosition();
        slime.ResetPosition();
    }
    public void ResetAll()
    {
        ResetPosition();
        foreach(Transform child in gridContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void SetEps()
    {
        float eps;
        if(!float.TryParse(epsInputField.text, out eps))
        {
            eps = 0;
        }
        slime.SetEps(eps);
        ManagerGroup.GetGridUIMgr().SetEpsText(slime.GetEps());
    }
}
