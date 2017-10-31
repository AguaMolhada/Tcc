// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameController.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController Instance { get; protected set; }

    public GameObject PeoplePrefab;
    public CityData City;

	// Use this for initialization
	void Start () {
        if (Instance != null)
        {
            Debug.LogError("You can't have 2 Game Controllers");
        }
        Instance = this;

        NewCity("Test City","Easy");
    }

    private void NewCity(string CName,string DifMode)
    {
        City.name = CName;
        switch (DifMode)
        {
            case "Easy":
                for (int i = 0; i < 15; i++)
                {
                    var cityTemp = Instantiate(PeoplePrefab, transform.position, Quaternion.identity);
                    cityTemp.GetComponent<Citzen>().Init();
                    City.CityHabitants.Add(cityTemp.GetComponent<Citzen>());
                }
                City.CityResources.Wood = 400;
                City.CityResources.Stone = 400;
                City.CityResources.Iron = 100;
                City.CityResources.Food = 400;
                break;
            case "Normal":
                for ( int i = 0 ; i < 10 ; i++ ) {
                    City.CityHabitants.Add(new Citzen());
                }
                City.CityResources.Wood = 250;
                City.CityResources.Stone = 250;
                City.CityResources.Iron = 10;
                City.CityResources.Food = 300;
                break;
            case "Hard":
                for ( int i = 0 ; i < 8 ; i++ ) {
                    City.CityHabitants.Add(new Citzen());
                }
                City.CityResources.Wood = 100;
                City.CityResources.Stone = 100;
                City.CityResources.Iron = 0;
                City.CityResources.Food = 200;
                break;
        }
    }

}
