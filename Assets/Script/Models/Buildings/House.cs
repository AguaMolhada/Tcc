// --------------------------------------------------------------------------------------------------------------------
// <copyright file="House.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class House : MonoBehaviour
{
    public Building BuildingData { get; protected set; }
    public List<Citzen> Habitants;

    public int MaxFamiles { get; protected set; }
    public float Temp { get; protected set; }
    public int PeopleInside { get; protected set; }
    public float AmmoutFood { get; protected set; } // Each food will give some amount. Depends on the season.
    public float AmoutCharcoal { get; protected set; } // Charcoal will provide heat to the house

    /// <summary>
    /// Constructor for the House Class
    /// </summary>
    /// <param name="baseData">Data will be acquired via database</param>
    /// <param name="bName">Name of desired building to create</param>
    /// <param name="familes">Number of families living in the house</param>
    /// <param name="t">Temperature of the House</param> TODO(Season and daytime will provide one or will be default)
    public House(BuildingData baseData, string bName, int familes, float t)
    {
        foreach (var data in baseData.Buildings)
        {
            if (data.BuildingName == bName )
            {
                BuildingData = data;
                break;
            }
        }
        MaxFamiles = familes;
        Temp = t;
    }
    /// <summary>
    /// Method to add food to the house storage
    /// </summary>
    /// <param name="a">Food ammout</param>
    /// <returns>If the house isn't empy and the ammout not 0 return true</returns>
    public HouseEventsHandler AddFood(float a)
    {
        if (a > 0 && Habitants.Count > 0)
        {
            AmmoutFood += a;
            return HouseEventsHandler.Sucess;
        }
        return HouseEventsHandler.EmptyHouse;
    }

    //Try to register one citzen in the house.
    //If have more than 2 familes (4people over 20years) new citzen cant move
    //Otherwise will add the citzen to the house
    /// <summary>
    /// Method to register one citzen to the house
    /// </summary>
    /// <param name="people">Citzen that will be added</param>
    /// <returns>If the house dont have the number max</returns>
    public HouseEventsHandler RegisterPeopleInHouse(Citzen people)
    {
        
        var count = Habitants.Count(habitant => habitant.Age >= 20);
        if (count > MaxFamiles*2)
        {
            return HouseEventsHandler.EnoughtFamilies;
        }
        else
        {
            if (Habitants.Count < BuildingData.MaxCitzenInside)
            {
                Habitants.Add(people);
                return HouseEventsHandler.Sucess;
            }
            else
            {
                return HouseEventsHandler.HabitantesFull;
            }
        }
    }

}
