// --------------------------------------------------------------------------------------------------------------------//
// <copyright file="BuildingCOntroller.cs" company="Dauler Palhares">                                                  //
//  © Copyright Dauler Palhares da Costa Viana 2017.                                                                   //
//          http://github.com/DaulerPalhares                                                                           //
// </copyright>                                                                                                        //
// --------------------------------------------------------------------------------------------------------------------//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {

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
    public bool CheckOverlap(int x, int y,BuildingPattern pattern)
    {
        var grid = WorldController.MapBuildingGrid;
        for (var xGrid = x; xGrid < x+pattern.Rows.Length; xGrid++)
        {
            for (var yGrid = y; yGrid < y+pattern.Rows[xGrid].Collums.Length; yGrid++)
            {
                if(grid[xGrid,yGrid] != 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void AssignBuildingToGrid(int x,int y, BuildingPattern pattern, int id)
    {
        var grid = WorldController.MapBuildingGrid;
        for (var xGrid = x; xGrid < x + pattern.Rows.Length; xGrid++)
        {
            for (var yGrid = y; yGrid < y + pattern.Rows[xGrid].Collums.Length; yGrid++)
            {
                grid[xGrid, yGrid] = id;
            }
        }
        WorldController.MapBuildingGrid = grid;
    }
}
