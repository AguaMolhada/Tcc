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
/// Class used for the mesh generation script.
/// </summary>
public class MeshData {
    /// <summary>
    /// Array with all vertices.
    /// </summary>
    public Vector3[] Vertices;
    /// <summary>
    /// Array with all triangles.
    /// </summary>
    public int[] Triangles;
    /// <summary>
    /// Mesh uv map
    /// </summary>
    public Vector2[] Uvs;
    /// <summary>
    /// I DONT FUCKING KNOW HOW TO EXPLAIN SEE MESHDATA.
    /// </summary>
    private int _triangleIndex;

    /// <summary>
    /// Bool to use activate the flat shading.
    /// </summary>
    /// <summary>
    /// MeshData constructor.
    /// </summary>
    /// <param name="meshWidth">Mesh width</param>
    /// <param name="meshHeight">Mesh Height</param>
    public MeshData( int meshWidth , int meshHeight ) {
        Vertices = new Vector3[meshWidth * meshHeight];
        Uvs = new Vector2[meshWidth * meshHeight];
        Triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }

    /// <summary>
    /// To add triangles in the trinangles array.
    /// </summary>
    /// <param name="a">Position a</param>
    /// <param name="b">Position b</param>
    /// <param name="c">Position c</param>
    public void AddTriangle( int a , int b , int c ) {
        Triangles[_triangleIndex] = a;
        Triangles[_triangleIndex + 1] = b;
        Triangles[_triangleIndex + 2] = c;
        _triangleIndex += 3;
    }

    /// <summary>
    /// Flat shading function. This will put all triangles independent.
    /// </summary>
    public void FlatShading() {
        Vector3[] flatShadedVertices = new Vector3[Triangles.Length];
        Vector2[] flatShaderUv = new Vector2[Triangles.Length];
        for ( int i = 0 ; i < Triangles.Length ; i++ ) {
            flatShadedVertices[i] = Vertices[Triangles[i]];
            flatShaderUv[i] = Uvs[Triangles[i]];

            Triangles[i] = i;
        }
        Vertices = flatShadedVertices;
        Uvs = flatShaderUv;

    }
    /// <summary>
    /// Function to mesh creation.
    /// </summary>
    /// <returns>New Mesh</returns>
    public Mesh CreateMesh() {
        Mesh mesh = new Mesh {
            vertices = Vertices ,
            triangles = Triangles ,
            uv = Uvs
        };
        mesh.RecalculateNormals();
        return mesh;
    }
}
/// <summary>
/// Class used to manager the resources in-game.
/// </summary>
[System.Serializable]
public class GameResources {
    /// <summary>
    /// Ammout of wood.
    /// </summary>
    public int Wood;
    /// <summary>
    /// Ammout of stone.
    /// </summary>
    public int Stone;
    /// <summary>
    /// Ammout of Iron.
    /// </summary>
    public int Iron;
    /// <summary>
    /// Ammout of food.
    /// </summary>
    public int Food;
}

/// <summary>
/// Class used for monitoring the game time.
/// </summary>
[System.Serializable]
public class DateTimeGame
{
    public string CurrentSeason;
    public Season[] Seasons;
    private int _seasonIndex;
    public int CurrentDay { get; private set; }
    private int _maxDay;
    /// <summary>
    /// In-game hour (0-23 format).
    /// </summary>
    public int Hour { get; private set; }
    /// <summary>
    /// In-game minutes (0-59 format).
    /// </summary>
    public int Minutes { get; private set; }
    /// <summary>
    /// Speed that game will run (1 = 1min/second).
    /// </summary>
    [Range(1 , 4)]
    public int Speed;
    /// <summary>
    /// Methodo to change the time.
    /// </summary>
    /// <param name="x">Time in minutes.</param>
    public void TimePass(int x)
    {
        Minutes += x;
        if (Minutes <= 59)
        {
            return;
        }
        Hour++;
        Minutes -= 59;
        if (Hour <= 23)
        {
            return;
        }
        Hour = 0;
        CurrentDay++;
        if (CurrentDay <= _maxDay)
        {
            return;
        }
        _seasonIndex++;
        if (_seasonIndex == Seasons.Length)
        {
            _seasonIndex = 0;
        }
        CurrentSeason = Seasons[_seasonIndex].SeasonName;
        CurrentDay = 1;
        _maxDay = Seasons[_seasonIndex].Days;
    }
}

/// <summary>
/// Class used for seed plantation variables.
/// </summary>
[System.Serializable]
public class PlantationSeeds {
    /// <summary>
    /// Seed Name.
    /// </summary>
    public string SeedName;
    /// <summary>
    /// time to finish planting
    /// </summary>
    public int DaysToPlant;
    /// <summary>
    /// Days to full grow the seed and give food.
    /// </summary>
    public int DaysToGrow;
    /// <summary>
    /// Time to harverst the plantation (Farm size will affect the total time).
    /// </summary>
    public int DaysToHarvest;
    /// <summary>
    /// Minimum temperature that the plantation will resist.
    /// </summary>
    public int MinTemperatureResistence;
    /// <summary>
    /// Ammount of food given after havesting the grow seed.
    /// </summary>
    public int AmmountFood;
}

/// <summary>
/// Class used for all trees used in the Orcherd
/// </summary>
[System.Serializable]
public class OrchardTrees {
    /// <summary>
    /// Tree Type Name.
    /// </summary>
    public string TreeName;
    /// <summary>
    /// Tree Age in years.
    /// </summary>
    public int Age;
    /// <summary>
    /// Age to start the production.
    /// </summary>
    public int AgeToProduce;
    /// <summary>
    /// Ammout Food Given.
    /// </summary>
    public int AmmoutFood;
    /// <summary>
    /// Time spend haversting.
    /// </summary>
    public int TimeHarvest;
}

/// <summary>
/// Class used for in-game season.
/// </summary>
[System.Serializable]
public class Season {
    /// <summary>
    /// Season Name.
    /// </summary>
    public string SeasonName;
    /// <summary>
    /// Number of days that this season will have.
    /// </summary>
    public int Days;
    /// <summary>
    /// Season minimum temperature.
    /// </summary>
    public float MinTemp;
    /// <summary>
    /// Season maximum temperature.
    /// </summary>
    public float MaxTemp;
}

/// <summary>
/// Base class for job.
/// </summary>
[System.Serializable]
public class Job {
    /// <summary>
    /// Job Name.
    /// </summary>
    public string JobName;
    /// <summary>
    /// Moviment speed.
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

//--------------------Ignorar isso-------------------//
//A-B-C-D-E-F-G-H-I-J-K-L-M-N-O-P-Q-R-S-T-U-V-W-X-Y-Z//
//---------------------------------------------------//
/// <summary>
/// Enum with all building types.
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
/// Enum with filter organizers to sort some arrays in-game.
/// </summary>
public enum OrganizerFilter {
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
/// Enum that handle all general building events.
/// </summary>
public enum BuildingEventsHandler {
    Complete,
    NoLumber,
    NoStone,
    NoIron,
    InvalidPos,
}

/// <summary>
/// Enum that handle all house events.
/// </summary>
public enum HouseEventsHandler {
    Sucess,
    HabitantesFull,
    EnoughtFamilies,
    TooCold,
    EmptyHouse,
}

public enum FarmEventsHandler {
    Idle,
    Planting,
    Growing,
    Harvesting,
    Decaying,
    Plage,
}

/// <summary>
/// Citzen genere.
/// </summary>
public enum Genere {
    Female,
    Male,
}
