using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour {

    public GameObject circleCursorPrefab;

    TileType buildModeTile;

    bool buildModeIsObjects = false;

    Tile tileDragStart;
    Tile tileUnderMouse;

    Vector3 lastFrameMousePosition;
    Vector3 dragStartPosition;
    Vector3 currentFrameMousePosition;

    List<GameObject> dragPreviewGameObjects;

    // Use this for initialization
    void Start () {
        dragPreviewGameObjects = new List<GameObject>();

        SimplePool.Preload(circleCursorPrefab, 100);
	}
	
	// Update is called once per frame
	void Update () {
        currentFrameMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentFrameMousePosition.z = 0;
        tileUnderMouse = WorldController.Instance.GetTileAtWorldCoord(currentFrameMousePosition);

        //UpdateCursor();
        UpdateDragging();
        UpdateCameraMovement();

        lastFrameMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFrameMousePosition.z = 0;
        tileUnderMouse = null;
        currentFrameMousePosition = Vector3.zero;


    }

    //void UpdateCursor()
    //{
        
    //    if (tileUnderMouse != null)
    //    {
    //        circleCursor.SetActive(true);
    //        Vector3 cursorPos = new Vector3(tileUnderMouse.X, tileUnderMouse.Y, 0);
    //        circleCursor.transform.position = cursorPos;
    //    }
    //    else
    //    {
    //        circleCursor.SetActive(false);
    //    }
    //}

    void UpdateDragging ()
    {
        
        if ( EventSystem.current.IsPointerOverGameObject() )
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() )
        {
            dragStartPosition = currentFrameMousePosition;
        }
        int startX = Mathf.FloorToInt(dragStartPosition.x);
        int endX = Mathf.FloorToInt(currentFrameMousePosition.x);
        int startY = Mathf.FloorToInt(dragStartPosition.y);
        int endY = Mathf.FloorToInt(currentFrameMousePosition.y);

        if (endX < startX)
        {
            int temp = endX;
            endX = startX;
            startX = temp;
        }


        if (endY < startY)
        {
            int temp = endY;
            endY = startY;
            startY = temp;
        }

        while(dragPreviewGameObjects.Count > 0)
        {
            GameObject go = dragPreviewGameObjects[0];
            dragPreviewGameObjects.RemoveAt(0);
            SimplePool.Despawn(go);
        }

        if (Input.GetMouseButton(0))
        {
            if (dragStartPosition.z == -1)
            {
                return;
            }
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    Tile t = WorldController.Instance.World.GetTileAt(x, y);
                    if (t != null)
                    {
                        GameObject go = (GameObject)SimplePool.Spawn(circleCursorPrefab, new Vector3(x,y,0), Quaternion.identity );
                        dragPreviewGameObjects.Add(go);
                        go.transform.SetParent(this.transform);

                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (dragStartPosition.z == -1)
            {
                return;
            }

            for (int x = startX; x <= endX; x++)
             {
                for (int y = startY; y <= endY; y++)
                {
                    Tile t = WorldController.Instance.World.GetTileAt(x, y);
                   
                    if (t != null)
                    {
                        if (buildModeIsObjects == true)
                        {
                            //FIXME Only walls will be built at this mode ATM

                        }
                        else
                        {
                            t.Type = buildModeTile;
                        }
                    }
                }
            }
            dragStartPosition = new Vector3( -1, -1, -1);
            
        }
    }

    void UpdateCameraMovement()
    {
        if (Input.GetMouseButton(2))
        {
            Vector3 dif = lastFrameMousePosition - currentFrameMousePosition;
            Camera.main.transform.Translate(dif);
        }


        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 25f);


    }

    public void SetModeBuildFloor()
    {
        buildModeIsObjects = false;
        buildModeTile = TileType.Floor;
    }

    public void SetModeDeleteFloor()
    {
        buildModeIsObjects = false;
        buildModeTile = TileType.Empty;
    }

    public void SetModeBuildWall()
    {
        buildModeIsObjects = true;
    }
}
