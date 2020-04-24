using System.Collections;
using System.Collections.Generic;
using System;

public interface Player
{
    void SetPlayersTurn();
    void EndPlayersTurn();
    void SetListeners(Action<int> onDiceRolled);
    void ClearListeners();


    Pawn.PawnID GetAPawnOutOfStart();
    int GetTilesTraveled(Pawn.PawnID pawnID);
    void ReturnPawnToStart(Pawn.PawnID pawnID);
    void MovePawn(Pawn.PawnID pawnID, int tiles);
    int CountPawnsInStart();
    int CountPawnsInHome();
    int CountPawnsInOpen();
    List<Pawn.PawnID> GetAllPawnsInStart();
    List<Pawn.PawnID> GetAllPawnsInOpen();
    List<Pawn.PawnID> GetAllPawns();
}
