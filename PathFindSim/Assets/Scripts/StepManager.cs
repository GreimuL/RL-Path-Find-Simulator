using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ActionType = Slime.ActionType;

public class StepManager : MonoBehaviour
{

    public Slime slime;
    public TMP_InputField episodeCountInput;
    int defaultEpisodeCount = 1000;
    int episodeCount;

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
        if (ManagerGroup.GetGridMapMgr().IsFinish())
        {
            return false;
        }
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

        return true;
    }
    public void NextStepButton()
    {
        ManagerGroup.GetGridUIMgr().SetEpsText(slime.GetEps());
        if (!NextStep())
        {
            ManagerGroup.GetGridMapMgr().ResetPosition();
            slime.Annealing();
        }
    }
    public void PlayEpisode()
    {
        ManagerGroup.GetGridUIMgr().ActivatePausePanel();
        if (!int.TryParse(episodeCountInput.text, out episodeCount))
        {
            episodeCount = defaultEpisodeCount;
        }
        ManagerGroup.GetGridUIMgr().SetEpsText(slime.GetEps());
        StartCoroutine("StartEpisodeCoroutine");
        ManagerGroup.GetGridMapMgr().ResetPosition();
        ManagerGroup.GetGridUIMgr().SetEpsText(slime.GetEps());

    }

    IEnumerator StartEpisodeCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < episodeCount; i++)
        {
            ManagerGroup.GetGridMapMgr().ResetPosition();
            while (NextStep()) { }
            slime.Annealing();
            ManagerGroup.GetGridUIMgr().SetProgressText(i + 1, episodeCount);
            ManagerGroup.GetGridUIMgr().SetEpsText(slime.GetEps());
            yield return null;
        }
        ManagerGroup.GetGridUIMgr().DeActivatePausePanel();
    }
}
