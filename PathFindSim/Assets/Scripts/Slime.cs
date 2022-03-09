using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Slime : MonoBehaviour
{
    public enum ActionType { LEFT, UP, RIGHT, DOWN, CENTER = -1 };

    private float[,,] actionValueTable;
    public GridMapManager gridMapMgr;
    private GridMap gridMap;
    float eps;
    int actionNumber;
    int reward;
    float alpha;

    // Start is called before the first frame update
    void Start()
    {
        actionNumber = 4;
        reward = -1;
        alpha = 0.1f;
        eps = 0.9f;
    }

    public void ActionTableInit()
    {
        gridMap = gridMapMgr.GetGridMap();
        actionValueTable = new float[gridMap.GetSize().Item1, gridMap.GetSize().Item2, actionNumber];
    }

    // Update is called once per frame
    void Update()
    {
        float verticalVector = Input.GetAxis("Vertical");
        float horizontalVector = Input.GetAxis("Horizontal");

        Vector3 direction = Vector3.forward * verticalVector + Vector3.right * horizontalVector;
        //transform.rotation = Quaternion.LookRotation(direction);
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
