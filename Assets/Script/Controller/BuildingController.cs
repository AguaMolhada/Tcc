// --------------------------------------------------------------------------------------------------------------------//
// <copyright file="BuildingCOntroller.cs" company="Dauler Palhares">                                                  //
//  © Copyright Dauler Palhares da Costa Viana 2017.                                                                   //
//          http://github.com/DaulerPalhares                                                                           //
// </copyright>                                                                                                        //
// --------------------------------------------------------------------------------------------------------------------//
using UnityEngine;

/// <summary>
/// Class that handle all things related to buildings
/// </summary>
public class BuildingController : MonoBehaviour
{
    /// <summary>
    /// Singleton
    /// </summary>
    public static BuildingController Instance { get; protected set; }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("You can't have 2 Building Controllers");
        }
        Instance = this;
    }

    /// <summary>
    /// Check if the building is overlaping another one or is trying to construct on water
    /// </summary>
    /// <param name="x">Position X to check </param>
    /// <param name="y">POsition Y to check</param>
    /// <param name="pattern">Building Pattern grid</param>
    /// <returns></returns>
    public bool CheckOverlap(int x, int y, BuildingPattern pattern)
    {
        var grid = WorldController.MapBuildingGrid;
        if (CheckIfInsideMapGrid(x, y))
        {
            for (int gridX = x; gridX < x+pattern.Rows.Length; gridX++)
            {
                for (int gridY = y; gridY < y+pattern.Rows[0].Collums.Length; gridY++)
                {
                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    if (grid[gridX, gridY] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        return false;
    }
    /// <summary>
    /// Check if the position is inside of building grid
    /// </summary>
    /// <param name="x">Position X to check </param>
    /// <param name="y">POsition Y to check</param>
    /// <returns></returns>
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
            for (var yGrid = y; yGrid < y + pattern.Rows[0].Collums.Length; yGrid++)
            {
                grid[xGrid, yGrid] = id;
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
    public void AssignBuildingToGrid(int x, int y, BuildingPattern pattern, float id, bool rotate)
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

    /// <summary>
    /// On click to construck will check if have something worng. If the building is clear to build will return Completed
    /// </summary>
    /// <returns>Return the Event related to the building</returns>
    public BuildingEventsHandler OnConstruction(int x, int y, GameObject buildingGameObject)
    {
        var building = buildingGameObject.GetComponent<GenericBuilding>();
        if (GameController.Instance.City.CityResources.Wood < building.LumberCost)
        {
            return BuildingEventsHandler.NoLumber;
        }
        if (GameController.Instance.City.CityResources.Stone < building.RockCost)
        {
            return BuildingEventsHandler.NoStone;
        }
        if (GameController.Instance.City.CityResources.Iron < building.MetalCost)
        {
            return BuildingEventsHandler.NoIron;
        }
        if (CheckOverlap(x, y, building.Pattern))
        {
            Vector3 worldPos = BuildingGrid.GridPositionRelatedToWorld(WorldController.MapChunkSize, x, y);
            building.SetPositionOnWorld(worldPos);
            AssignBuildingToGrid(x, y, building.Pattern, (int)building.Type, buildingGameObject);
            Instantiate(buildingGameObject, worldPos, Quaternion.identity);
            GameController.Instance.City.CityResources.UpdateResources(building.LumberCost, building.RockCost, building.MetalCost);
            GameController.Instance.CityBuildings.Add(buildingGameObject);
            return BuildingEventsHandler.Complete;
        }
        return BuildingEventsHandler.InvalidPos;
    }
}
