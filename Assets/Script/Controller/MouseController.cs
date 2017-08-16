// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MouseController.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/AguaMolhada
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
    [SerializeField]
    private GameObject circleCursor;

    private Tile.TileType buildType = Tile.TileType.Grass;

    private Vector3 lastFramePosition;
    private Vector3 currFramePosition;

    private Vector3 dragStartPosition;

    private List<GameObject> dragCircles;

    private void Start()
    {
        this.dragCircles = new List<GameObject>();
    }

    private void Update()
    {
        this.currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.CameraMovment();
        this.SelectionArea();
       
    }

    private void SelectionArea()
    {
        //Check if mouse is over something in the UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            this.dragStartPosition = this.currFramePosition;
            Debug.Log("DragStart");
        }

        var xStart = Mathf.FloorToInt(this.dragStartPosition.x);
        var xEnd = Mathf.FloorToInt(this.currFramePosition.x);
        if (xEnd < xStart)
        {
            var temp = xEnd;
            xEnd = xStart;
            xStart = temp;
        }

        var yStart = Mathf.FloorToInt(this.dragStartPosition.y);
        var yEnd = Mathf.FloorToInt(this.currFramePosition.y);
        if (yEnd < yStart)
        {
            var temp = yEnd;
            yEnd = yStart;
            yStart = temp;
        }

        while (this.dragCircles.Count > 0)
        {
            GameObject go = this.dragCircles[0];
            this.dragCircles.RemoveAt(0);
            SimplePool.Despawn(go);
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Start:" + xStart + "/" + yStart + "||| End:" + xEnd + "/" + yEnd);
            for (int x = xStart; x <= xEnd; x++)
            {
                for (int y = yStart; y <= yEnd; y++)
                {
                    Tile tempTile = WorldController.Instance.World.GeTileAt(x, y);
                    if (tempTile != null)
                    {
                        GameObject go = SimplePool.Spawn(this.circleCursor, new Vector3(x, y, 0), Quaternion.identity);
                        go.transform.parent = this.transform;
                        this.dragCircles.Add(go);
                    }
                }
            }
            Debug.Log("Dragging");
        }

        if (Input.GetMouseButtonUp(0)) {
            for ( int x = xStart ; x <= xEnd ; x++ ) {
                for ( int y = yStart ; y <= yEnd ; y++ ) {
                    Tile tempTile = WorldController.Instance.World.GeTileAt(x , y);
                    if ( tempTile != null ) {
                        GameObject go = SimplePool.Spawn(this.circleCursor , new Vector3(x , y , 0) , Quaternion.identity);
                        go.transform.parent = this.transform;
                        this.dragCircles.Add(go);
                        tempTile.Type = buildType;
                    }
                }
            }
            Debug.Log("DragEnded");
        }
    }

    private void CameraMovment()
    {
        
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            var dif = this.lastFramePosition - this.currFramePosition;
            Camera.main.transform.Translate(dif);
        }

        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") *Camera.main.orthographicSize;

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 40f);


        this.lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void ConstructRoadTiles(string type)
    {
        if (type == "Stone")
        {
            buildType = Tile.TileType.Rock;
        }
        else if (type == "Dirt")
        {
            buildType = Tile.TileType.Dirt;
        }
    }

    public void Bulldozer()
    {
        buildType = Tile.TileType.Grass;
    }

}