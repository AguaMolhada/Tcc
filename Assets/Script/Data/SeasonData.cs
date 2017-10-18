// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SeasonData.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to store all seasons (this will be loaded with a database and can be edited)
/// </summary>
[CreateAssetMenu(menuName = "Data/Season")]
public class SeasonData : UpdatableObject
{
    /// <summary>
    /// List with all seasons
    /// </summary>
    public List<Season> Seasons;
}