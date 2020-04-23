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

    public bool HasPossibleMove(int diceRoll)
    {
        return boardPlayer.HasPossibleMove(diceRoll);
    }

    public bool HasMoreThanOnePossibleMove()
    {
        return false;
    }


    public bool IsPlayerWon()
    {
        return boardPlayer.IsPlayerWon();
    }

    public bool IsLocalPlayer() { return playerType == PlayerType.LocalPlayer; }
    public bool IsOnlinePlayer() { return playerType == PlayerType.OnlinePlayer; }
    public LudoType GetPlayerBoardType() { return playerBoardType; }
    #endregion


    #region INTERNAL
    protected virtual PlayerType playerType { get; }
    protected LudoType playerBoardType = LudoType.Red;
    protected LudoType playerOriginalType = LudoType.Red;

    protected Action<int> onDiceRolled = null;

    protected BoardPlayer boardPlayer = null;
    protected BoardPlayerUI boardPlayerUI = null;


    protected void DiceRolled(int diceRoll)
    {
        boardPlayerUI.DisplayDiceFace(diceRoll);
        if (onDiceRolled != null)
            onDiceRolled.Invoke(diceRoll);
    }

    protected void ShowDiceRoll()
    {
        boardPlayerUI.RollDice(OnAnimationEnded);
    }

    protected virtual void OnAnimationEnded() { }
    #endregion

}
