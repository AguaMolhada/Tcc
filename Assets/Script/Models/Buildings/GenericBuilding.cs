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
    /// Building type (used for diferent scripts to work).
    /// </summary>
    public TypeBuilding Type;
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
    public int Xpos { get; protected set; }
    /// <summary>
    /// Position y on the world.
    /// </summary>
    public int Ypos { get; protected set; }
    /// <summary>
    /// Position z on the world.
    /// </summary>
    public int Zpos { get; protected set; }
    /// <summary>
    /// Rotation on X axis.
    /// </summary>
    public int Xrot { get; protected set; }
    /// <summary>
    /// Rotation on Y axis.
    /// </summary>
    public int Yrot { get; protected set; }
    /// <summary>
    /// Rotation on Z axis.
    /// </summary>
    public int Zrot { get; protected set; }
    /// <summary>
    /// On click to construck will check if have something worng. If the building is clear to build will return Completed
    /// </summary>
    /// <returns>Return the Event related to the building</returns>
    public BuildingEventsHandler OnConstruction() {
        var x = 0; //TODO need to implement something to pick the desired location to build
        var y = 0;

        if ( GameController.Instance.City.CityResources.Wood < LumberCost ) {
            return BuildingEventsHandler.NoLumber;
        }
        if ( GameController.Instance.City.CityResources.Stone < RockCost ) {
            return BuildingEventsHandler.NoStone;
        }
        if ( GameController.Instance.City.CityResources.Iron < MetalCost ) {
            return BuildingEventsHandler.NoIron;
        }
        if ( CheckOverlap(x , y) ) {
            Xpos = x;
            Ypos = y;
            return BuildingEventsHandler.InvalidPos;
        }
        //TODO need to implement check if overlap buildings
        return BuildingEventsHandler.Complete;
    }
    //TODO
    public bool CheckOverlap( int x , int y ) {
        throw new System.NotImplementedException();
    }

}