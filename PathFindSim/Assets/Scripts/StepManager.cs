using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionType = Slime.ActionType;

public class StepManager : MonoBehaviour
{

    public Slime slime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextStep()
    {
        System.Tuple<int, int> currentState = ManagerGroup.GetGridMapMgr().GetCurrentState();
        int currentX = currentState.Item1;
        int currentY = currentState.Item2;

        ActionType action = slime.ActionDecision(currentX, currentY);
        ManagerGroup.GetGridMapMgr().MoveSlime(action);

        System.Tuple<int, int> nextState = ManagerGroup.GetGridMapMgr().GetCurrentState();
        int nextX = nextState.Item1;
        int nextY = nextState.Item2;

        slime.ActionTableUpdate(currentX, currentY, action, nextX, nextY);
        ActionType maxDirection = slime.FindMaxValueDirection(currentX, currentY);
        ManagerGroup.GetGridMapMgr().UpdateGridDirection(currentX,currentY,maxDirection);
    }
}
