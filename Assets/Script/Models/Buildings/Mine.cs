// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Mine.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;

/// <summary>
/// Class used on the mining.
/// </summary>
public class Mine : GenericBuilding , IJobBuilding
{
    /// <summary>
    /// Total Resources that the player will gather. When depleted the mine will "Recharge" itself.
    /// </summary>
    public int TotalResources;
    /// <summary>
    /// Ammout on days that the mine will wait to recharge itself.
    /// </summary>
    public int Cooldown;
    /// <summary>
    /// List of Workers.
    /// </summary>
    public List<Citzen> Workers;
    /// <summary>
    /// Ammout resources collected each day.
    /// </summary>
    public int Ammout => Workers.Count * 4;


    public int ShowProgress()
    {
        throw new System.NotImplementedException();
    }

    public void AddResources(GameResources x)
    {
        throw new System.NotImplementedException();
    }

    public bool AssignWorker()
    {
        throw new System.NotImplementedException();
    }
}
