// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MouseController.cs" by="Akapagion">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>
/// The mouse controller.
/// </summary>
public class MouseController : MonoBehaviour
{
    public MouseMode SelectedMouseMode = MouseMode.Normal;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                var x = BuildingGrid.WorldPositionRelatedToGrid(WorldController.MapChunkSize, hit.point);
                if (SelectedMouseMode == MouseMode.Building)
                {
                    BuildThing(x,hit.point);
                    SelectedMouseMode = MouseMode.Normal;
                }
                if (SelectedMouseMode == MouseMode.Demolish)
                {
                    //DO Things
                }
                if (SelectedMouseMode == MouseMode.Normal)
                {
                    if (!GameController.Instance.GameStarted)
                    {
                        GameController.Instance.NewCity("-","Normal",hit.point);
                    }
                }
            }
        }
    }

    private void BuildThing(int[] x,Vector3 pos) 
    {
        GameObject buildTemp = null;
        foreach (var building in GameController.Instance.GameData.Buildings)
        {
            if (building.GetComponent<GenericBuilding>().Type == BuildingController.Instance.SelectedTypeToBuild)
            {
                if (building.GetComponent<GenericBuilding>().BuildingName == BuildingController.Instance.SelectedBuildingName)
                {
                    buildTemp = building;
                }
            }
        }
        if (buildTemp != null)
        {
            Debug.Log(buildTemp);
            BuildingController.Instance.OnConstruction(x[0], x[1], buildTemp, pos);
        }
    }

    public void SetMouseMode(int x)
    {
        SelectedMouseMode = (MouseMode) x;
    }
}