// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Citzen.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citzen : MonoBehaviour {

    public string Name { get; protected set; }
    public int Age { get; protected set; }
    public CitzenGenere Genere { get; protected set; }
    public float DeathChance { get; protected set; }
    public float Saturation { get; protected set; }
    public float Happines { get; protected set; }

    public Job Profession;

    public Building House;
    public Building JobLocation;
    
    public void HappyBirthday()
    {
        var x = 0f;
        if (Age > 20)
        {
            x = 0.4f;
        }
        else if(Age >= 20 && Age <60)
        {
            x = 0.5f;
        }
        else if (Age >=60)
        {
            x = 0.8f;
        }
        DeathChance = (float) (0.01f * Mathf.Pow(1, 2) + Age * x);
    }

}
