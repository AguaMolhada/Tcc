using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Townhall : GenericBuilding
{
    public CityData CityData => GameController.Instance.City;
    public Text InfoText;

    private void Start()
    {
        Showinfo("Citzens");
    }

    public void Showinfo(string infoType)
    {
        var infoToShow ="";
        if( infoType == "Citzens")
        {
            infoToShow = CityData.CityHabitants.Aggregate(infoToShow, (current, citzen) => current + (citzen.name + " \t Age:" + citzen.Age + "\n\r"));
        }
        InfoText.text = infoToShow;
    }

}
