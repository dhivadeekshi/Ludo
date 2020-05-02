using System;
using System.Collections;
using System.Collections.Generic;

public class CommonPlayer
{

    #region USER INTERFACE
    public enum PlayerType
    {
        LocalPlayer,
        OnlinePlayer
    }

    public void SetListeners(Action<int> onDiceRolled)
    {
        this.onDiceRolled += onDiceRolled;
    }

    public void ClearListeners()
    {
        this.onDiceRolled = null;
    }

    public bool IsLocalPlayer() { return playerType == PlayerType.LocalPlayer; }
    public bool IsOnlinePlayer() { return playerType == PlayerType.OnlinePlayer; }
    public LudoType GetPlayerBoardType() { return playerBoardType; }

    public void GetPawnOutOfStart(Pawn.PawnID pawnID, Action<Pawn.PawnID> onCompleted) { }
    public void GetLastPawnOutOfStart(Action<Pawn.PawnID> onCompleted) { }
    public void MakeOnlyPossibleMove(int tiles, Action<Pawn.PawnID> onMoveCompleted) { }

    // Board Player Start
    public bool IsPawnInSafeTile(Pawn.PawnID pawnID) => boardPlayer.IsPawnSafeTile(pawnID);
    public Pawn.PawnID GetAPawnOutOfStart() { return boardPlayer.GetAPawnOutOfStart(); }
    public int GetTilesTraveled(Pawn.PawnID pawnID) { return boardPlayer.GetTilesTraveled(pawnID); } 
    public void ReturnPawnToStart(Pawn.PawnID pawnID) { boardPlayer.ReturnPawnToStart(pawnID); }
    public int CountPawnsInStart() {return boardPlayer.CountPawnsInStart(); }
    public int CountPawnsInHome() { return boardPlayer.CountPawnsInHome(); }
    public int CountPawnsInOpen() { return boardPlayer.CountPawnsInOpen(); }
    public int CountPawnsInOpenTraveledMax(int tiles) { return boardPlayer.CountPawnsInOpenTraveledMax(tiles); }
    public List<Pawn.PawnID> GetAllPawnsInStart() { return boardPlayer.GetAllPawnsInStart(); }
    public List<Pawn.PawnID> GetAllPawnsInOpen() { return boardPlayer.GetAllPawnsInOpen(); }
    public List<Pawn.PawnID> GetAllPawnsInOpenTraveledMax(int tiles) { return boardPlayer.GetAllPawnsInOpenTraveledMax(tiles); }
    public List<Pawn.PawnID> GetAllPawns() { return boardPlayer.GetAllPawns(); }
    public List<Pawn.PawnID> GetAllPawnsTraveled(int tilesTraveled) => boardPlayer.GetAllPawnsTraveled(tilesTraveled);
    // Board Player End

    #endregion


    #region INTERNAL
    protected virtual PlayerType playerType { get; }
    protected LudoType playerBoardType = LudoType.Red;
    protected LudoType playerOriginalType = LudoType.Red;
    protected string playerName = string.Empty;

    protected Action<int> onDiceRolled = null;

    protected BoardPlayer boardPlayer = null;
    protected BoardPlayerUI boardPlayerUI = null;

    protected void DiceRolled(int diceRoll)
    {
        Debugger.Log("[CommonPlayer] DiceRolled " + diceRoll);
        boardPlayerUI.DisplayDiceFace(diceRoll);
        if (onDiceRolled != null)
            onDiceRolled.Invoke(diceRoll);
    }

    protected void ShowDiceRoll()
    {
        boardPlayerUI.RollDice(OnAnimationEnded);
    }

    protected virtual void OnAnimationEnded() { }

    public override string ToString()
    {
        return playerName;
    }
    #endregion

}
