// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobData.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to store all jobs in the game
/// </summary>
[CreateAssetMenu(menuName = "Data/Job")]
public class JobData : UpdatableObject
{
    /// <summary>
    /// List with all jobs
    /// </summary>
    public List<Job> Jobs;

    /// <summary>
    /// Method to sort in alphabetical order
    /// </summary>
    public void SortArray()
    {
        Jobs.Sort((a, b) => string.Compare(a.JobName, b.JobName, StringComparison.Ordinal));
    }

}