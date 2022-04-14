using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGroup : MonoBehaviour
{
    private static object objLock = new object();
    private static ManagerGroup instance;

    public static ManagerGroup Instance
    {
        get
        {
            lock (objLock)
            {
                if (instance == null)
                {
                    instance = (ManagerGroup)FindObjectOfType(typeof(ManagerGroup));
                }
                return instance;
            }
        }
    }

    public GridMapManager gridMapMgr;
    public static GridMapManager GetGridMapMgr()
    {
        if(Instance != null)
        {
            return Instance.gridMapMgr;
        }
        return null;
    }

    public GridUIManger gridUIMgr;
    public static GridUIManger GetGridUIMgr()
    {
        if (Instance != null)
        {
            return Instance.gridUIMgr;
        }
        return null;
    }

    public StepManager stepMgr;
    public static StepManager GetStepMgr()
    {
        if (Instance != null)
        {
            return Instance.stepMgr;
        }
        return null;
    }
}
