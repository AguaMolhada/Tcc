using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour , IBuilding , IJobBuilding {
    public Building BuildingData { get; private set; }
    public PlantationSeeds SelectedSeed { get; private set; }

    public int DaysSinceStart { get; private set; }
    public int FarmSize { get; private set; }
    
    public List<Citzen> Workers;

    public int MaxWorkers { get { return (int)(1.25f * FarmSize); } }

    public FarmEventsHandler FarmState;


    public BuildingEventsHandler OnConstruction()
    {
        throw new NotImplementedException();
    }

    public int ShowProgress()
    {
        switch (FarmState)
        {
            case FarmEventsHandler.Planting:
                return (int)Ultility.PercentValue(SelectedSeed.DaysToGrow, DaysSinceStart);
            case FarmEventsHandler.Growing:
                return (int)Ultility.PercentValue(SelectedSeed.DaysToGrow, DaysSinceStart);
            case FarmEventsHandler.Harvesting:
                return (int)Ultility.PercentValue(SelectedSeed.DaysToHarvest, DaysSinceStart);
            default:
                return 0;
        }        
    }

    public void AddResources(GameResources resources)
    {
        resources.Food += SelectedSeed.AmmountFood;
    }

    public bool AssignWorker()
    {
        throw new NotImplementedException();
    }

    private void Update()
    {
        switch (FarmState)
        {
            case FarmEventsHandler.Planting:
                break;
            case FarmEventsHandler.Growing:
                break;
            case FarmEventsHandler.Harvesting:
                if (ShowProgress() >= 100)
                {
                    AddResources(GameController.Instance.City.CityResources);
                }
                FarmState = FarmEventsHandler.Idle;
                break;
            case FarmEventsHandler.Decaying:
                if(GameController.Instance.City.CurrentSeason.SeasonName == "Winter")
                {

                }
                break;
            case FarmEventsHandler.Plage:
                break;
            case FarmEventsHandler.Idle:
                if(GameController.Instance.City.CurrentSeason.SeasonName == "Summer")
                {
                    DaysSinceStart = 0;
                    FarmState = FarmEventsHandler.Planting;
                }
                break;
            default:
                break;
        }
        if(ShowProgress() >= 100)
        {
            AddResources(GameController.Instance.City.CityResources);
        }
    }

}
