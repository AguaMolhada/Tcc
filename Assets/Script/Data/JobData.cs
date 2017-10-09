// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobData.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Job")]
public class JobData : UpdatableObject
{
    public List<Job> Jobs;

    public void SortArray()
    {
        Jobs.Sort((a, b) => string.Compare(a.JobName, b.JobName, StringComparison.Ordinal));

    }

}