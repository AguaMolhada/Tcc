// --------------------------------------------------------------------------------------------------------------------//
// <copyright file="BuildingCOntroller.cs" company="Dauler Palhares">                                                  //
//  © Copyright Dauler Palhares da Costa Viana 2017.                                                                   //
//          http://github.com/DaulerPalhares                                                                           //
// </copyright>                                                                                                        //
// --------------------------------------------------------------------------------------------------------------------//
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{

    public static BuildingController Instance { get; protected set; }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("You can't have 2 Building Controllers");
        }
        Instance = this;
    }

    //TODO Test this
    public bool CheckOverlap(int x, int y, BuildingPattern pattern)
    {
        var grid = WorldController.MapBuildingGrid;
        for (var xGrid = x; xGrid < x + pattern.Rows.Length; xGrid++)
        {
            for (var yGrid = y; yGrid < y + pattern.Rows[xGrid].Collums.Length; yGrid++)
            {
                if (grid[xGrid, yGrid] != 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool CheckIfInsideMapGrid(int x, int y)
    {
        var grid = WorldController.MapBuildingGrid;
        if (x >= 0 && y >= 0)
        {
            if (x < grid.GetLength(0) && y < grid.GetLength(1))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Method to assign the building to the grid
    /// </summary>
    /// <param name="x">Grid Pos X</param>
    /// <param name="y">Grid pos Y</param>
    /// <param name="pattern">Building Pattern</param>
    /// <param name="id">Building type ID</param>
    /// <param name="building">Building prefab to instanciate</param>
    public void AssignBuildingToGrid(int x, int y, BuildingPattern pattern, float id,GameObject building)
    {
        var grid = WorldController.MapBuildingGrid;

        for (var xGrid = x; xGrid < x + pattern.Rows.Length; xGrid++)
        {
            for (var yGrid = y; yGrid < y + pattern.Rows[xGrid].Collums.Length; yGrid++)
            {
                grid[xGrid, yGrid] = id;
                var instanciate = Instantiate(building, BuildingGrid.GridPositionRelatedToWorld(WorldController.MapChunkSize, x, y), Quaternion.identity);
                instanciate.tag ="Building";
            }
        }
        WorldController.MapBuildingGrid = grid;
    }

    /// <summary>
    /// Method to assign the building to the grid
    /// </summary>
    /// <param name="x">Grid Pos X</param>
    /// <param name="y">Grid pos Y</param>
    /// <param name="pattern">Building Pattern</param>
    /// <param name="id">Building type ID</param>
    /// <param name="rotate">Is Rotate?</param>
    /// <param name="building">Building prefab to instanciate</param>
    public void AssignBuildingToGrid(int x, int y, BuildingPattern pattern, float id, bool rotate, GameObject building)
    {
        var grid = WorldController.MapBuildingGrid;
        if (CheckIfInsideMapGrid(x, y))
        {
            for (var xGrid = x; xGrid < x + pattern.Rows.Length; xGrid++)
            {
                for (var yGrid = y; yGrid < y + pattern.Rows[xGrid].Collums.Length; yGrid++)
                {
                    grid[xGrid, yGrid] = id;
                }
            }
            WorldController.MapBuildingGrid = grid;
        }
        else
        {
            Debug.Log("NotValid");
        }
    }

}
