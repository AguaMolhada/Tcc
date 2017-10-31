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
    public CityData City;

	// Use this for initialization
	void Awake () {
        if (Instance != null)
        {
            Debug.LogError("You can't have 2 Game Controllers");
        }
        Instance = this;

        City.CityName = Ultility.CityNameGenerator();


    }
	
}
