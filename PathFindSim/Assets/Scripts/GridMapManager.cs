using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridMapManager : MonoBehaviour
{

    public int gridSize = 0;
    public int gridCellSize = 50;
    private GridMap gridMap;
    private GameObject[,] gridButtonArray;
    public GridLayoutGroup gridContainer;
    public GameObject gridInstance;

    public GridRenderer gridRenderer;

    void Start()
    {
        gridMap = new GridMap(gridSize);
        gridButtonArray = new GameObject[gridSize, gridSize];
        gridContainer.constraintCount = gridSize;
        gridContainer.cellSize = new Vector2(gridCellSize,gridCellSize);
        ShowGrid();
        gridRenderer.init();
    }

    void Update()
    {
        
    }
    public void ShowGrid()
    {
        bool[,] currentGridStat = gridMap.GetGridStat();

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
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

}
