using UnityEngine;
using System.Collections;

public class UnpackedObject {

    Tile tile;

    string objectType;

    float movementCost = 1f;

    int width = 1;
    int height = 1;

    public UnpackedObject( string objectType, float movementCost=1, int width=1, int height=1)
    {
        this.objectType = objectType;
        this.movementCost = movementCost;
        this.width = width;
        this.height = height;
    }

    public UnpackedObject( UnpackedObject proto, Tile tile )
    {
        this.objectType = proto.objectType;
        this.movementCost = proto.movementCost;
        this.width = proto.width;
        this.height = proto.height;
        this.tile = tile;

        tile.UnpackedObject = this;
    }
}
