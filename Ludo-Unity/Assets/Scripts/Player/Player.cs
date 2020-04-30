﻿using System.Collections;
using System.Collections.Generic;
using System;

public interface Player
{
    void SetPlayersTurn();
    void EndPlayersTurn();
    void SetListeners(Action<int> onDiceRolled);
    void ClearListeners();


    void GainedExtraDiceThrow();

    void HighlightAllPawnsInStart(Action<Pawn.PawnID> onPawnSelected);
    void HighlightPawnsInOpenTraveledMax(int tiles, Action<Pawn.PawnID> onPawnSelected);
    void MakeOnlyPossibleMove(int diceRoll, Action<Pawn.PawnID> onMoveCompleted);
    void GetLastPawnOutOfStart(Action<Pawn.PawnID> onCompleted);

    void GetPawnOutOfStart(Pawn.PawnID pawnID, Action<Pawn.PawnID> onCompleted);
    Pawn.PawnID GetAPawnOutOfStart();
    int GetTilesTraveled(Pawn.PawnID pawnID);
    void ReturnPawnToStart(Pawn.PawnID pawnID);
    void MovePawn(Pawn.PawnID pawnID, int tiles, Action<Pawn.PawnID> onMoveCompleted);
    int CountPawnsInStart();
    int CountPawnsInHome();
    int CountPawnsInOpen();
    int CountPawnsInOpenTraveledMax(int lessThan);
    List<Pawn.PawnID> GetAllPawnsInStart();
    List<Pawn.PawnID> GetAllPawnsInOpenTraveledMax(int lessThan);
    List<Pawn.PawnID> GetAllPawnsInOpen();
    List<Pawn.PawnID> GetAllPawns();
}
