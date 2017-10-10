// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CityData.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/CityData")]
public class CityData : UpdatableObject {

    public string CityName;
    public List<Citzen> CityHabitants;
    public Season CurrentSeason;
    public DateTimeGame Time;

    public void SortArray(OrganizerFilter comparsionType)
    {
        switch (comparsionType)
        {
            case OrganizerFilter.Name:
                CityHabitants.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
                break;
            case OrganizerFilter.AgeAsc:
                CityHabitants.OrderBy(a => a.Age);
                break;
            case OrganizerFilter.AgeDesc:
                CityHabitants.OrderByDescending(a => a.Age);
                break;
            case OrganizerFilter.HappyAsc:
                CityHabitants.OrderBy(a => a.Happiness);
                break;
            case OrganizerFilter.HappyDesc:
                CityHabitants.OrderByDescending(a => a.Happiness);
                break;
            case OrganizerFilter.Job:
                CityHabitants.Sort((a, b) => string.Compare(a.Profession.JobName, b.Profession.JobName, StringComparison.Ordinal));
                break;
            case OrganizerFilter.GenereFM:
                break;
            case OrganizerFilter.GenereMF:
                break;
            default:
                break;
        }
    }

}
