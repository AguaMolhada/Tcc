// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildingData.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Building")]
public class BuildingData : UpdatableObject
{
    public List<Building> Buildings;

    public void SortArray() {
        Buildings.Sort(( a , b ) => string.Compare(a.BuildingName , b.BuildingName , StringComparison.Ordinal));
    }
    
}
