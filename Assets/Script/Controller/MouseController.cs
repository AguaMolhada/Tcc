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
                //draw invisible ray cast/vector
                Debug.DrawLine(ray.origin, hit.point);
                int[] x = BuildingGrid.WorldPositionRelatedToGrid(WorldController.MapChunkSize, hit.point);
                //log hit area to the console

                Debug.Log(WorldController.MapBuildingGrid[x[0],x[1]]);
            }
        }
    }

    public void SetBuildingMode(string buildigType)
    {
        
    }

}