// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InstalledObject.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/AguaMolhada
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;


// InstalledObjects == Roads,stones,trees,
public class InstalledObject
{
    //Base tile of the object
    public Tile Tile { get; private set; }
    //ammount tiles used by the object;
    public int Width { get; private set; }
    public int Height { get; private set; }

    private Action<InstalledObject> cbOnChanged;

    //Type to know wich sprite will be redered;
    public string ObjectType { get; private set; }

    // Slow Percent (0== cant pass,1 == no slow, 2 50% slow)
    public float MovementCost { get; private set; }

    // Doesn't allow create dummy Instances on others places
    protected InstalledObject()
    {
        
    }

    public static InstalledObject CreatePrototype(string objectType, float movementCost,int widht,int height)
    {
        var obj = new InstalledObject
        {
            ObjectType = objectType,
            MovementCost = movementCost,
            Width = widht,
            Height = height
        };

        return obj;
    }
    public static InstalledObject PlaceInstance( InstalledObject proto, Tile tile ) {
        var obj = new InstalledObject
        {
            ObjectType = proto.ObjectType,
            MovementCost = proto.MovementCost,
            Width = proto.Width,
            Height = proto.Height,
            Tile = tile
    };

        //if tile.PlaceObject return false, failed to place the object on the respective tile and return null;
        return tile.PlaceObject(obj) == false ? null : obj;
    }

    public void RegisterOnChangedCallback(Action<InstalledObject> cbAction)
    {
        cbOnChanged += cbAction;
    }
    public void UnRegisterOnChangedCallback( Action<InstalledObject> cbAction ) {
        cbOnChanged -= cbAction;
    }

}
