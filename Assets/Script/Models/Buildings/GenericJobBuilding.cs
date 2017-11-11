using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericJobBuilding : GenericBuilding
{    
    /// <summary>
    /// List of workers
    /// </summary>
    public List<GameObject> Workers;
    /// <summary>
    /// Hours that the people will work here.
    /// </summary>
    public int[] HoursToWork;
    /// <summary>
    /// Seasons that the people will work here.
    /// </summary>
    public string[] SeasonsToWork;
    /// <summary>
    /// Max workers allowed on the building
    /// </summary>
    public int MaxWorkers;
    /// <summary>
    /// If is ready to start.
    /// </summary>
    protected bool IsReady;

    /// <summary>
    /// Method to show the building progress.
    /// </summary>
    /// <returns></returns>
    public virtual int ShowProgress()
    {
        return 0;
    }
    /// <summary>
    /// Method to add resources to the city.
    /// </summary>
    /// <param name="x">GameController instance.</param>
    public virtual void AddResources(GameResources x)
    {
        
    }
    /// <summary>
    /// Method to assign worker on the building.
    /// </summary>
    /// <returns>True if the worker has been assigned to the building.</returns>
    public virtual bool AssignWorker(GameObject citzen)
    {
        if (Workers.Count < MaxWorkers)
        {
            Workers.Add(citzen);
            citzen.GetComponent<Citzen>().JobLocation = gameObject;
            return true;
        }
        else
        {
            return false;
        } 
    }

    public virtual void RemoveWoerker(GameObject citzen)
    {
        Workers.Remove(citzen);
    }
}
