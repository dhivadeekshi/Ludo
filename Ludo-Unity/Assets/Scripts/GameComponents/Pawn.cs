
public class Pawn
{
    #region USER INTERFACE
    public struct PawnID
    {
        public int id { get; private set; }
        public PawnID(int id) { this.id = id; }

        public static PawnID nullID { get { return new PawnID(-1); } }

        public bool equals(PawnID otherID) { return this.id == otherID.id; }
    }

    public LudoType PawnType { get; private set; }
    public int TilesTravled { get; private set; }
    public PawnID pawnID { get; private set; }

    public bool IsInStart { get { return TilesTravled == Constants.DiceRoll.StartTileNo; } }
    public bool IsHome { get { return TilesTravled == Constants.DiceRoll.TotalStepsToReachHome; } }
    public bool IsSafe { get { return IsInStart || IsHome || IsInInnerTile() || IsInSafeTile(); } }
    public bool IsPawnOut { get { return !IsHome && !IsInStart; } }

    public Pawn(LudoType pawnType, PawnID pawnID)
    {
        PawnType = pawnType;
        this.pawnID = pawnID;
        TilesTravled = Constants.DiceRoll.StartTileNo;
    }

    public void ReturnToStart()
    {
        TilesTravled = Constants.DiceRoll.StartTileNo;
    }

    public bool GetOutOfStart()
    {
        if (!IsInStart) return false;
        TilesTravled = Constants.DiceRoll.FirstTileNoOut;
        return true;
    }

    public void MoveTo(int tiles)
    {
        TilesTravled += tiles;
    }

    public bool CanMove(int tiles)
    {
        if (IsHome) return false;   // Cant move if we already reached home
        if (IsInStart)
            return tiles == Constants.DiceRoll.RollToGetOutFromStart;   // If we are not out we can only get out on rolling the exact dice face
        return TilesTravled + tiles <= Constants.DiceRoll.TotalStepsToReachHome;    // Can move only if we have tiles to move
    }
    #endregion

    #region INTERNALS
    private bool IsInSafeTile()
    {
        // Assert SafeTiles is in Ascending order
        int lo = 0;
        int hi = Constants.DiceRoll.SafeTiles.Length - 1;
        while(lo <= hi)
        {
            int mid = lo + ((hi - lo) / 2);
            if (TilesTravled < Constants.DiceRoll.SafeTiles[mid])
                hi = mid - 1;
            else if (TilesTravled > Constants.DiceRoll.SafeTiles[mid])
                lo = mid + 1;
            else
                return true;
        }
        return false;
    }

    private bool IsInInnerTile()
    {
        return Constants.DiceRoll.InnerTileStarting <= TilesTravled && TilesTravled <= Constants.DiceRoll.InnerTileEnding;
    }
    #endregion
}
