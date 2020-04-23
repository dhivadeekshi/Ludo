using System.Collections;
using System.Collections.Generic;

public class LocalPlayer : CommonPlayer,  Player
{
    public LocalPlayer(LudoType playerType, BoardPlayer boardPlayer, BoardPlayerUI boardPlayerUI)
    {
        playerBoardType = playerType;
        playerOriginalType = playerType;
        this.boardPlayer = boardPlayer;
        this.boardPlayerUI = boardPlayerUI;
    }

    public void SetPlayersTurn()
    {
        boardPlayerUI.DisplayDice();
        boardPlayerUI.EnableDice(OnDiceTapped);
    }

    public void EndPlayersTurn()
    {
        boardPlayerUI.HideDice();
        boardPlayerUI.DisableDice();
        boardPlayerUI.ResetDice();
    }

    protected override PlayerType playerType { get { return PlayerType.LocalPlayer; } }

    private void OnDiceTapped()
    {
        ShowDiceRoll();
    }

    protected override void OnAnimationEnded()
    {
        DiceRolled(Dice.RollDice());
    }
}
