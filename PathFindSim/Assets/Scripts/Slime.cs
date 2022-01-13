using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Slime : MonoBehaviour
{
    public enum ActionType { LEFT, UP, RIGHT, DOWN, CENTER = -1 };

    private float[,,] actionValueTable;
    private bool[,] gridStat;
    public GridMap gridMap;
    float eps;
    int actionNumber;
    int reward;
    float alpha;

    // Start is called before the first frame update
    void Start()
    {
        gridStat = gridMap.GetGridStat();
        actionNumber = 4;
        reward = -1;
        alpha = 0.1f;
        eps = 0.9f;
        actionValueTable = new float[gridMap.GetSize(),gridMap.GetSize(),actionNumber];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ActionType ActionDecision(int currentPositionX, int currentPositionY)
    {
        float randomValue = Random.value;

        if (randomValue < eps)
        {
            return (ActionType)Random.Range(0, actionNumber);
        }
        else
        {
            List<float> currentActionValueList = Enumerable.Range(0, actionNumber).Select(i => actionValueTable[currentPositionX, currentPositionY, i]).ToList();
            float maxActionValue = currentActionValueList.Max();
            float maxActionIndex = currentActionValueList.IndexOf(maxActionValue);
            return (ActionType)maxActionIndex;
        }
    }

    void ActionTableUpdate(int currentPositionX, int currentPositionY, ActionType selectedAction, int nextPositionX, int nextPositionY)
    {
        List<float> nextActionValueList = Enumerable.Range(0, actionNumber).Select(i => actionValueTable[nextPositionX, nextPositionY, i]).ToList();
        float maxActionValue = nextActionValueList.Max();
        float currentSelectedActionValue = actionValueTable[currentPositionX, currentPositionY, (int)selectedAction];
        actionValueTable[currentPositionX, currentPositionY, (int)selectedAction] += (reward + maxActionValue - currentSelectedActionValue) * alpha;
    }
}
