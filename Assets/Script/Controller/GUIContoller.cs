// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GUIController.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
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
