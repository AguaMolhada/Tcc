using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once InconsistentNaming
public class GUIContoller : MonoBehaviour
{
    public static GUIContoller Instance;

    public string BuildingType;
    public int SelectedBuilding = -1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SelectBuilding(int x)
    {
        SelectedBuilding = x;
    }

}
