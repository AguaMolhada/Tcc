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
}

[System.Serializable]
public class Building
{
    public GameObject BuildingGameObject;
    public int MaxCitzenInside;
    public List<Citzen> Citzens;
    public TypeBuilding Type;

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

public enum CitzenGenere
{
    Female,
    Male,
}