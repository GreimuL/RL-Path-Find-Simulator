using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionType = Slime.ActionType;
using System.Linq;

public class GridMap
{

    private int sizeX = 0;
    private int sizeY = 0;
    private int[,] grid;
    private bool[,] gridStat;
    private List<List<ActionType>> gridDecision;
    private int currentPositionX;
    private int currentPositionY;
    private int endPositionX;
    private int endPositionY;

    public GridMap(int sizeX,int sizeY)
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        init();
    }

    void init()
    {
        grid = new int[sizeX, sizeY];
        gridStat = new bool[sizeX, sizeY];
        gridDecision = Enumerable.Range(0, sizeX).Select(i => Enumerable.Repeat(ActionType.CENTER, sizeY).ToList()).ToList();
        SetGridStatsOn();
        currentPositionX = 0;
        currentPositionY = 0;
        endPositionX = sizeX - 1;
        endPositionY = sizeY - 1;
    }

    public void ToggleGridStat(int x,int y)
    {
        gridStat[x, y] = !gridStat[x, y];
    }

    void SetGridStatsOn()
    {
        for(int i = 0; i < sizeX; i++)
        {
            for(int j=0;j<sizeY;j++)
            {
                gridStat[i, j] = true;
            }
        }
    }

    void SetGridStatsOff()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
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
    public System.Tuple<int,int> GetSize()
    {
        return new System.Tuple<int,int>(sizeX,sizeY);
    }

    public void MoveSlime(ActionType selectedAction)
    {
        switch (selectedAction)
        {
            case ActionType.LEFT:
                if (currentPositionX - 1 >= 0)
                {
                    currentPositionX--;
                }
                break;
            case ActionType.UP:
                if (currentPositionY - 1 >= 0)
                {
                    currentPositionY--;
                }
                break;
            case ActionType.RIGHT:
                if (currentPositionX + 1 <sizeX)
                {
                    currentPositionX++; ;
                }
                break;
            case ActionType.DOWN:
                if (currentPositionY + 1 < sizeY)
                {
                    currentPositionY++;
                }
                break;

        }
    }

    public System.Tuple<int,int> GetCurrentState()
    {
        return new System.Tuple<int, int>(currentPositionX, currentPositionY);
    }
}
