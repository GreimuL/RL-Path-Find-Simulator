using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridRenderer : MonoBehaviour
{
    private GridMap gridMap;
    private bool[,] gridStat;
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
    }

    public void CreateWalls()
    {
        ClearWalls();
        for(int i = 0; i < gridMap.GetSize(); i++)
        {
            for(int j = 0; j < gridMap.GetSize(); j++)
            {
                if (gridStat[i, j] == false)
                {
                    Instantiate(wall, new Vector3(i*5,1,j*5), new Quaternion(),this.transform);
                }
                else
                {
                    Instantiate(plane, new Vector3(i * 5, 1, j * 5), new Quaternion(), this.transform);
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

}
