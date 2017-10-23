// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LivingFarm.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingFarm : GenericBuilding , IBuilding , IJobBuilding
{
    public string AnimalAllowed;
    public List<Animal> AnimalInside;
    public int[] FarmSize { get; private set; }
    public List<Citzen> Workers;

    public int MaxWorkers => (int)(1.25f * (FarmSize[0] + FarmSize[1] / 2f));

    public int MaxAnimals;
    private Animal _oldestAnimal;

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
    //TODO
    public bool CheckOverlap(int x, int y)
    {
        throw new System.NotImplementedException();
    }

    public int ShowProgress()
    {
        if (AnimalInside.Count > MaxAnimals)
        {
            var oldestAge = 0;
            foreach (var animal in AnimalInside)
            {
                if (animal.Age > oldestAge)
                {
                    _oldestAnimal = animal;
                    oldestAge = animal.Age;
                }
            }
            AddResources(GameController.Instance.City.CityResources);
            AnimalInside.Remove(_oldestAnimal);
            _oldestAnimal = null;
        }
        return (int)Ultility.PercentValue(MaxAnimals, AnimalInside.Count);
    }

    public void AddResources(GameResources x)
    {
        x.Food = _oldestAnimal.HaverstValue;
    }

    public bool AssignWorker()
    {
        throw new System.NotImplementedException();
    }
}
