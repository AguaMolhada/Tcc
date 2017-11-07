// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CityData.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// ReSharper disable ReturnValueOfPureMethodIsNotUsed

/// <summary>
/// City data used to save things in the game
/// </summary>
[CreateAssetMenu(menuName = "Data/CityData")]
public class CityData : UpdatableObject
{
    /// <summary>
    /// City name.
    /// </summary>
    public string CityName;
    /// <summary>
    /// List with all habitants living in the city.
    /// </summary>
    public List<GameObject> CityHabitants;
    /// <summary>
    /// Game Recources
    /// </summary>
    public GameResources CityResources;
    /// <summary>
    /// Current time in-game.
    /// </summary>
    public DateTimeGame Time;
    /// <summary>
    /// For sorting the citzen List.
    /// </summary>
    /// <param name="comparsionType">Param used to determine how the List witch sort will be used.</param>
    public void SortArray(OrganizerFilter comparsionType)
    {
        switch (comparsionType)
        {
            case OrganizerFilter.Name:
                CityHabitants.Sort((a, b) => string.Compare(a.GetComponent<Citzen>().Name, b.GetComponent<Citzen>().Name, StringComparison.Ordinal));
                break;
            case OrganizerFilter.AgeAsc:
                CityHabitants.OrderBy(a => a.GetComponent<Citzen>().Age);
                break;
            case OrganizerFilter.AgeDesc:
                CityHabitants.OrderByDescending(a => a.GetComponent<Citzen>().Age);
                break;
            case OrganizerFilter.HappyAsc:
                CityHabitants.OrderBy(a => a.GetComponent<Citzen>().Happiness);
                break;
            case OrganizerFilter.HappyDesc:
                CityHabitants.OrderByDescending(a => a.GetComponent<Citzen>().Happiness);
                break;
            case OrganizerFilter.Job:
                CityHabitants.Sort((a, b) => string.Compare(a.GetComponent<Citzen>().Profession.JobName, b.GetComponent<Citzen>().Profession.JobName, StringComparison.Ordinal));
                break;
            case OrganizerFilter.GenereFm:
                break;
            case OrganizerFilter.GenereMf:
                break;
        }
    }

}
