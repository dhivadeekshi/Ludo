using System.Collections.Generic;
using PawnID = Pawn.PawnID;

public class BoardPlayer
{
    #region USER INTERFACE
    public BoardPlayer(LudoType playerType, TileManager tileManager)
    {
        PlayerType = playerType;
        this.tileManager = tileManager;
        CreatePawns();
    }

    public PawnID GetAPawnOutOfStart()
    {
        foreach(var pawn in pawns)
        {
            if (pawn.GetOutOfStart())
                return pawn.pawnID;
        }
        return PawnID.nullID;
    }

    public void ReturnPawnToStart(PawnID pawnID)
    {
        Pawn pawn = GetPawnForID(pawnID);
        if(pawn != null) pawn.ReturnToStart();
    }

    public void MovePawn(PawnID pawnID, int tiles)
    {
        Pawn pawn = GetPawnForID(pawnID);
        if (pawn != null) pawn.MoveTo(tiles);
    }

    public bool HasPossibleMove(int diceRoll)
    {   
        return CanAnyPawnMove(diceRoll);
    }

    public bool IsPlayerWon()
    {
        // Player is won if all pawns reached home
        return IsAllPawnsInHome();

    }

    public List<PawnID> GetAllMovablePawns(int diceRoll)
    {
        List<PawnID> pawnIDs = new List<PawnID>();
        foreach(var pawn in pawns)
        {
            if (pawn.CanMove(diceRoll))
                pawnIDs.Add(pawn.pawnID);
        }
        return pawnIDs;
    }

    public LudoType PlayerType { get; private set; }
    #endregion

    #region INTERNALS
    private Pawn[] pawns = null;
    private TileManager tileManager = null;

    private void CreatePawns()
    {
        pawns = new Pawn[Constants.NoOfPawnsPerPlayer];
        for (int i = 0; i < pawns.Length; i++)
            pawns[i] = new Pawn(PlayerType, new PawnID(i));
    }

    private bool IsAnyPawnInStart()
    {
        foreach(var pawn in pawns)
        {
            if (pawn.IsInStart) return true;
        }
        return false;
    }

    private bool CanAnyPawnMove(int diceRoll)
    {
        foreach(var pawn in pawns)
        {
            if (pawn.CanMove(diceRoll)) return true;
        }
        return false;
    }

    private bool IsAllPawnsInHome()
    {
        foreach(var pawn in pawns)
        {
            if (!pawn.IsHome) return false;
        }
        return true;
    }

    public Pawn GetPawnForID(PawnID pawnID)
    {
        foreach (var pawn in pawns)
        {
            if (pawn.pawnID.equals(pawnID))
                return pawn;
        }
        return null;
    }
    #endregion
}
