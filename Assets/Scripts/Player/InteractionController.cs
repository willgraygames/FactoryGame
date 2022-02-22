using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class InteractionController : MonoBehaviour
{
    void Update () {
        if (Input.GetMouseButtonDown(0)) {
            TileData tile = TileController.Instance.GetTileData(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            tile.resourcesRemaining--;
        }
    }

}
