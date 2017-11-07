using UnityEngine;

/// <summary>
/// Dummy class used on all Data
/// </summary>
public class UpdatableObject : ScriptableObject
{

    public bool AutoUpdate;

    protected virtual void OnValidate()
    {
        
    }
    
}
