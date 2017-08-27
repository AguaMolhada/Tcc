// --------------------------------------------------------------------------------------------------------------------
// <copyright file="World.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/AguaMolhada
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class World
{
    private Tile[,] _tiles;
    private Dictionary<string, InstalledObject> _installedObjectPrototypes;
    private Action<InstalledObject> cbInstalledObject;

    public int Width { get; private set; }

    public int Height { get; private set; }

    public World(int width, int height)
    {
        this.Width = width;
        this.Height = height;
        this._tiles = new Tile[width, height];

        for (var x = 0; x < this.Width; x++)
        {
            for (var y = 0; y < this.Height; y++)
            {
                this._tiles[x, y] = new Tile(this, x, y);
            }
        }
        CreateInstalledObjectPrototypes();
    }

    private void CreateInstalledObjectPrototypes()
    {
        _installedObjectPrototypes = new Dictionary<string, InstalledObject>
        {
            {"Road", InstalledObject.CreatePrototype("Road", 0.5f, 1, 1,true)}
        };

    }

    public void PlaceInstalledObject(string objectType, Tile t)
    {
        if (!_installedObjectPrototypes.ContainsKey((objectType)))
        {
            Debug.LogError("Object with the key doesn't exist: Key="+objectType);
            return;
        }

        InstalledObject obj = InstalledObject.PlaceInstance(_installedObjectPrototypes[objectType], t);

        if (obj == null)
        {
            return;
        }

        cbInstalledObject?.Invoke(obj);
    }

    public void RegisterInstalledObject(Action<InstalledObject> cbAction)
    {
        cbInstalledObject += cbAction;
    }
    public void UnRegisterInstalledObject( Action<InstalledObject> cbAction ) {
        cbInstalledObject -= cbAction;
    }

    //TODO create a World Generator
    public void RandomizeTiles()
    {
        for (var x = 0; x < this.Width; x++)
        {
            for (var y = 0; y < this.Height; y++)
            {
                var i = Random.Range(0, 3);
                switch (i)
                {
                    case 0:
                        this._tiles[x, y].Type = TileType.Grass;
                        break;
                    case 1:
                        this._tiles[x, y].Type = TileType.Dirt;
                        break;
                    default:
                        this._tiles[x, y].Type = TileType.Water;
                        break;
                }
            }
        }
    }
    
    public Tile GeTileAt(int x, int y)
    {
        if (x <= this.Width && x >= 0 && y <= this.Height && y >= 0)
        {
            return this._tiles[x, y];
        }
        Debug.LogError("Position:{" + x + "," + y + "} out of range");
        return null;
    }

    public Tile GetTileAtWorldCoord(Vector3 coord)
    {
        var x = Mathf.FloorToInt(coord.x);
        var y = Mathf.FloorToInt(coord.y);

        return this.GeTileAt(x, y);
    }


}
