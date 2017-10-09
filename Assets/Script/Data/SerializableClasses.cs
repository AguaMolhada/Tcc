// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializableClasses.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Season {
    public string SeasonName;
    public float MinTemp;
    public float MaxTemp;
}

[System.Serializable]
public class Job {
    public string JobName;
    public float Speed;
    public bool Female;
    public bool Male;
}

[System.Serializable]
public class Building
{
    public string BuildingName;
    public TypeBuilding Type;
    public GameObject BuildingGameObject;
    public float TimeToBuild;
    public float Timer { get; protected set; }
    public bool IsFinished { get; protected set; }
    public int MaxCitzenInside;
    public int X { get; protected set; }
    public int Y { get; protected set; }

    public List<Citzen> Habitants;

    //Default Constructor
    public Building()
    {
    }

    IEnumerator LetsBuild()
    {

        yield return new WaitForSeconds(TimeToBuild);
        IsFinished = true;
    }

}

//All Building Types
//--------------------Ignorar isso-------------------//
//A-B-C-D-E-F-G-H-I-J-K-L-M-N-O-P-Q-R-S-T-U-V-W-X-Y-Z//
//---------------------------------------------------//
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
    Orchard,
    School,
    Storage,
    Tailor,
    Tavern,
    TownHall,
    Woodcutter,
}

public enum HouseEventsHandler
{
    Sucess,
    HabitantesFull,
    EnoughtFamilies,
    TooCold,
    EmptyHouse,
}

public enum CitzenGenere
{
    Female,
    Male,
}