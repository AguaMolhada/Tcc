// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableClasses.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used for monitoring the game time.
/// </summary>
[System.Serializable]
public class DateTimeGame
{
    /// <summary>
    /// In-game hour (0-23 format)
    /// </summary>
    [Range(0,23)]
    public int Hour;
    /// <summary>
    /// In-game minutes (0-59 format)
    /// </summary>
    [Range(0,59)]
    public int Minutes;
    /// <summary>
    /// Speed that game will run (1 = 1min/second)
    /// </summary>
    [Range(1,4)]
    public int Speed;
}

/// <summary>
/// Class used for in-game season
/// </summary>
[System.Serializable]
public class Season {
    /// <summary>
    /// Season Name
    /// </summary>
    public string SeasonName;
    /// <summary>
    /// Number of days that this season will have
    /// </summary>
    public int Days;
    /// <summary>
    /// Season minimum temperature
    /// </summary>
    public float MinTemp;
    /// <summary>
    /// Season maximum temperature
    /// </summary>
    public float MaxTemp;
}

/// <summary>
/// Base class for job
/// </summary>
[System.Serializable]
public class Job {
    /// <summary>
    /// Job Name
    /// </summary>
    public string JobName;
    /// <summary>
    /// Moviment speed
    /// </summary>
    public float Speed;
    /// <summary>
    /// Famale can work on this job?
    /// </summary>
    public bool Female;
    /// <summary>
    /// Male can work on this job?
    /// </summary>
    public bool Male;
}

/// <summary>
/// Base class for buildings
/// </summary>
[System.Serializable]
public class Building
{
    /// <summary>
    /// Building Name
    /// </summary>
    public string BuildingName;
    /// <summary>
    /// Building type (used for diferent scripts to work)
    /// </summary>
    public TypeBuilding Type;
    /// <summary>
    /// Game object that will spaw this building
    /// </summary>
    public GameObject BuildingGameObject;
    /// <summary>
    /// Lumber cost to build
    /// </summary>
    public int LumberCost;
    /// <summary>
    /// Rock cost to build
    /// </summary>
    public int RockCost;
    /// <summary>
    /// Metal cost to build
    /// </summary>
    public int MetalCost;
    /// <summary>
    /// Time to finish the construction
    /// </summary>
    public float TimeToBuild;
    /// <summary>
    /// Timer that will decrease (used fo the construction
    /// and if the building produces something this will
    /// be the cooldown
    /// </summary>
    public float Timer { get; protected set; }
    /// <summary>
    /// Is the building alread constructed
    /// </summary>
    public bool IsFinished { get; protected set; }
    /// <summary>
    /// How many Citzens that building will support
    /// </summary>
    public int MaxCitzenInside;
    /// <summary>
    /// Position x on the world
    /// </summary>
    public int Xpos { get; protected set; }
    /// <summary>
    /// Position y on the world
    /// </summary>
    public int Ypos { get; protected set; }
    /// <summary>
    /// Position z on the world
    /// </summary>
    public int Zpos { get; protected set; }
    /// <summary>
    /// Rotation on X axis
    /// </summary>
    public int Xrot { get; protected set; }
    /// <summary>
    /// Rotation on Y axis
    /// </summary>
    public int Yrot { get; protected set; }
    /// <summary>
    /// Rotation on Z axis
    /// </summary>
    public int Zrot { get; protected set; }
    //Default Constructor
    public Building()
    {
    }

    /// <summary>
    /// Default method that will construct the building 
    /// </summary>
    /// <returns></returns>
    IEnumerator LetsBuild()
    {
//TODO some building effects
        yield return new WaitForSeconds(TimeToBuild);
        IsFinished = true;
    }

}

//--------------------Ignorar isso-------------------//
//A-B-C-D-E-F-G-H-I-J-K-L-M-N-O-P-Q-R-S-T-U-V-W-X-Y-Z//
//---------------------------------------------------//
/// <summary>
/// Enum with all building types
/// </summary>
public enum TypeBuilding {
    Blacksmith,
    Church,
    Decoration,
    Docks,
    Farm,
    Graveyard,
    Hosipital,
    House,
    LivingFarm,
    Market,
    Mine,
    Orchard,
    School,
    Storage,
    Tailor,
    Tavern,
    TownHall,
    Woodcutter,
}

/// <summary>
/// Enum with filter organizers to sort some arrays in-game
/// </summary>
public enum OrganizerFilter
{
    Name,
    AgeAsc,
    AgeDesc,
    HappyAsc,
    HappyDesc,
    Job,
    GenereFm,
    GenereMf,
}

/// <summary>
/// Enum that handle all house events
/// </summary>
public enum HouseEventsHandler
{
    Sucess,
    HabitantesFull,
    EnoughtFamilies,
    TooCold,
    EmptyHouse,
}

/// <summary>
/// Citzen genere
/// </summary>
public enum CitzenGenere
{
    Female,
    Male,
}