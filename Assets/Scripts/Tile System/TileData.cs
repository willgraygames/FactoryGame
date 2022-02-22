using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "ScriptableObjects/TileData")]
public class TileData : ScriptableObject
{
    public List<TileBase> tiles;                       //The tilemap tiles for this tileType
    public TileType tileType;                      //The tile type
    [Header("Player Affecting")]
    public bool passable;                          //Whether the player is able to move through/over this tile
    public bool selectable;                        //Whether the tile is selectable
    public float walkingSpeed = 1;                 //How fast the player moves over this tile
    public float damagePerSecond = 0;              //How much damage the player takes per second by standing on this tile

    [Header("Resources")]
    public bool hasResources = false;
    public int resourcesRemaining = 0;

    [Header("Pollution")]
    public float currentPollution;                 //How much pollution this tile currently has
    public int pollutionThreshold;                 //How much pollution this tile can take before switching to a more polluted version
    public float pollutionAbsorption;              //How fast this tile absorbs and removes pollution
    public float pollutionDiffusion;               //How fast this tile diffuses pollution to other tiles around it


}
