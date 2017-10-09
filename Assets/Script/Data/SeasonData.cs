// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SeasonData.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Season")]
public class SeasonData : UpdatableObject
{
    public List<Season> Seasons;
}