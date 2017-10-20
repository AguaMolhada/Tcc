// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildingData.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class storing all building data. (this will be loaded with a database and can be edited)
/// </summary>
[CreateAssetMenu(menuName = "Data/Building")]
public class BuildingData : UpdatableObject
{
    /// <summary>
    /// List with all buildings data
    /// </summary>
    public List<GenericBuilding> Buildings;

    /// <summary>
    /// Sort by alphabetical order
    /// </summary>
    public void SortArray() {
        Buildings.Sort(( a , b ) => string.Compare(a.BuildingName , b.BuildingName , StringComparison.Ordinal));
    }
    
}
