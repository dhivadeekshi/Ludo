using System;
using System.Collections;
using System.Collections.Generic;

public class LocalPlayer : CommonPlayer, Player
{
    public LocalPlayer(LudoType playerType, string playerName, BoardPlayer boardPlayer, BoardPlayerUI boardPlayerUI)
    {
        playerBoardType = playerType;
        this.playerName = playerName;
        playerOriginalType = playerType;
        this.boardPlayer = boardPlayer;
        this.boardPlayerUI = boardPlayerUI;
        InitPawnData();
    }

    public void SetPlayersTurn()
    {
        boardPlayerUI.DisplayDice();
        EnableDice();
    }

    public void EndPlayersTurn()
    {
        boardPlayerUI.HideDice();
        boardPlayerUI.ResetDice();
    }

    public void HighlightAllPawnsInStart(Action<Pawn.PawnID> onPawnSelected) =>
        boardPlayerUI.HighlightPawns(GetAllPawnUIsInStart(), (pawnUIID) => { OnPawnTapped(pawnUIID, onPawnSelected); });

    public void HighlightPawnsInOpenTraveledMax(int tiles, Action<Pawn.PawnID> onPawnSelected) =>
        boardPlayerUI.HighlightPawns(GetAllPawnUIsTraveledMax(tiles), (pawnUIID) => { OnPawnTapped(pawnUIID, onPawnSelected); });

    public void GainedExtraDiceThrow() => EnableDice();

    public new void GetPawnOutOfStart(Pawn.PawnID pawnID, Action<Pawn.PawnID> onCompleted)
    {
        var pawnData = GetPawnData(pawnID);
        boardPlayerUI.GetPawnOutOfStart(pawnData.pawnUI);
        boardPlayer.GetPawnOutOfStart(pawnID);
        pawnsInStart.Remove(pawnData);
        pawnsInOpen.Add(pawnData);
        onCompleted.Invoke(pawnID);
        GroupPawnsInTile(TileManager.Instance.GetStartingTileNo(playerBoardType), Constants.Tiles.FirstTileNoOut);
    }
    public new void GetLastPawnOutOfStart(Action<Pawn.PawnID> onCompleted) => GetPawnOutOfStart(pawnsInStart[0].pawn, onCompleted);
    public new void MakeOnlyPossibleMove(int tiles, Action<Pawn.PawnID> onMoveCompleted) => MovePawn(pawnsInOpen[0].pawn, tiles, onMoveCompleted);

    public void MovePawn(Pawn.PawnID pawnID, int tiles, Action<Pawn.PawnID> onMoveCompleted)
    {
        var pawnData = GetPawnData(pawnID);
        int tilesTraveled = boardPlayer.GetTilesTraveled(pawnID);
        int tileNo = TileManager.Instance.GetTileNo(playerBoardType, tilesTraveled + tiles);
        UngroupPawnsInTile(tilesTraveled);
        var pawnsInTile = boardPlayer.GetAllPawnsTraveled(tilesTraveled);
        if (pawnsInTile.Count > 2)
            boardPlayerUI.ReturnPawnToNormal(GetPawnData(pawnID).pawnUI);
        Debugger.Log("Move Pawn To : " + tileNo);
        boardPlayerUI.MovePawnToTile(pawnData.pawnUI, tileNo, () =>
        {
            boardPlayer.MovePawn(pawnID, tiles);
            GroupPawnsInTile(tileNo, tilesTraveled + tiles);
            onMoveCompleted.Invoke(pawnID);
        });
    }

    public void GroupPawnsInTile(int tileNo, int tilesTraveled)
    {
        var pawnsInTile = boardPlayer.GetAllPawnsTraveled(tilesTraveled);
        if(pawnsInTile.Count > 1)
            boardPlayerUI.GroupPawns(GetPawnUIsFrom(pawnsInTile), tileNo);
    }

    public void UngroupPawnsInTile(int tilesTraveled)
    {
        int tileNo = TileManager.Instance.GetTileNo(playerBoardType, tilesTraveled);
        var pawnsInTile = boardPlayer.GetAllPawnsTraveled(tilesTraveled);
        if (pawnsInTile.Count == 2)
            boardPlayerUI.UngroupPawns(GetPawnUIsFrom(pawnsInTile), tileNo);
    }

    private List<PawnUI.PawnUIID> GetPawnUIsFrom(List<Pawn.PawnID> pawnIDs)
    {
        var pawnUIIDs = new List<PawnUI.PawnUIID>();
        foreach (var pawnID in pawnIDs)
            pawnUIIDs.Add(GetPawnData(pawnID).pawnUI);
        return pawnUIIDs;
    }
    
    protected override PlayerType playerType { get { return PlayerType.LocalPlayer; } }
    private void OnDiceTapped() { DisableDice(); ShowDiceRoll(); }
    private void EnableDice() => boardPlayerUI.EnableDice(OnDiceTapped);
    private void DisableDice() => boardPlayerUI.DisableDice();
    private void StopAllHighlights() => boardPlayerUI.StopPawnsHighlighting();
    protected override void OnAnimationEnded() => DiceRolled(Dice.RollDice());

    private class PawnData
    {
        public Pawn.PawnID pawn { get; private set; }
        public PawnUI.PawnUIID pawnUI { get; private set; }

        public PawnData(Pawn.PawnID pawn, PawnUI.PawnUIID pawnUI)
        {
            this.pawn = pawn;
            this.pawnUI = pawnUI;
        }
    }

    private List<PawnData> pawnsInStart = new List<PawnData>();
    private List<PawnData> pawnsInOpen = new List<PawnData>();

    private void InitPawnData()
    {
        var pawns = boardPlayer.GetAllPawns();
        var pawnsUI = boardPlayerUI.GetAllPawns();
        for (int i = 0; i < pawns.Count; i++)
            pawnsInStart.Add(new PawnData(pawns[i], pawnsUI[i]));
    }

    private List<PawnUI.PawnUIID> GetAllPawnUIsInStart()
    {
        var pawnUIs = new List<PawnUI.PawnUIID>();
        foreach (var pawn in pawnsInStart)
            pawnUIs.Add(pawn.pawnUI);
        return pawnUIs;
    }

    private List<PawnUI.PawnUIID> GetAllPawnUIsTraveledMax(int tiles)
    {
        var pawnUIs = new List<PawnUI.PawnUIID>();
        var pawns = GetAllPawnsInOpenTraveledMax(tiles);
        var tilesTraveledVisited = new List<int>();
        foreach (var pawn in pawns)
        {
            int tileTraveled = boardPlayer.GetTilesTraveled(pawn);
            bool isIgnore = false;
            foreach (var tile in tilesTraveledVisited)
            {
                if (tileTraveled == tile) { isIgnore = true; break; }
            }
            if (isIgnore) continue;
            tilesTraveledVisited.Add(tileTraveled);
            pawnUIs.Add(GetPawnData(pawn).pawnUI);
        }
        return pawnUIs;
    }

    private void OnPawnTapped(PawnUI.PawnUIID pawnUI, Action<Pawn.PawnID> onPawnSelected)
    {
        StopAllHighlights();
        if (onPawnSelected != null)
        {
            var pawnData = GetPawnData(pawnUI);
            onPawnSelected.Invoke(pawnData.pawn);
        }
    }

    private PawnData GetPawnData(PawnUI.PawnUIID pawnUIID)
    {
        foreach (var pawnData in pawnsInStart)
        {
            if (pawnData.pawnUI.equals(pawnUIID))
                return pawnData;
        }
        foreach (var pawnData in pawnsInOpen)
        {
            if (pawnData.pawnUI.equals(pawnUIID))
                return pawnData;
        }
        return null;
    }

    private PawnData GetPawnData(Pawn.PawnID pawnID)
    {
        var pawnData = GetPawnDataInStart(pawnID);
        if (pawnData != null) return pawnData;
        pawnData = GetPawnDataInOpen(pawnID);
        return pawnData;
    }

    private PawnData GetPawnDataInStart(Pawn.PawnID pawnID)
    {
        foreach (var pawnData in pawnsInStart)
        {
            if (pawnData.pawn.equals(pawnID))
                return pawnData;
        }
        return null;
    }

    private PawnData GetPawnDataInOpen(Pawn.PawnID pawnID)
    {
        foreach (var pawnData in pawnsInOpen)
        {
            if (pawnData.pawn.equals(pawnID))
                return pawnData;
        }
        return null;
    }

}
