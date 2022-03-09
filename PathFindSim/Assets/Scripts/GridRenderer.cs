using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridRenderer : MonoBehaviour
{
    private GridMap gridMap;
    private bool[,] gridStat;
    private ValuePlane[,] planeArray;
    public GameObject wall;
    public GameObject plane;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void init()
    {
        gridMap = ManagerGroup.GetGridMapMgr().GetGridMap();
        gridStat = gridMap.GetGridStat();
        planeArray = new ValuePlane[gridMap.GetSize().Item1, gridMap.GetSize().Item2];
    }

    public void CreateWalls()
    {
        ClearWalls();
        for(int i = 0; i < gridMap.GetSize().Item1; i++)
        {
            for(int j = 0; j < gridMap.GetSize().Item2; j++)
            {
                if (!gridStat[i, j])
                {
                    Instantiate(wall, new Vector3(i*GlobalConstants.worldRatio,1,j* GlobalConstants.worldRatio), new Quaternion(),this.transform);
                }
                else
                {
                    GameObject tempPlane = Instantiate(plane, new Vector3(i * GlobalConstants.worldRatio, 1, j * GlobalConstants.worldRatio), new Quaternion(), this.transform);
                    planeArray[i, j] = tempPlane.GetComponent<ValuePlane>();
                }
            }
        }
    }

    void ClearWalls()
    {
        foreach(Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void UpdateGridValue(float[,] actionValueTable)
    {
        for(int i = 0; i < gridMap.GetSize().Item1; i++)
        {
            for (int j = 0; j < gridMap.GetSize().Item2; j++)
            {
                if (!gridStat[i, j])
                {
                    continue;
                }
                planeArray[i, j].SetPlaneValue(actionValueTable[i, j]);
            }
        }
    }

}
