using UnityEngine;
using System.Collections;

public class World{

    Tile[,] tiles;

    int width;
    public int Width
    {
        get
        {
            return width;
        }
    }

    int height;
    public int Height
    {
        get
        {
            return height;
        }
    }

    public World ( int width = 100, int height = 100)
    {
        this.width = width;
        this.height = height;

        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(this, x, y);
            }
        }
        Debug.Log("A world "+width+"x"+height+" created.");
    }

    public Tile GetTileAt(int x, int y)
    {
        if (x > width || x < 0 || y > height || y < 0)
        {
            Debug.Log( "Tile you tried to access is out of bounds." );
            return null;
        }
        return tiles[x, y];
    }

    public void RandomizeTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(Random.Range(0, 2) == 0)
                {
                    tiles[x, y].Type = Tile.TileType.Empty;
                } else
                {
                    tiles[x, y].Type = Tile.TileType.Floor;
                }
            }
        }
    }
}
