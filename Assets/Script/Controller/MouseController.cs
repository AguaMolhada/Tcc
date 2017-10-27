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
    [SerializeField]
    private GameObject _circleCursor;


    private Vector3 _lastFramePosition;
    private Vector3 _currFramePosition;

    private Vector3 _dragStartPosition;

    private List<GameObject> _dragCircles;

    private void Start()
    {
        this._dragCircles = new List<GameObject>();
    }

    private void Update()
    {
        this._currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
            this._dragStartPosition = this._currFramePosition;
            Debug.Log("DragStart");
        }

        var xStart = Mathf.FloorToInt(this._dragStartPosition.x);
        var xEnd = Mathf.FloorToInt(this._currFramePosition.x);
        if (xEnd < xStart)
        {
            var temp = xEnd;
            xEnd = xStart;
            xStart = temp;
        }

        var yStart = Mathf.FloorToInt(this._dragStartPosition.y);
        var yEnd = Mathf.FloorToInt(this._currFramePosition.y);
        if (yEnd < yStart)
        {
            var temp = yEnd;
            yEnd = yStart;
            yStart = temp;
        }

        while (this._dragCircles.Count > 0)
        {
            var go = this._dragCircles[0];
            this._dragCircles.RemoveAt(0);
            SimplePool.Despawn(go);
        }

        if (Input.GetMouseButton(0))
        {

        }

        if (Input.GetMouseButtonUp(0)) {

        }
    }

    private void CameraMovment()
    {
        
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            var dif = this._lastFramePosition - this._currFramePosition;
            Camera.main.transform.Translate(dif);
        }

        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") *Camera.main.orthographicSize;

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 40f);


        this._lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SetMode_ConstructRoadTiles(string objecType)
    {

    }

    public void SetMode_Bulldozer()
    {

    }


}