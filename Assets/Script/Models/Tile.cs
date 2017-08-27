// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tile.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/AguaMolhada
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;

public enum TileType {
    Grass = 0,
    Dirt = 1,
    Rock = 2,
    Water = 3
}

public class Tile
{
    private Action<Tile> _cbTypeChanged;

    public TileType Type
    {
        get
        {
            return this._type;
        }
        set
        {
            this._type = value;
            _cbTypeChanged?.Invoke(this);
        }
    }


    private TileType _type = TileType.Grass;

    private LooseObject _looseObject;
    public InstalledObject InstalledObject { get; private set; }

    private World _world;

    public int X { get; private set; }
    public int Y { get; private set; }

    public Tile(World world, int x, int y)
    {
        this._world = world;
        this.X = x;
        this.Y = y;

    }
    
    public void RegisterTileTypeChangedCb(Action<Tile> callback)
    {
        this._cbTypeChanged += callback;
    }

    public void UnregisterTileTypeChangeCb(Action<Tile> callback)
    {
        var cbTypeChanged = this._cbTypeChanged;
        if (cbTypeChanged != null)
        {
            this._cbTypeChanged -= callback;
        }
    }

    public bool PlaceObject(InstalledObject objInstance)
    {
        //Removing the object
        if (objInstance == null)
        {
            InstalledObject = null;
            return true;
        }
        if (InstalledObject != null)
        {
            Debug.LogError("Tile Already Have an object Installed");
            return false;
        }
        InstalledObject = objInstance;
        return true;
    }

}