// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MouseController.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor.Events;

/// <summary>
/// The mouse controller.
/// </summary>
public class MouseController : MonoBehaviour
{


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                var x = BuildingGrid.WorldPositionRelatedToGrid(WorldController.MapChunkSize, hit.point);
                GenericBuilding buildTemp = null;
                foreach (var building in GameController.Instance.GameData.Buildings)
                {
                    if (building.Type == GameController.Instance.SelectedTypeToBuild)
                    {
                        if (building.BuildingName == GameController.Instance.SelectedBuildingName)
                        {
                            buildTemp = building;
                        }
                    }
                }
                if (buildTemp != null)
                {
                    if (buildTemp.OnConstruction(x[0], x[1]) == BuildingEventsHandler.Complete)
                    {
                        return;
                    }
                }
            }
        }
    }
}