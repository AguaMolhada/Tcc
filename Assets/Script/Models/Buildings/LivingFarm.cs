// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LivingFarm.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;

/// <summary>
/// Script used for living farm
/// </summary>
public class LivingFarm : GenericJobBuilding
{
    /// <summary>
    /// Name of the animal Allowed inside.
    /// </summary>
    public string AnimalAllowed;
    /// <summary>
    /// List of all animals inside.
    /// </summary>
    public List<Animal> AnimalInside;
    /// <summary>
    /// Farm size in X,Y
    /// </summary>
    public int[] FarmSize { get; private set; }
    /// <summary>
    /// Max workers allowed on the building
    /// </summary>
    public new int MaxWorkers => (int)(1.25f * (FarmSize[0] + FarmSize[1] / 2f));
    /// <summary>
    /// Max animals allowed on the farm.
    /// </summary>
    public int MaxAnimals;
    /// <summary>
    /// Oldest animal on the farm.
    /// </summary>
    private Animal _oldestAnimal;

    /// <inheritdoc />
    public override int ShowProgress()
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
        return (int) Ultility.PercentValue(MaxAnimals, AnimalInside.Count);
    }

    /// <inheritdoc />
    public override void AddResources(GameResources x)
    {
        x.Food = _oldestAnimal.HaverstValue;
    }

}