using System.Collections.Generic;
using PawnID = Pawn.PawnID;

public class BoardPlayer
{
    #region USER INTERFACE
    public BoardPlayer(LudoType playerType)
    {
        PlayerType = playerType;
        CreatePawns();
    }

    public PawnID GetAPawnOutOfStart()
    {
        foreach (var pawn in pawns)
        {
            if (pawn.GetOutOfStart())
                return pawn.pawnID;
        }
        return PawnID.nullID;
    }

    public void GetPawnOutOfStart(PawnID pawnID) => GetPawnForID(pawnID).GetOutOfStart();

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

    public int GetTilesTraveled(PawnID pawnID)
    {
        Pawn pawn = GetPawnForID(pawnID);
        if (pawn == null) return -1;
        return pawn.TilesTravled;
    }

    public int CountPawnsInStart()
    {
        int count = 0;
        foreach (var pawn in pawns)
        {
            if (pawn.IsInStart) count++;
        }
        return count;
    }

    public int CountPawnsInHome()
    {
        int count = 0;
        foreach(var pawn in pawns)
        {
            if (pawn.IsHome) count++;
        }
        return count;
    }

    public int CountPawnsInOpen()
    {
        int count = 0;
        foreach(var pawn in pawns)
        {
            if (pawn.IsPawnOut) count++;
        }
        return count;
    }

    public int CountPawnsInOpenTraveledMax(int tiles)
    {
        int count = 0;
        foreach (var pawn in pawns)
        {
            if (pawn.IsPawnOut && pawn.TilesTravled <= tiles) count++;
        }
        return count;
    }

    public List<PawnID> GetAllPawnsInStart()
    {
        List<PawnID> pawnIDs = new List<PawnID>();
        foreach (var pawn in pawns)
        {
            if (pawn.IsInStart)
                pawnIDs.Add(pawn.pawnID);
        }
        return pawnIDs;
    }

    public List<PawnID> GetAllPawnsInOpen()
    {
        List<PawnID> pawnIDs = new List<PawnID>();
        foreach (var pawn in pawns)
        {
            if (pawn.IsPawnOut)
                pawnIDs.Add(pawn.pawnID);
        }
        return pawnIDs;
    }

    public List<PawnID> GetAllPawnsInOpenTraveledMax(int tiles)
    {
        List<PawnID> pawnIDs = new List<PawnID>();
        foreach (var pawn in pawns)
        {
            if (pawn.IsPawnOut && pawn.TilesTravled <= tiles)
                pawnIDs.Add(pawn.pawnID);
        }
        return pawnIDs;
    }

    public List<PawnID> GetAllPawnsTraveled(int tiles)
    {
        List<PawnID> pawnIDs = new List<PawnID>();
        foreach (var pawn in pawns)
        {
            if (pawn.IsPawnOut && pawn.TilesTravled == tiles)
                pawnIDs.Add(pawn.pawnID);
        }
        return pawnIDs;
    }

    public List<PawnID> GetAllPawns()
    {
        List<PawnID> pawnIDs = new List<PawnID>();
        foreach(var pawn in pawns)
        {
            pawnIDs.Add(pawn.pawnID);
        }
        return pawnIDs;
    }

    public LudoType PlayerType { get; private set; }
    #endregion

    #region INTERNALS
    private Pawn[] pawns = null;

    private void CreatePawns()
    {
        pawns = new Pawn[Constants.NoOfPawnsPerPlayer];
        for (int i = 0; i < pawns.Length; i++)
            pawns[i] = new Pawn(PlayerType, new PawnID(i));
    }

    private Pawn GetPawnForID(PawnID pawnID)
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
