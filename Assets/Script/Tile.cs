using UnityEngine;
using System.Collections;
using System;

public enum TileType
{
    Empty,
    Floor
}

public class Tile {

    Action<Tile> tileTypeChangedDelegate;

    TileType type;
    public TileType Type{
        get
        {
            return type;
        }
        set
        {
            TileType oldType = type;
            type = value;
            //Callback
            if ( tileTypeChangedDelegate != null && oldType != type ) {
                tileTypeChangedDelegate(this);
            }
        }
    }
    PackedObject packedObject;
    UnpackedObject unpackedObject;
    public UnpackedObject UnpackedObject
    {
        get
        {
            return unpackedObject;
        }
        set
        {
            unpackedObject = value;
        }
    }

    World world;

    int x;
    public int X
    {
        get
        {
            return x;
        }
    }

    int y;
    public int Y
    {
        get
        {
            return y;
        }
    }

    public Tile( World world, int x, int y )
    {
        this.world = world;
        this.x = x;
        this.y = y;
    }

    public void RegisterTileTypeChangeDelegate ( Action<Tile> callBack )
    {
        tileTypeChangedDelegate += callBack;
    }

    public void UnRegisterTileTypeChangeDelegate(Action<Tile> callBack)
    {
        tileTypeChangedDelegate -= callBack;
    }
}
