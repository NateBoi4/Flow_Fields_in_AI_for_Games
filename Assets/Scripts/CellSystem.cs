using UnityEngine;

/*
Credit to JOHNNYTURBO from https://pastebin.com/u/JohnnyTurbo
Date: August 14, 2020
GridDirection code from https://pastebin.com/nj7fhkTM
*/

public class CellSystem
{
    public Vector3 worldPos;
    public Vector2Int gridIndex;
    public byte cost;
    public ushort bestCost;
    public GridDirection bestDirection;

    public CellSystem(Vector3 _worldPos, Vector2Int _gridIndex)
    {
        worldPos = _worldPos;
        gridIndex = _gridIndex;
        cost = 1;
        bestCost = ushort.MaxValue;
        bestDirection = GridDirection.None;
    }

    public void IncreaseCost(int amnt)
    {
        if (cost == byte.MaxValue) { return; }
        if (amnt + cost >= 255) { cost = byte.MaxValue; }
        else { cost += (byte)amnt; }
    }
}