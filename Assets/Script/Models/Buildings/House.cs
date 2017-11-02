// --------------------------------------------------------------------------------------------------------------------
// <copyright file="House.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class used on all houses
/// </summary>
public class House : GenericBuilding
{
    /// <summary>
    /// List with all habitants living in this house
    /// </summary>
    public List<Citzen> Habitants;
    /// <summary>
    /// Max families allowed on the house
    /// </summary>
    public int MaxFamiles { get; protected set; }
    /// <summary>
    /// House temperature
    /// </summary>
    public float Temp { get; protected set; }       
    /// <summary>
    /// How many people inside the house at the moment
    /// </summary>
    public int PeopleInside { get; protected set; }
    /// <summary>
    /// Each food will give some ammout. Depens on the season.
    /// </summary>
    public float AmmoutFood { get; protected set; }
    /// <summary>
    /// Ammout charcoal inside the house
    /// </summary>
    public float AmoutCharcoal { get; protected set; }

    /// <summary>
    /// Constructor for the House Class
    /// </summary>
    /// <param name="baseData">Data will be acquired via database</param>
    /// <param name="bName">Name of desired building to create</param>
    /// <param name="familes">Number of families living in the house</param>
    /// <param name="t">Temperature of the House</param> TODO(Season and daytime will provide one or will be default)
    public House(string bName, int familes, float t)
    {
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
            if (Habitants.Count < MaxCitzenInside)
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
