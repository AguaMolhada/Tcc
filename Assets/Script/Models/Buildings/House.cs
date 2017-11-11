// --------------------------------------------------------------------------------------------------------------------
// <copyright file="House.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used on all houses
/// </summary>
public class House : GenericBuilding
{
    /// <summary>
    /// List with all habitants living in this house
    /// </summary>
    public List<GameObject> Habitants;

    /// <summary>
    /// Max families allowed on the house
    /// </summary>
    public int MaxFamiles;
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
    public HouseEventsHandler RegisterPeopleInHouse(GameObject people)
    {
        var count = 0;
        if (Habitants.Count != 0)
        {
            foreach (var habitant in Habitants)
            {
                if (habitant != null)
                {
                    if (habitant.GetComponent<Citzen>().Age >= 20)
                    {
                        count++;
                    }
                }
            }
            if (count > MaxFamiles * 2)
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
        Habitants.Add(people);
        return HouseEventsHandler.Sucess;
    }
}
