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

    public bool NextStep()
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

        Debug.Log(action);
        Debug.Log(maxDirection);

        if (ManagerGroup.GetGridMapMgr().IsFinish())
        {
            return false;
        }
        return true;
    }
    public void NextStepButton()
    {
        ManagerGroup.GetGridUIMgr().SetEpsText(slime.GetEps());
        if (!NextStep())
        {
            ManagerGroup.GetGridMapMgr().ResetPosition();
            slime.annealing();
        }
    }
    public void PlayEpisode()
    {
        ManagerGroup.GetGridUIMgr().SetEpsText(slime.GetEps());
        for (int i = 0; i < 1000; i++)
        {
            ManagerGroup.GetGridMapMgr().ResetPosition();
            while (NextStep()) { }
            slime.annealing();
        }
        ManagerGroup.GetGridMapMgr().ResetPosition();
        ManagerGroup.GetGridUIMgr().SetEpsText(slime.GetEps());

    }
}
