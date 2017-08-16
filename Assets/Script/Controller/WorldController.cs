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
    private Sprite[] sprites;

    private Dictionary<Tile, GameObject> tileToGameObjectMap;


    private void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("You can't have 2 World Controllers");
        }
        Instance = this;

        // Create the world with the size in parentheses
        this.World = new World(100, 100);
        
        tileToGameObjectMap = new Dictionary<Tile, GameObject>();

        for (var x = 0; x < this.World.Width; x++)
        {
            for (var y = 0; y < this.World.Height; y++)
            {
                // Instanciate all tiles in the respective position
                var tileData = this.World.GeTileAt(x, y);
                var newTileObj = new GameObject { name = "Tile_" + x + "_" + y };
                //Add both data and GameObject to the dictionary
                tileToGameObjectMap.Add(tileData,newTileObj);

                newTileObj.transform.position = new Vector3(tileData.X, tileData.Y);
                newTileObj.transform.SetParent(this.transform, true);

                newTileObj.AddComponent<SpriteRenderer>();

                // Funcao anonima que recebe tile como paramentro e chama OnTileTypeChanged
                tileData.RegisterTileTypeChangedCb(OnTileTypeChanged);
            }
        }
        this.World.RandomizeTiles();
    }

    private void OnTileTypeChanged(Tile tileData)
    {
        if (!tileToGameObjectMap.ContainsKey(tileData))
        {
            return;
        }
        GameObject tileGo = tileToGameObjectMap[tileData];
        switch (tileData.Type)
        {
            case Tile.TileType.Grass:
                tileGo.GetComponent<SpriteRenderer>().sprite = this.sprites[(int)Tile.TileType.Grass];
                break;
            case Tile.TileType.Dirt:
                tileGo.GetComponent<SpriteRenderer>().sprite = this.sprites[(int)Tile.TileType.Dirt];
                break;
            case Tile.TileType.Rock:
                tileGo.GetComponent<SpriteRenderer>().sprite = this.sprites[(int)Tile.TileType.Rock];
                break;
            case Tile.TileType.Water:
                tileGo.GetComponent<SpriteRenderer>().sprite = this.sprites[(int)Tile.TileType.Water];
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}