using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once InconsistentNaming
public class GUIContoller : MonoBehaviour
{
    public static GUIContoller Instance;

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

    public void SelectBuildingType(int x)
    {
        GameController.Instance.SelectedTypeToBuild = (TypeBuilding) x;
    }

    public void SelectBuilding(string x)
    {
        GameController.Instance.SelectedBuildingName = x;
    }

}
