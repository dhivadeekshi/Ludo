using System.Collections;
using System.Collections.Generic;
using PlayerType = LudoType;

public class TileManager
{
    public void SetBottomLeftPlayer(PlayerType bottomLeftPlayer) => this.bottomLeftPlayer = bottomLeftPlayer;
    public int GetStartingTileNo(PlayerType playerType) => Constants.Tiles.StartingTilesNo[GetPlayerIndex(playerType)];
    public int GetStartingInnerTileNo(PlayerType playerType) => CalculateTileNo(GetStartingTileNo(playerType), Constants.Tiles.InnerTileStarting);
    public bool IsInnerTile(int tilesTraveled) => Constants.Tiles.InnerTileStarting <= tilesTraveled && tilesTraveled <= Constants.Tiles.InnerTileEnding;
    public int CalculateTileNo(int startingTile, int tileTraveled) => (startingTile - 1 + tileTraveled) % Constants.Tiles.TotalTilesInOuterPath;

    public int GetPlayerIndex(PlayerType playerType)
    {
        // Consider BottomLeftPlayer:2
        // PlayerType:2 => 2 - 2               = 0
        // PlayerType:3 => 3 - 2               = 1
        // PlayerType:0 => 0 - 2 = -2 => 4 - 2 = 2
        // PlayerType:1 => 1 - 2 = -1 => 4 - 1 = 3
        int player = (int)playerType - (int)bottomLeftPlayer;
        if (player < 0) player += (int)PlayerType.END;
        return player;
    }

    public int GetRowNo(int tileIndex)
    {
        // rows 0   1   2   3   4   5   6   7   8   9   10  11  12  13  14
        // tile 0   1   2   3   4   5   6   12  13  19  20  21  22  23  24
        // tile 50  49  48  47  46  45  44  38  37  31  30  29  28  27  26
        int[] rows = { 0, 1, 2, 3, 4, 5, 6, 12, 13, 19, 20, 21, 22, 23, 24 };
        if (tileIndex > 25) tileIndex = 50 - tileIndex;
        int lo = 0;
        int hi = rows.Length - 1;
        while (lo <= hi)
        {
            int mid = lo + ((hi - lo) / 2);
            if (tileIndex > rows[hi]) return hi;
            else if (rows[mid] < tileIndex) lo = mid + 1;
            else if (rows[mid] > tileIndex) hi = mid - 1;
            else return mid;
        }
        return 0;
    }

    public int GetColNo(int tileIndex)
    {
        // col -6 => 13 - 11
        // col -5 => 14 , 10
        // col -4 => 15 , 9
        // col -3 => 16 , 8
        // col -2 => 17 , 7
        // col -1 => 18 , 6
        // col  0 => 19-24,0-5
        // col  1 => 25 , 51
        // col  2 => 26-31,45-50
        // col  3 => 32 , 44
        // col  4 => 33 , 43
        // col  5 => 34 , 42
        // col  6 => 35 , 41
        // col  7 => 36 , 40
        // col  8 => 37 - 39

        // index       -6, -5, -4, -3, -2, -1,  0, 1,  2,  3,  4,  5,  6,  7,  8
        int[] cols = { 11, 14, 15, 16, 17, 18, 19, 25, 26, 32, 33, 34, 35, 36, 37 };
        //                 10,  9,  8,  7,  6,  5, 51, 50, 44, 43, 42, 41, 40, 39
        int lo = 0;
        int hi = cols.Length - 1;
        int startIndex = -6;
        if (tileIndex < cols[lo])
        {
            tileIndex = 24 - tileIndex;
        }
        else if (tileIndex > cols[hi])
        {
            tileIndex = 51 - (tileIndex - 25);
        }

        while (lo <= hi)
        {
            int mid = lo + ((hi - lo) / 2);
            if (tileIndex > cols[hi]) return startIndex + hi;
            else if (cols[mid] < tileIndex) lo = mid + 1;
            else if (cols[mid] > tileIndex) hi = mid - 1;
            else return startIndex + mid;
        }
        return startIndex;
    }

    public int GetNextTileNo(PlayerType playerType, int tilesTraveled) => GetTileNo(playerType, tilesTraveled, 1);

    public int GetTileNo(PlayerType playerType, int tilesTraveled, int tiles = 0)
    {
        int startingTileNo = GetStartingTileNo(playerType);
        int tilesReached = CalculateTileNo(startingTileNo, tilesTraveled + tiles);
        return tilesReached;
    }

    private PlayerType bottomLeftPlayer = PlayerType.Red;

}
