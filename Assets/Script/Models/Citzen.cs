// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Citzen.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>
/// Class for all NPC's
/// </summary>
public class Citzen : MonoBehaviour
{
    /// <summary>
    /// NPC name
    /// </summary>
    public string Name { get; protected set; }
    /// <summary>
    /// NPC Age
    /// </summary>
    public int Age { get; protected set; }
    /// <summary>
    /// NPC Genere
    /// </summary>
    public Genere Genere { get; protected set; }
    /// <summary>
    /// NPC Death Change, more old more chance to die
    /// </summary>
    public float DeathChance { get; protected set; }
    /// <summary>
    /// NPC Hunger value. If stay 0 for 1 day he will die doesn't matter the death chance
    /// </summary>
    public float Saturation { get; protected set; }
    /// <summary>
    /// NPC Happiness, more happier more efficient on the job
    /// </summary>
    public float Happiness { get; protected set; }
    /// <summary>
    /// NPC Job, children will transform in studendt if a chuch is avaliable and have 1 professor
    /// </summary>
    public Job Profession;
    /// <summary>
    /// NPC House
    /// </summary>
    public GenericBuilding House;
    /// <summary>
    /// NPC job Location, children doesn't work and student will "work" on the chuch to learn more things. (you'll be able to "Graduate" the children)
    /// </summary>
    public GenericBuilding JobLocation;
    
    /// <summary>
    /// Each year the age will increment (O RLY?!) and the death chance will adjust automaticaly.
    /// </summary>
    public void HappyBirthday()
    {
        Age++;
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
