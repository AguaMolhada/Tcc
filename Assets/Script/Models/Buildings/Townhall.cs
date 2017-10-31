using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Townhall : GenericBuilding
{
    public CityData CityData => GameController.Instance.City;
    public Text InfoText;

    void Start()
    {
        Showinfo("Citzens");
    }

    public void Showinfo(string infoType)
    {
        var infoToShow ="";
        if( infoType == "Citzens")
        {
            foreach (var citzen in CityData.CityHabitants)
            {
                infoToShow += citzen.name + " \t Age:"+citzen.Age+"\n\r";
            }
        }
    }

}
