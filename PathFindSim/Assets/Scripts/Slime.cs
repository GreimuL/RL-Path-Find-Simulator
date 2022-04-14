using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Slime : MonoBehaviour
{
    public enum ActionType { UP, LEFT, DOWN, RIGHT, CENTER = -1 };

    private float[,,] actionValueTable;
    private GridMap gridMap;
    float eps;
    int actionNumber;
    int reward;
    float alpha;
    bool isSetEps;

    // Start is called before the first frame update
    void Start()
    {
        /*
        isSetEps = false;
        actionNumber = 4;
        reward = -1;
        alpha = 0.1f;
        eps = 0.9f;
        */
    }

    public void ActionTableInit()
    {
        isSetEps = false;
        actionNumber = 4;
        reward = -1;
        alpha = 0.1f;
        eps = 0.9f;
        gridMap = ManagerGroup.GetGridMapMgr().GetGridMap();
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

    public ActionType ActionDecision(int currentPositionX, int currentPositionY)
    {
        float randomValue = Random.value;

        if (randomValue < eps)
        {
            Debug.Log("Random Action");
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

    public void ActionTableUpdate(int currentPositionX, int currentPositionY, ActionType selectedAction, int nextPositionX, int nextPositionY)
    {
        List<float> nextActionValueList = Enumerable.Range(0, actionNumber).Select(i => actionValueTable[nextPositionX, nextPositionY, i]).ToList();
        float maxActionValue = nextActionValueList.Max();
        float currentSelectedActionValue = actionValueTable[currentPositionX, currentPositionY, (int)selectedAction];
        actionValueTable[currentPositionX, currentPositionY, (int)selectedAction] += (reward + maxActionValue - currentSelectedActionValue) * alpha;

        //Debug.Log(selectedAction);
        transform.position = new Vector3(nextPositionX * 5, 1, nextPositionY * 5);
    }

    public ActionType FindMaxValueDirection(int currentPositionX,int currentPositionY)
    {
        float maxValue = -0x3f3f3f3f;
        ActionType direction = 0;
        for(int i = 0; i < actionNumber; i++)
        {
            float value = actionValueTable[currentPositionX, currentPositionY, i];
            if (maxValue < value)
            {
                maxValue = value;
                direction = (ActionType)i;
            }
        }
        return direction;
    }

    public void Annealing()
    {
        if (!isSetEps)
        {
            eps = Mathf.Max(eps - 0.03f, 0.1f);
        }
    }
    public float GetEps()
    {
        return eps;
    }
    public void SetEps(float value)
    {
        isSetEps = true;
        eps = value;
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(0, 1, 0);
    }
}
