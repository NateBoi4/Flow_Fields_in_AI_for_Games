using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Credit to JOHNNYTURBO from https://pastebin.com/u/JohnnyTurbo
Date: August 14, 2020
GridDirection code from https://pastebin.com/Je907W7f
*/

public class UnitManager : MonoBehaviour
{
    public GridSystem gridController;
    public GameObject unitPrefab;
    public int numUnitsPerSpawn;
    public float moveSpeed;

    public Transform parentUnit;

    private List<GameObject> unitsInGame;

    public float spawnTime = 5.0f;

    private void Awake()
    {
        unitsInGame = new List<GameObject>();
    }

    private void Start()
    {
        InvokeRepeating("SpawnUnits", spawnTime, spawnTime);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnUnits();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DestroyUnits();
        }
    }

    private void FixedUpdate()
    {
        if (gridController.curFlowField == null) { return; }
        foreach (GameObject unit in unitsInGame)
        {
            if (unit)
            {
                CellSystem cellBelow = gridController.curFlowField.GetCellFromWorldPos(unit.transform.position);
                Vector3 moveDirection = new Vector3(cellBelow.bestDirection.Vector.x, 0, cellBelow.bestDirection.Vector.y);
                Rigidbody unitRB = unit.GetComponent<Rigidbody>();
                unitRB.velocity = moveDirection * moveSpeed;
            }
        }
    }

    private void SpawnUnits()
    {
        Vector2Int gridSize = gridController.gridSize;
        float nodeRadius = gridController.cellRadius;
        Vector2 maxSpawnPos = new Vector2(gridSize.x * nodeRadius * 2 + nodeRadius, gridSize.y * nodeRadius * 2 + nodeRadius);
        int colMask = LayerMask.GetMask("Impassible", "Units");
        Vector3 newPos;
        for (int i = 0; i < numUnitsPerSpawn; i++)
        {
            GameObject newUnit = Instantiate(unitPrefab);
            newUnit.transform.parent = parentUnit;
            unitsInGame.Add(newUnit);
            do
            {
                newPos = new Vector3(Random.Range(0, maxSpawnPos.x), 0.5f, Random.Range(0, maxSpawnPos.y));
                newUnit.transform.position = newPos;
            }
            while (Physics.OverlapSphere(newPos, 0.25f, colMask).Length > 0);
        }
    }

    private void DestroyUnits()
    {
        foreach (GameObject go in unitsInGame)
        {
            Destroy(go);
        }
        unitsInGame.Clear();
    }


}