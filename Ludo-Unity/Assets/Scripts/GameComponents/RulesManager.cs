using System.Collections;
using System.Collections.Generic;

public class RulesManager
{
    public enum RulesState
    {
        Highlight,
        Move,
        EndTurn
    }


    public RulesState OnDiceRolled(Player player, int diceRoll)
    {
        if (IsPlayerChanged(player)) ChangePlayer(player);
        AddDiceRolled(diceRoll);
        return CheckRules();
    }

    private Player CurrentPlayer { get; set; }
    private List<int> diceRolls = new List<int>();
    private void AddDiceRolled(int diceRoll)
    {
        diceRolls.Add(diceRoll);
    }

    private bool IsPlayerChanged(Player player) { return player != CurrentPlayer; }
    private void ChangePlayer(Player player) { CurrentPlayer = player; ResetDiceRolls(); }
    private void ResetDiceRolls() { diceRolls.Clear(); }


    private RulesState CheckRules()
    {
        // TODO update rules
        if (CurrentPlayer.HasPossibleMove(diceRolls[diceRolls.Count - 1]))
        {
            if (CurrentPlayer.HasMoreThanOnePossibleMove())
                return RulesState.Highlight;
            else
                return RulesState.Move;
        }
        else
            return RulesState.EndTurn;
    }
}
