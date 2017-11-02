// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericBuilding.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>
/// Base class for buildings.
/// </summary>
[System.Serializable]
public class GenericBuilding : MonoBehaviour
{
    /// <summary>
    /// Building Name.
    /// </summary>
    public string BuildingName;
    /// <summary>
    /// Unique building id to assign to the building grid
    /// </summary>
    public int BuildingID;
    /// <summary>
    /// Building type (used for diferent scripts to work).
    /// </summary>
    public TypeBuilding Type;

    /// <summary>
    /// Building area Pattern
    /// </summary>
    public BuildingPattern Pattern;

    /// <summary>
    /// Lumber cost to build.
    /// </summary>
    public int LumberCost;

    /// <summary>
    /// Rock cost to build.
    /// </summary>
    public int RockCost;

    /// <summary>
    /// Metal cost to build.
    /// </summary>
    public int MetalCost;

    /// <summary>
    /// Time to finish the construction.
    /// </summary>
    public float TimeToBuild;

    /// <summary>
    /// Timer that will decrease (used fo the construction
    /// and if the building produces something this will
    /// be the cooldown.
    /// </summary>
    public float Timer { get; protected set; }

    /// <summary>
    /// Is the building alread constructed.
    /// </summary>
    public bool IsFinished { get; protected set; }

    /// <summary>
    /// How many Citzens that building will support.
    /// </summary>
    public int MaxCitzenInside;

    /// <summary>
    /// Position x on the world.
    /// </summary>
    public float Xpos { get; protected set; }

    /// <summary>
    /// Position y on the world.
    /// </summary>
    public float Ypos { get; protected set; }

    /// <summary>
    /// Position z on the world.
    /// </summary>
    public float Zpos { get; protected set; }

    /// <summary>
    /// Rotation on X axis.
    /// </summary>
    public float Xrot { get; protected set; }

    /// <summary>
    /// Rotation on Y axis.
    /// </summary>
    public float Yrot { get; protected set; }

    /// <summary>
    /// Rotation on Z axis.
    /// </summary>
    public float Zrot { get; protected set; }

    /// <summary>
    /// On click to construck will check if have something worng. If the building is clear to build will return Completed
    /// </summary>
    /// <returns>Return the Event related to the building</returns>
    public BuildingEventsHandler OnConstruction(int x, int y)
    {
        if (GameController.Instance.City.CityResources.Wood < LumberCost)
        {
            return BuildingEventsHandler.NoLumber;
        }
        if (GameController.Instance.City.CityResources.Stone < RockCost)
        {
            return BuildingEventsHandler.NoStone;
        }
        if (GameController.Instance.City.CityResources.Iron < MetalCost)
        {
            return BuildingEventsHandler.NoIron;
        }
        if (!BuildingController.Instance.CheckOverlap(x, y, Pattern))
        {
            Vector3 worldPos = BuildingGrid.GridPositionRelatedToWorld(WorldController.MapBuildingGrid, x, y);
            Xpos = worldPos.x;
            Zpos = worldPos.z;
            BuildingController.Instance.AssignBuildingToGrid(x, y, Pattern, BuildingID);
            GameController.Instance.City.CityResources.UpdateResources(LumberCost, RockCost, MetalCost);
            GameController.Instance.City.CityBuildings.Add(this);
            return BuildingEventsHandler.Complete;
        }
        return BuildingEventsHandler.InvalidPos;

    }
}