using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeFlowField : MonoBehaviour
{
    public int cellWidth;
    public int cellHeight;
    public float cellSpacing;
    public List<int> cellCost;

    //private GridSystem gridSystem;

    private void Start()
    {
        cellWidth = 18;
        cellHeight = 10;
        cellSpacing = 1.0f;

        //gridSystem = new GridSystem(cellWidth, cellHeight);

        calculateCostField();
        visualizeField();
    }

    void calculateCostField()
    {
        for(int i = 0; i < cellWidth; i++)
        {
            for(int j = 0; j < cellHeight; j++)
            {
                if (i % 5 == 0)
                {
                    cellCost.Add(255);
                }
                else if (j % 2 == 0)
                {
                    cellCost.Add(Random.Range(2, 255));
                }
                else
                {
                    cellCost.Add(1);
                }
            }
        }
    }

    void calculateIntegrationField()
    {

    }

    void generateFlowField()
    {

    }

    void visualizeField()
    {
        
    }

    private void GetNeighbor(int index)
    {
        
    }

    private void OnDrawGizmos()
    {
        Vector3 cellCenter = this.transform.position - new Vector3(cellWidth * 0.5f, cellHeight * 0.5f);
        Vector3 startPos = cellCenter;
        Vector3 cellSize = new Vector3(cellSpacing, cellSpacing);
        cellCenter += cellSize * 0.5f;
        int index = 0;
        for (int i = 0; i < cellWidth; i++)
        {
            for (int j = 0; j < cellHeight; j++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(cellCenter, cellSize);
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(cellCenter, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                cellCenter += new Vector3(0.0f, cellSpacing);
                index++;
            }
            cellCenter.y = startPos.y + cellSpacing * 0.5f;
            cellCenter += new Vector3(cellSpacing, 0.0f);
        }
    }
}
