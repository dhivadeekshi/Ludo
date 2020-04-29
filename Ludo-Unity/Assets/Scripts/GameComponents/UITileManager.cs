using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerType = LudoType;

public class UITileManager : MonoBehaviour
{
    public void SetTileManager(TileManager tileManager) => this.tileManager = tileManager;

    public Vector2 GetStartingTilePositionFor(PlayerType playerType)
    {
        switch (tileManager.GetPlayerIndex(playerType))
        {
            case 0: return GetBottomLeftPlayerStartingTile();
            case 1: return GetTopLeftPlayerStartingTile();
            case 2: return GetTopRightPlayerStartingTile();
            case 3: return GetBottomRightPlayerStartingTile();
        }
        return Vector2.zero;
    }

    public Vector2 GetNextTilePosition(PlayerType playerType, int tilesTraveled)
    {
        if (tileManager.IsInnerTile(tilesTraveled + 1))
            return GetNextInnerTilePosition(playerType, tilesTraveled);
        return GetTilePosition(tileManager.GetNextTileNo(playerType, tilesTraveled));
    }


    [SerializeField]
    private GameObject startingTileBottomLeft = null;
    [SerializeField]
    private GameObject startingTileTopLeft = null;
    [SerializeField]
    private GameObject startingTileTopRight = null;
    [SerializeField]
    private GameObject startingTileBottomRight = null;

    [SerializeField]
    private GameObject tile_0_0 = null;
    [SerializeField]
    private GameObject tile_1_1 = null;

    private TileManager tileManager = null;
    private Vector2 tileSize = Vector2.one;
    private float tileHeight { get { return tileSize.y; } }
    private float tileWidth { get { return tileSize.x; } }
    private Vector2 GetBottomLeftPlayerStartingTile() => startingTileBottomLeft.transform.position;
    private Vector2 GetTopLeftPlayerStartingTile() => startingTileTopLeft.transform.position;
    private Vector2 GetTopRightPlayerStartingTile() => startingTileTopRight.transform.position;
    private Vector2 GetBottomRightPlayerStartingTile() => startingTileBottomRight.transform.position;

    // Start is called before the first frame update
    void Start()
    {
        CalculateTileSize();
    }

    private void CalculateTileSize()
    {
        float tileWidth = Mathf.Abs(tile_1_1.transform.position.x - tile_0_0.transform.position.x);
        float tileHeight = Mathf.Abs(tile_1_1.transform.position.y - tile_0_0.transform.position.y);
        tileSize = new Vector2(tileWidth, tileHeight);
    }

    private Vector2 GetPositionToTop(Vector2 startingPosition, int tiles) => startingPosition + (Vector2.up * tileHeight * tiles);
    private Vector2 GetPositionToBottom(Vector2 startingPosition, int tiles) => startingPosition + (Vector2.down * tileHeight * tiles);
    private Vector2 GetPositionToLeft(Vector2 startingPosition, int tiles) => startingPosition + (Vector2.left * tileWidth * tiles);
    private Vector2 GetPositionToRight(Vector2 startingPosition, int tiles) => startingPosition + (Vector2.right * tileWidth * tiles);

    private Vector2 GetNextInnerTilePosition(PlayerType playerType, int tilesTraveled, int tiles = 1)
    {
        int noOfInnerTilesReached = tilesTraveled + tiles - Constants.Tiles.InnerTileStarting;
        Vector2 startingInnerTilePosition = GetTilePosition(tileManager.GetStartingInnerTileNo(playerType));
        switch (tileManager.GetPlayerIndex(playerType))
        {
            case 0: return GetPositionToTop(startingInnerTilePosition, noOfInnerTilesReached); // Go up from first inner tile
            case 1: return GetPositionToRight(startingInnerTilePosition, noOfInnerTilesReached); // Go right from first inner tile
            case 2: return GetPositionToBottom(startingInnerTilePosition, noOfInnerTilesReached); // Go bottom from first inner tile
            case 3: return GetPositionToLeft(startingInnerTilePosition, noOfInnerTilesReached); // Go left from first inner tile
        }
        return Vector2.zero;
    }

    private Vector2 GetTilePosition(int tileIndex)
    {
        int row = tileManager.GetRowNo(tileIndex);
        int col = tileManager.GetColNo(tileIndex);
        return new Vector2(col * tileWidth, row * tileHeight);
    }

}
