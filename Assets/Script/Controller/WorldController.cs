// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorldController.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/AguaMolhada
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class WorldController : MonoBehaviour
{

    public static WorldController Instance { get; protected set; }

    public World World { get; protected set; }

    [SerializeField]
    private Sprite[] _sprites; //TODO
    [SerializeField]
    private Sprite _road; //TODO

    private Dictionary<Tile, GameObject> _tileToGameObjectMap;
    private Dictionary<InstalledObject , GameObject> _installedObjectToGameObjectMap;

    private void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("You can't have 2 World Controllers");
        }
        Instance = this;

        // Create the world with the size in parentheses
        this.World = new World(100, 100);
        
        World.RegisterInstalledObject(OnInstalledObjectCreated);

        _tileToGameObjectMap = new Dictionary<Tile, GameObject>();
        _installedObjectToGameObjectMap = new Dictionary<InstalledObject, GameObject>();

        OnTileTypeCreated();


        this.World.RandomizeTiles();
    }

    #region TileController
    private void OnTileTypeCreated()
    {
        for ( var x = 0 ; x < this.World.Width ; x++ ) {
            for ( var y = 0 ; y < this.World.Height ; y++ ) {
                // Instanciate all tiles in the respective position
                Tile tileData = this.World.GeTileAt(x , y);
                var newTileObj = new GameObject { name = "Tile_" + x + "_" + y };
                //Add both data and GameObject to the dictionary
                _tileToGameObjectMap.Add(tileData , newTileObj);


                newTileObj.transform.position = new Vector3(tileData.X , tileData.Y);
                newTileObj.transform.SetParent(this.transform , true);

                newTileObj.AddComponent<SpriteRenderer>();

                tileData.RegisterTileTypeChangedCb(OnTileTypeChanged);
            }
        }
    }
    
    private void OnTileTypeChanged(Tile tileData)
    {
        if (_tileToGameObjectMap.ContainsKey(tileData) == false) {
            return;
        }

        GameObject tileGo = _tileToGameObjectMap[tileData];

        if (tileGo == null)
        {
            return;
        }

        switch (tileData.Type)
        {
            case TileType.Grass:
                tileGo.GetComponent<SpriteRenderer>().sprite = this._sprites[(int) TileType.Grass];
                break;
            case TileType.Dirt:
                tileGo.GetComponent<SpriteRenderer>().sprite = this._sprites[(int) TileType.Dirt];
                break;
            case TileType.Rock:
                tileGo.GetComponent<SpriteRenderer>().sprite = this._sprites[(int) TileType.Rock];
                break;
            case TileType.Water:
                tileGo.GetComponent<SpriteRenderer>().sprite = this._sprites[(int) TileType.Water];
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    #endregion

    #region InstalledObjectController
    public void OnInstalledObjectCreated(InstalledObject obj)
    {
        // Instanciate all tiles in the respective position
        var objGo = new GameObject {name = obj.ObjectType + "_" + obj.Tile.X + "_" + obj.Tile.Y};

        //Add both data and GameObject to the dictionary
        _installedObjectToGameObjectMap.Add(obj , objGo);


        objGo.transform.position = new Vector3(obj.Tile.X , obj.Tile.Y);
        objGo.transform.SetParent(this.transform , true);

        objGo.AddComponent<SpriteRenderer>().sprite = _road;
        objGo.GetComponent<SpriteRenderer>().sortingOrder = 1;

        obj.RegisterOnChangedCallback(OnInstalledObjectChanged);
    }

    private void OnInstalledObjectChanged(InstalledObject obj)
    {
        
    }
    #endregion
}