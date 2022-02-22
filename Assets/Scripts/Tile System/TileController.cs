using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    public static TileController Instance {get; private set;}
    [SerializeField]
    public Tilemap hardMap;
    [SerializeField]
    public Tilemap interactiveMap;
    public Grid grid;

    [SerializeField]
    private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;
    private Dictionary<Vector3Int, TileData> instancedTileData;

    [SerializeField]
    private Tile selector;
    private Vector3Int previousMouseGridPosition = new Vector3Int();

    void Awake () {
        if (Instance != null) {
            Debug.LogError("There should only ever be one TileController");
            Destroy(this);
        } else {
            Instance = this;
        }

        grid = GetComponent<Grid>();

        InitializeTileData();
    }
    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = hardMap.WorldToCell(mousePosition);

            TileBase clickedTile = hardMap.GetTile(gridPosition);
            bool passable = dataFromTiles[clickedTile].passable;
        }
        SetSelectorOverMousePosition();
    }

    private void InitializeTileData () {
        BoundsInt bounds = hardMap.cellBounds;
        TileBase[] allTiles = hardMap.GetTilesBlock(bounds);

        dataFromTiles = new Dictionary<TileBase, TileData>();
        instancedTileData = new Dictionary<Vector3Int, TileData>();

        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
        for (int x = hardMap.origin.x; x < hardMap.size.x; x++)
        {
            for (int y = hardMap.origin.y; y < hardMap.size.y; y++)
            {
                TileBase tile = hardMap.GetTile(new Vector3Int(x,y,0));
                if (tile!= null) {
                    foreach (TileData tileData in tileDatas) {
                        if(tileData.tiles.IndexOf(tile) != -1) {
                            TileData newTileData = ScriptableObject.Instantiate(tileData);
                            instancedTileData.Add(new Vector3Int(x, y, 0), newTileData);
                        }
                    }
                }
            }
        }
    }

    public TileData GetTileData (Vector2 worldPosition) {
        Vector3Int gridPosition = hardMap.WorldToCell(worldPosition);
        TileData tileData = instancedTileData[gridPosition];
        return tileData;
    }

    public void SetSelectorOverMousePosition () {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = grid.WorldToCell(mouseWorldPosition);
        if (!gridPosition.Equals(previousMouseGridPosition) && instancedTileData[gridPosition].selectable) {
            interactiveMap.SetTile(previousMouseGridPosition, null);
            interactiveMap.SetTile(gridPosition, selector);
            previousMouseGridPosition = gridPosition;
        } else if (!instancedTileData[gridPosition].selectable) {
            if (interactiveMap.GetTile(gridPosition) != null || interactiveMap.GetTile(previousMouseGridPosition) != null) {
                interactiveMap.SetTile(gridPosition, null);
                interactiveMap.SetTile(previousMouseGridPosition, null);
            }
        }
    }
}
