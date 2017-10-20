using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController Instance { get; protected set; }
    public CityData City;

    public Dictionary<string, Season> Seasons;
    


	// Use this for initialization
	void Start () {
        if (Instance != null)
        {
            Debug.LogError("You can't have 2 Game Controllers");
        }
        Instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
