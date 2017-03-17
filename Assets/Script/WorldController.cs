using UnityEngine;
using System.Collections;
using System;

public class WorldController : MonoBehaviour {

    static WorldController instance;

    public static WorldController Instance {
        get
        {
            return instance;
        }

        protected set {
            instance = value;
        }
    }

    World world;
    public World World {
        get
        {
            return world;
        }

        protected set
        {
            world = value;
        }
    }
    public Sprite floorSprite;
    // Use this for initialization
    void Awake()
    {
        if (instance != null)
        {

        }
        instance = this;
    }

	void Start () {

        

        world = new World();
        
        //world.RandomizeTiles();
        SetupTileGameObjects();
        world.RandomizeTiles();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTileTypeChange( Tile tileData, GameObject tileGO )
    {
        if (tileData.Type == Tile.TileType.Floor)
        {
            tileGO.GetComponent<SpriteRenderer>().sprite = floorSprite;
        }
        else if (tileData.Type == Tile.TileType.Empty)
        {
            tileGO.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            Debug.Log( "Unrecognised tile type." );
        }
    }

    void SetupTileGameObjects()
    {
        for (int x = 0; x < world.Width; x++)
        {
            for (int y = 0; y < world.Height; y++)
            {
                GameObject tileGO = new GameObject();
                tileGO.AddComponent<SpriteRenderer>();

                Tile tileData = world.GetTileAt(x, y);
                tileData.RegisterTileTypeChangeDelegate((tile) => { OnTileTypeChange(tile, tileGO); });

                tileGO.name = "Tile_" + x + "_" + y;
                tileGO.transform.position = new Vector3(tileData.X, tileData.Y, 0);
                tileGO.transform.SetParent(this.transform, true);
                
            }
        }
    }

    public Tile GetTileAtWorldCoord(Vector3 coord)
    {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);
        return world.GetTileAt(x,y);
    }
}
