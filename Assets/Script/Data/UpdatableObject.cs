using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatableObject : ScriptableObject
{

    public bool AutoUpdate;

    protected virtual void OnValidate()
    {
        
    }
    
}
