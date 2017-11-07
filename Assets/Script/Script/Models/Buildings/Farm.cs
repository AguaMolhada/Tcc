// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Farm.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Farm Script
/// </summary>
public class Farm : GenericBuilding, IJobBuilding
{
    /// <summary>
    /// Seed that will be planted next Spring.
    /// </summary>
    public PlantationSeeds SelectedSeed { get; private set; }
    /// <summary>
    /// Days passed since the start FarmState.
    /// </summary>
    public int DaysSinceStart { get; private set; }
    /// <summary>
    /// Farm size in X and Y.
    /// </summary>
    public int[] FarmSize { get; private set; }
    /// <summary>
    /// Ammount of food lost.
    /// </summary>
    public float DecayingStatus { get; private set; }
    /// <summary>
    /// List of workers working on the farm.
    /// </summary>
    public List<Citzen> Workers;
    /// <summary>
    /// Max workers allowed on the farm.
    /// </summary>
    public int MaxWorkers => (int) (1.25f * (FarmSize[0] + FarmSize[1] / 2f));
    /// <summary>
    /// Current Farm State;
    /// </summary>
    public FarmEventsHandler FarmState;
    /// <summary>
    /// Current day stored.
    /// </summary>
    private int currentDay;

    /// <summary>
    /// Show Progress of the current season.
    /// </summary>
    /// <returns>A number bteween 0 and 100</returns>
    public int ShowProgress()
    {
        switch (FarmState)
        {
            case FarmEventsHandler.Planting:
                return (int) Ultility.PercentValue(SelectedSeed.DaysToPlant, DaysSinceStart);
            case FarmEventsHandler.Growing:
                return (int) Ultility.PercentValue(SelectedSeed.DaysToGrow, DaysSinceStart);
            case FarmEventsHandler.Harvesting:
                return (int) Ultility.PercentValue(SelectedSeed.DaysToHarvest, DaysSinceStart);
            default:
                return 0;
        }
    }
    /// <summary>
    /// Method to add resources to the city.
    /// </summary>
    /// <param name="resources">GameController instance</param>
    public void AddResources(GameResources resources)
    {
        resources.Food += (int) (SelectedSeed.AmmountFood * (FarmSize[0] + FarmSize[1] / 2f));
    }

    public bool AssignWorker()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Check current farm state.
    /// </summary>
    private void CheckFarmState()
    {
        if (GameController.Instance.City.Time.CurrentDay != currentDay)
        {
            currentDay = GameController.Instance.City.Time.CurrentDay;
            DaysSinceStart++;
        }
        switch (FarmState)
        {
            case FarmEventsHandler.Planting:

                if (ShowProgress() >= 100)
                {
                    FarmState = FarmEventsHandler.Growing;
                    DaysSinceStart = 0;
                }
                break;
            case FarmEventsHandler.Growing:
                if (ShowProgress() >= 100)
                {
                    FarmState = FarmEventsHandler.Harvesting;
                    ;
                    DaysSinceStart = 0;
                }
                break;
            case FarmEventsHandler.Harvesting:
                if (ShowProgress() >= 100)
                {
                    AddResources(GameController.Instance.City.CityResources);
                }
                FarmState = FarmEventsHandler.Idle;
                break;
            case FarmEventsHandler.Decaying:
                if (GameController.Instance.City.Time.CurrentSeason == "Winter")
                {
                    DecayingStatus += 0.10f;
                }
                break;
            case FarmEventsHandler.Plage:
                break;
            case FarmEventsHandler.Idle:
                if (GameController.Instance.City.Time.CurrentSeason == "Spring")
                {
                    DaysSinceStart = 0;
                    FarmState = FarmEventsHandler.Planting;
                }
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        CheckFarmState();
    }
}