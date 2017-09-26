// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Furniture.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/AguaMolhada
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;


// InstalledObjects == Roads,stones,trees,
public class Furniture
{
    //Base tile of the object
    public Tile Tile { get; private set; }
    //ammount tiles used by the object;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public bool LinksToNeighbour { get; private set; }

    private Action<Furniture> cbOnChanged;

    //Type to know wich sprite will be redered;
    public string ObjectType { get; private set; }

    // Slow Percent (0== cant pass,1 == no slow, 2 50% slow)
    public float MovementCost { get; private set; }

    // Doesn't allow create dummy Instances on others places
    protected Furniture()
    {
        
    }

    public static Furniture CreatePrototype(string objectType, float movementCost,int widht,int height, bool links)
    {
        var obj = new Furniture
        {
            ObjectType = objectType,
            MovementCost = movementCost,
            Width = widht,
            Height = height,
            LinksToNeighbour = links
        };

        return obj;
    }
    public static Furniture PlaceInstance( Furniture proto, Tile tile )
    {
        var obj = new Furniture
        {
            ObjectType = proto.ObjectType,
            MovementCost = proto.MovementCost,
            Width = proto.Width,
            Height = proto.Height,
            LinksToNeighbour = proto.LinksToNeighbour,
            Tile = tile
        };

        if (tile.PlaceObject(obj) == false)
        {
            return null;
        }

        if (obj.LinksToNeighbour)
        {

            var x = tile.X;
            var y = tile.Y;
            var temp = tile.World.GeTileAt(x ,y + 1);
            if ( temp?.Furniture != null && temp.Furniture.ObjectType == obj.ObjectType )
            {
                temp.Furniture.cbOnChanged(temp.Furniture);
            }
            temp = tile.World.GeTileAt(x + 1 , y);
            if ( temp?.Furniture != null && temp.Furniture.ObjectType == obj.ObjectType ) {
                temp.Furniture.cbOnChanged(temp.Furniture);
            }
            temp = tile.World.GeTileAt(x ,y - 1);
            if ( temp?.Furniture != null && temp.Furniture.ObjectType == obj.ObjectType ) {
                temp.Furniture.cbOnChanged(temp.Furniture);
            }
            temp = tile.World.GeTileAt(x - 1 , y);
            if ( temp?.Furniture != null && temp.Furniture.ObjectType == obj.ObjectType ) {
                temp.Furniture.cbOnChanged(temp.Furniture);
            }
        }

        //if tile.PlaceObject return false, failed to place the object on the respective tile and return null;
        return obj;
    }

    public void RegisterOnChangedCallback(Action<Furniture> cbAction)
    {
        cbOnChanged += cbAction;
    }
    public void UnRegisterOnChangedCallback( Action<Furniture> cbAction ) {
        cbOnChanged -= cbAction;
    }

}
