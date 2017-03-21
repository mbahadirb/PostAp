using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class WorldController : MonoBehaviour {

    static WorldController instance;

    Dictionary<Tile, GameObject> tileToGameObjectDictionary;

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

        tileToGameObjectDictionary = new Dictionary<Tile, GameObject>();
        
        //world.RandomizeTiles();
        SetupTileGameObjects();
        world.RandomizeTiles();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void DestroyAllTileGameObjects()
    {
        while (tileToGameObjectDictionary.Count > 0)
        {
            Tile tileData = tileToGameObjectDictionary.Keys.First();
            GameObject tileGO = tileToGameObjectDictionary[tileData];

            tileToGameObjectDictionary.Remove(tileData);

            tileData.UnRegisterTileTypeChangeDelegate(OnTileTypeChange);
            Destroy(tileGO);

        }
    }

    void OnTileTypeChange( Tile tileData )
    {
        if (tileToGameObjectDictionary.ContainsKey(tileData) == false )
        {
            Debug.LogError("Doesn't contain the tile data.");
            return;
        }

        GameObject tileGO = tileToGameObjectDictionary[tileData];

        if ( tileGO == null )
        {
            Debug.LogError("Tile Game Object didn't register correctly.");
            return;
        }

        if (tileData.Type == TileType.Floor)
        {
            tileGO.GetComponent<SpriteRenderer>().sprite = floorSprite;
        }
        else if (tileData.Type == TileType.Empty)
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
                tileData.RegisterTileTypeChangeDelegate( OnTileTypeChange);
                tileToGameObjectDictionary.Add(tileData, tileGO);

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
