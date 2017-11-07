// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Animal.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used for animal variables.
/// </summary>
[System.Serializable]
public class Animal : MonoBehaviour {
    /// <summary>
    /// Animal spcies Name.
    /// </summary>
    public string AnimalName;
    /// <summary>
    /// Animal Age.
    /// </summary>
    public int Age { get; private set; }
    /// <summary>
    /// Animal Genere.
    /// </summary>
    public Genere AnimalGenere;
    /// <summary>
    /// Time in days that this animal will procreate.
    /// </summary>
    public int TimeToProcreate;
    /// <summary>
    /// Colldown to procreate again.
    /// </summary>
    public int Cooldown { get; private set; }
    /// <summary>
    /// Ammount food Given;
    /// </summary>
    public int HaverstValue;

    public bool Procreate( Animal a ) {
        if ( a.AnimalGenere != AnimalGenere && a.Cooldown == 0 && Cooldown == 0 ) {
            Cooldown = TimeToProcreate;
            return true;
        }
        return false;
    }
}