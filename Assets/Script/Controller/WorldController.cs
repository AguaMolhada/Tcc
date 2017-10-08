// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorldController.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
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

    public MeshFilter MeshFilter;
    public MeshRenderer MeshRenderer;
    public MeshCollider MeshColiderWorld;

    //Dictionary with all tile sprites used in the game.
    private Dictionary<string, Sprite> _spritesTilesDictionary;

    private Dictionary<Tile, GameObject> _tileToGameObjectMap;
    private Dictionary<Furniture , GameObject> _furnitureToGameObjectMap;

    private void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("You can't have 2 World Controllers");
        }
        Instance = this;

        //Loading all Sprites to make the road and add to the dictionary
        _spritesTilesDictionary = new Dictionary<string, Sprite>();
        var temp = Resources.LoadAll<Sprite>("Tiles");

        foreach (var s in temp)
        {
            _spritesTilesDictionary[s.name] = s;
        }

        // Create the world with the size in parentheses
        this.World = new World(100, 100);
        
        World.RegisterFurniture(OnFurnitureCreated);

        _tileToGameObjectMap = new Dictionary<Tile, GameObject>();
        _furnitureToGameObjectMap = new Dictionary<Furniture, GameObject>();

        OnTileTypeCreated();

    }

    public void DrawMesh(MeshData meshdata)
    {
        MeshFilter.sharedMesh = meshdata.CreateMesh();
        MeshColiderWorld.sharedMesh = meshdata.CreateMesh();
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
                tileGo.GetComponent<SpriteRenderer>().sprite = _spritesTilesDictionary["Grass"];
                break;
            case TileType.Dirt:
                tileGo.GetComponent<SpriteRenderer>().sprite = _spritesTilesDictionary["Dirt"];
                break;
            case TileType.Rock:
                tileGo.GetComponent<SpriteRenderer>().sprite = _spritesTilesDictionary["Rock"];
                break;
            case TileType.Water:
                tileGo.GetComponent<SpriteRenderer>().sprite = _spritesTilesDictionary["Water"];
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    #endregion

    #region FurnitureController
    public void OnFurnitureCreated(Furniture furn)
    {
        // Instanciate all tiles in the respective position
        var furnGameObject = new GameObject {name = furn.ObjectType + "_" + furn.Tile.X + "_" + furn.Tile.Y};

        //Add both data and GameObject to the dictionary
        _furnitureToGameObjectMap.Add(furn , furnGameObject);


        furnGameObject.transform.position = new Vector3(furn.Tile.X , furn.Tile.Y);
        furnGameObject.transform.SetParent(this.transform , true);

        furnGameObject.AddComponent<SpriteRenderer>().sprite = GetSpriteForFurniture(furn);
        furnGameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

        furn.RegisterOnChangedCallback(OnFurnitureChanged);
    }

    private void OnFurnitureChanged( Furniture furn ) {
        if ( _furnitureToGameObjectMap.ContainsKey(furn) == false ) {
            Debug.LogError("Furniture does't exist KEY: " + furn.ObjectType);
        }
        var furnGameObject = _furnitureToGameObjectMap[furn];
        furnGameObject.GetComponent<SpriteRenderer>().sprite = GetSpriteForFurniture(furn);
    }

    private Sprite GetSpriteForFurniture(Furniture furn)
    {
        if (furn.LinksToNeighbour == false)
        {
            return _spritesTilesDictionary[furn.ObjectType];
        }
//TODO
        else
        {
            string spriteName = furn.ObjectType + "_";
            #region Tile Facing Check
            //Checking Neighbours
            //Order N E S W
            var temp = World.GeTileAt(furn.Tile.X, furn.Tile.Y + 1);
            if (temp?.Furniture != null && temp.Furniture.ObjectType == furn.ObjectType)
            {
                spriteName += "N";
            }
            temp = World.GeTileAt(furn.Tile.X +1 , furn.Tile.Y);
            if ( temp?.Furniture != null && temp.Furniture.ObjectType == furn.ObjectType ) {
                spriteName += "E";
            }
            temp = World.GeTileAt(furn.Tile.X , furn.Tile.Y - 1);
            if ( temp?.Furniture != null && temp.Furniture.ObjectType == furn.ObjectType ) {
                spriteName += "S";
            }
            temp = World.GeTileAt(furn.Tile.X - 1 , furn.Tile.Y);
            if ( temp?.Furniture != null && temp.Furniture.ObjectType == furn.ObjectType ) {
                spriteName += "W";
            }
            #endregion

            return _spritesTilesDictionary[spriteName];
        }
    }

    #endregion
   
}