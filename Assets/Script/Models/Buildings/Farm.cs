// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Farm.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;

/// <summary>
/// Farm Script
/// </summary>
public class Farm : GenericJobBuilding
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
    /// Max workers allowed on the building
    /// </summary>
    public new int MaxWorkers => (int) (1.25f * (FarmSize[0] + FarmSize[1] / 2f));
    /// <summary>
    /// Current Farm State;
    /// </summary>
    public FarmEventsHandler FarmState;
    /// <summary>
    /// Current day stored.
    /// </summary>
    private int _currentDay;

    /// <summary>
    /// Initialize the Farm
    /// </summary>
    /// <param name="seedName">Seed selected on the farm</param>
    public void StartFarm(string seedName)
    {
        if (seedName == "")
        {
            SelectedSeed = null;
            IsReady = false;
            FarmState = FarmEventsHandler.Idle;
            return;
        }
        SelectedSeed = GameController.Instance.GameData.Seeds.Find(a => a.SeedName.Contains(seedName));
        DaysSinceStart = 0;
        IsReady = true;
    }

    private void Start()
    {
        var rnd = new Random();
        StartFarm(GameController.Instance.GameData.Seeds[rnd.Next(0, GameController.Instance.GameData.Seeds.Count)].SeedName);
    }



    /// <inheritdoc />
    public override int ShowProgress()
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
    /// <inheritdoc />
    public override void AddResources(GameResources resources)
    {
        resources.Food += (int) (SelectedSeed.AmmountFood * (FarmSize[0] + FarmSize[1] / 2f));
    }

    /// <summary>
    /// Check current farm state.
    /// </summary>
    private void CheckFarmState()
    {
        if (GameController.Instance.City.Time.CurrentDay != _currentDay)
        {
            _currentDay = GameController.Instance.City.Time.CurrentDay;
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
                    DaysSinceStart = 0;
                }
                break;
            case FarmEventsHandler.Harvesting:
                if (ShowProgress() >= 100)
                {
                    AddResources(GameController.Instance.City.CityResources);
                    FarmState = FarmEventsHandler.Idle;
                    DaysSinceStart = 0;
                }
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
        if (IsReady)
        {
            CheckFarmState();
        }
    }
}