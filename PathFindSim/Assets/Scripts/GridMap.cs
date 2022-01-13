using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionType = Slime.ActionType;
using System.Linq;

public class GridMap
{

    private int size = 0;
    private int[,] grid;
    private bool[,] gridStat;
    private List<List<ActionType>> gridDecision;

    public GridMap(int size)
    {
        this.size = size;
        init();
    }

    void init()
    {
        grid = new int[size, size];
        gridStat = new bool[size, size];
        gridDecision = Enumerable.Range(0, size).Select(i => Enumerable.Repeat(ActionType.CENTER, size).ToList()).ToList();
        SetGridStatsOn();
    }

    public void ToggleGridStat(int x,int y)
    {
        gridStat[x, y] = !gridStat[x, y];
    }

    void SetGridStatsOn()
    {
        for(int i = 0; i < size; i++)
        {
            for(int j=0;j<size;j++)
            {
                gridStat[i, j] = true;
            }
        }
    }

    void SetGridStatsOff()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                gridStat[i, j] = false;
            }
        }
    }

    void SetGridValue(int x,int y,int value)
    {
        grid[x, y] = value;
    }

    public void SetGridDecision(int positionX,int positionY, ActionType bestAction)
    {
        gridDecision[positionX][positionY] = bestAction;
    }

    public bool[,] GetGridStat()
    {
        return gridStat;
    }

    public int[,] GetGrid()
    {
        return grid;
    }
    public int GetSize()
    {
        return size;
    }
}
