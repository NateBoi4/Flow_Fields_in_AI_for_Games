using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeFlowField : MonoBehaviour
{
    public int cellWidth;
    public int cellHeight;
    public List<int> cellCost;

    private void Start()
    {
        cellWidth = 10;
        cellHeight = 10;
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
        Vector3 startPos = this.transform.position;
        for(int i = 0; i < cellCost.Count; i++)
        {
            Vector3 endPos = startPos + new Vector3(1.0f, 1.0f, 0.0f);
            Debug.DrawLine(startPos, endPos, Color.red, (float)cellCost[i]);
            startPos = endPos;
        }
    }
}
