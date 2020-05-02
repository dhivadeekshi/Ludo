
public class Pawn
{
    #region USER INTERFACE
    public struct PawnID
    {
        public int id { get; private set; }
        public PawnID(int id) => this.id = id;
        public static PawnID nullID { get { return new PawnID(-1); } }
        public bool equals(PawnID otherID) => this.id == otherID.id;
        public bool IsNull() => equals(nullID);
    }

    public LudoType PawnType { get; private set; }
    public int TilesTravled { get; private set; }
    public PawnID pawnID { get; private set; }

    public bool IsInStart { get { return TilesTravled == Constants.Tiles.StartTileNo; } }
    public bool IsHome { get { return TilesTravled == Constants.Tiles.TotalStepsToReachHome; } }
    public bool IsSafe { get { return IsInStart || IsHome || IsInInnerTile() || IsInSafeTile(); } }
    public bool IsPawnOut { get { return !IsHome && !IsInStart; } }

    public Pawn(LudoType pawnType, PawnID pawnID)
    {
        PawnType = pawnType;
        this.pawnID = pawnID;
        TilesTravled = Constants.Tiles.StartTileNo;
    }

    public void ReturnToStart()
    {
        TilesTravled = Constants.Tiles.StartTileNo;
    }

    public bool GetOutOfStart()
    {
        if (!IsInStart) return false;
        TilesTravled = Constants.Tiles.FirstTileNoOut;
        return true;
    }

    public void MoveTo(int tiles)
    {
        TilesTravled += tiles;
    }
    #endregion

    #region INTERNALS
    private bool IsInSafeTile()
    {
        // Assert SafeTiles is in Ascending order
        int lo = 0;
        int hi = Constants.Tiles.SafeTiles.Length - 1;
        while(lo <= hi)
        {
            int mid = lo + ((hi - lo) / 2);
            if (TilesTravled < Constants.Tiles.SafeTiles[mid])
                hi = mid - 1;
            else if (TilesTravled > Constants.Tiles.SafeTiles[mid])
                lo = mid + 1;
            else
                return true;
        }
        return false;
    }

    private bool IsInInnerTile()
    {
        return Constants.Tiles.InnerTileStarting <= TilesTravled && TilesTravled <= Constants.Tiles.InnerTileEnding;
    }
    #endregion
}
