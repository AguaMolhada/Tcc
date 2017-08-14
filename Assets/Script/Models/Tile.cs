// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tile.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/AguaMolhada
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

public class Tile
{
    private Action<Tile> cbTypeChanged;

    public enum TileType
    {
        Grass = 0,
        Dirt = 1,
        Rock = 2,
        Water = 3
    }

    public TileType Type
    {
        get
        {
            return this.type;
        }
        set
        {
            this.type = value;
            if (this.cbTypeChanged != null)
            {
                this.cbTypeChanged(this);
            }
        }
    }


    private TileType type = TileType.Grass;

    private LooseObject looseObject;
    private InstalledObject installedObject;

    private World world;

    public int X { get; private set; }
    public int Y { get; private set; }

    public Tile(World world, int x, int y)
    {
        this.world = world;
        this.X = x;
        this.Y = y;

    }

    public void RegisterTileTypeChangedCb(Action<Tile> callback)
    {
        this.cbTypeChanged += callback;
    }

    public void UnregisterTileTypeChangeCb(Action<Tile> callback)
    {
        this.cbTypeChanged -= callback;
    }
}