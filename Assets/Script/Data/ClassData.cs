using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#TODO CHECK THIS Acho q é inutil
/// <summary>
/// 
/// </summary>
[CreateAssetMenu()]
public class ClassData : ScriptableObject {

    public string ClassName;
    [Range(0,1)]
    public float Speed;

    // Is Possible to have
    public bool Male;
    public bool Famale;

}
