// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameDataEditable.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to store all game ScriptableObject data.
/// </summary>
[CreateAssetMenu(menuName = "Game/Game Data")]
public class GameDataEditable : ScriptableObject
{
    #region Only Used in the custom Inspector DO NOT TOUCH
    public bool ShowJobCustomInspector;
    public bool ShowSeedsCustomInspector;
    public bool ShowSeasonCustomInspector;
    public bool ShowAnimalsCustomInspector;
    public bool ShowOrchardCustomInspector;
    public bool ShowBuildingsCustomInspector;
    #endregion

    #region GameData

    /// <summary>
    /// All in-Game Seasons.
    /// </summary>
    public List<Season> Seasons;

    /// <summary>
    /// All in-game buildings.
    /// </summary>
    public List<GenericBuilding> Buildings;

    /// <summary>
    /// All in-game jobs.
    /// </summary>
    public List<Job> Jobs;

    #region Farm

    /// <summary>
    /// All in-game seeds.
    /// </summary>
    public List<PlantationSeeds> Seeds;

    /// <summary>
    /// All in-game trees used on orchard.
    /// </summary>
    public List<OrchardTrees> TreeTypes;

    /// <summary>
    /// All in-game animals used on LivingFarms.
    /// </summary>
    public List<Animal> Animals;

    #endregion

    #endregion

    public void SortList()
    {
        Buildings.Sort((a, b) => string.Compare(a.BuildingName, b.BuildingName, StringComparison.Ordinal));
        Jobs.Sort((a, b) => string.Compare(a.JobName, b.JobName, StringComparison.Ordinal));
        Seeds.Sort((a, b) => string.Compare(a.SeedName, b.SeedName, StringComparison.Ordinal));
        TreeTypes.Sort((a, b) => string.Compare(a.TreeName, b.TreeName, StringComparison.Ordinal));
    }

} 