using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Credit to JOHNNYTURBO from https://pastebin.com/u/JohnnyTurbo
Date: August 14, 2020
GridDirection code from https://pastebin.com/8Y1JrsBM
*/

public class GridSystem : MonoBehaviour
{
    public Vector2Int gridSize;
    public float cellRadius = 0.5f;
    public FlowField curFlowField;
    public VisualizeGrid gridDebug;

    private void InitializeFlowField()
    {
        curFlowField = new FlowField(cellRadius, gridSize);
        curFlowField.CreateGrid();
        gridDebug.SetFlowField(curFlowField);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InitializeFlowField();

            curFlowField.CreateCostField();

            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            CellSystem destinationCell = curFlowField.GetCellFromWorldPos(worldMousePos);
            curFlowField.CreateIntegrationField(destinationCell);

            curFlowField.CreateFlowField();

            gridDebug.DrawFlowField();
        }
    }
}