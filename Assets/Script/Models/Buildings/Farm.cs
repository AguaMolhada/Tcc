// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Farm.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : GenericBuilding, IBuilding, IJobBuilding {
    public PlantationSeeds SelectedSeed { get; private set; }

    public int DaysSinceStart { get; private set; }
    public int[] FarmSize { get; private set; }
    public float DecayingStatus { get; private set; }

    public List<Citzen> Workers;

    public int MaxWorkers => (int)(1.25f * (FarmSize[0] + FarmSize[1] / 2f));

    public FarmEventsHandler FarmState;

    /// <summary>
    /// On click to construck will check if have something worng. If the building is clear to build will return Completed
    /// </summary>
    /// <returns>Return the Event related to the building</returns>
    public BuildingEventsHandler OnConstruction() {
        var x = 0; //TODO need to implement something to pick the desired location to build
        var y = 0;

        if ( GameController.Instance.City.CityResources.Wood < LumberCost ) {
            return BuildingEventsHandler.NoLumber;
        }
        if ( GameController.Instance.City.CityResources.Stone < RockCost ) {
            return BuildingEventsHandler.NoStone;
        }
        if ( GameController.Instance.City.CityResources.Iron < MetalCost ) {
            return BuildingEventsHandler.NoIron;
        }
        if ( CheckOverlap(x , y) ) {
            Xpos = x;
            Ypos = y;
            return BuildingEventsHandler.InvalidPos;
        }
        //TODO need to implement check if overlap buildings
        return BuildingEventsHandler.Complete;
    }

    public int ShowProgress() {
        switch ( FarmState ) {
            case FarmEventsHandler.Planting:
                return (int)Ultility.PercentValue(SelectedSeed.DaysToPlant , DaysSinceStart);
            case FarmEventsHandler.Growing:
                return (int)Ultility.PercentValue(SelectedSeed.DaysToGrow , DaysSinceStart);
            case FarmEventsHandler.Harvesting:
                return (int)Ultility.PercentValue(SelectedSeed.DaysToHarvest , DaysSinceStart);
            default:
                return 0;
        }
    }

    public void AddResources( GameResources resources ) {
        resources.Food += (int)(SelectedSeed.AmmountFood * (FarmSize[0] + FarmSize[1] / 2f));
    }

    public bool AssignWorker() {
        throw new NotImplementedException();
    }

    public bool CheckOverlap( int x , int y ) {
        throw new NotImplementedException();
    }
    /// <summary>
    /// Check current farm state.
    /// </summary>
    private void CheckFarmState() {
        switch ( FarmState ) {
            case FarmEventsHandler.Planting:
                if ( ShowProgress() >= 100 ) {
                    FarmState = FarmEventsHandler.Growing;
                    DaysSinceStart = 0;
                }
                break;
            case FarmEventsHandler.Growing:
                if ( ShowProgress() >= 100 ) {
                    FarmState = FarmEventsHandler.Harvesting;
                    ;
                    DaysSinceStart = 0;
                }
                break;
            case FarmEventsHandler.Harvesting:
                if ( ShowProgress() >= 100 ) {
                    AddResources(GameController.Instance.City.CityResources);
                }
                FarmState = FarmEventsHandler.Idle;
                break;
            case FarmEventsHandler.Decaying:
 //               if ( GameController.Instance.City.CurrentSeason.SeasonName == "Winter" ) {
 //                   DecayingStatus += 0.10f;
 //               }
                break;
            case FarmEventsHandler.Plage:
                break;
            case FarmEventsHandler.Idle:
 //               if ( GameController.Instance.City.CurrentSeason.SeasonName == "Summer" ) {
  //                  DaysSinceStart = 0;
   //                 FarmState = FarmEventsHandler.Planting;
    //            }
                break;
            default:
                break;
        }
    }

    private void Update() {
        CheckFarmState();
    }


}