// --------------------------------------------------------------------------------------------------------------------
// <copyright file="World.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/AguaMolhada
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

public class World
{
    private Tile[,] tiles;

    public int Width { get; private set; }

    public int Height { get; private set; }

    public World(int width, int height)
    {
        this.Width = width;
        this.Height = height;
        this.tiles = new Tile[width, height];

        for (var x = 0; x < this.Width; x++)
        {
            for (var y = 0; y < this.Height; y++)
            {
                this.tiles[x, y] = new Tile(this, x, y);
            }
        }

        Debug.Log("World Created");
    }

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
                        this.tiles[x, y].Type = Tile.TileType.Grass;
                        break;
                    case 1:
                        this.tiles[x, y].Type = Tile.TileType.Dirt;
                        break;
                    default:
                        this.tiles[x, y].Type = Tile.TileType.Water;
                        break;
                }
            }
        }
    }
    
    public Tile GeTileAt(int x, int y)
    {
        if (x <= this.Width && x >= 0 && y <= this.Height && y >= 0)
        {
            return this.tiles[x, y];
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
