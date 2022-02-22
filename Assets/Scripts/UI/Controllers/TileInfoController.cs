using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileInfoController : MonoBehaviour
{
    [SerializeField]
    private GameObject tileInfoContainer;
    [SerializeField]
    private TextMeshProUGUI tileTypeText;
    [SerializeField]
    private TextMeshProUGUI buildableText;
    [SerializeField]
    private TextMeshProUGUI passableText;
    [SerializeField]
    private TextMeshProUGUI resourcesText;
    [SerializeField]
    private GameObject resourcesTextContainer;

    void Update() {
        DisplayTileInfoText();
    }

    void DisplayTileInfoText () {
        TileData tile = TileController.Instance.GetTileData(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (tile != null) {
            tileInfoContainer.SetActive(tile.selectable);
            tileTypeText.text = tile.tileType.ToString();
            if (tile.selectable) {buildableText.text = "Yes";} else {buildableText.text = "No";}
            if (tile.passable) {passableText.text = "Yes";} else {passableText.text = "No";}
            resourcesTextContainer.SetActive(tile.hasResources);
            if (tile.hasResources) {resourcesText.text = tile.resourcesRemaining.ToString();}
        }
    }
    
}
