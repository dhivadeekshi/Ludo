﻿using System;
using System.Collections;
using System.Collections.Generic;

public class OnlinePlayer : CommonPlayer, Player
{
    public OnlinePlayer(LudoType playerType)
    {
        playerBoardType = playerType;
        // Player Original type is retrived from server connection
    }

    protected override PlayerType playerType { get { return PlayerType.OnlinePlayer; } }

    public void EndPlayersTurn()
    {
        throw new System.NotImplementedException();
    }

    public void SetPlayersTurn()
    {
        throw new System.NotImplementedException();
    }

    public void HighlightAllPawnsInStart() { }
    public void HighlightPawnsInOpenTraveledMax(int tiles) { }

    public void GainedExtraDiceThrow()
    {
        throw new NotImplementedException();
    }

    public void HighlightAllPawnsInStart(Action<Pawn.PawnID> onPawnSelected)
    {
        throw new NotImplementedException();
    }

    public void HighlightPawnsInOpenTraveledMax(int tiles, Action<Pawn.PawnID> onPawnSelected)
    {
        throw new NotImplementedException();
    }

    public void MovePawn(Pawn.PawnID pawnID, int tiles, Action<Pawn.PawnID> onMoveCompleted)
    {
        throw new NotImplementedException();
    }

    public void ReturnPawnToStart(Pawn.PawnID pawnID, Action onCompleted)
    {
        throw new NotImplementedException();
    }
}
