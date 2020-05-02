using System.Collections;
using System.Collections.Generic;

public class RulesManager
{
    public enum DiceRollStates
    {
        Highlight,
        Move,
        EndTurn
    }

    public enum PawnMoveStates
    {
        RollDice,
        KillPawn,
        EndTurn,
        GameOver
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
    }

    public void ChangeTurn(Player player)
    {
        ChangePlayer(player);
        ResetDiceRolls();
        killPawnExtraTurns = 0;
        reachedHomeExtraTurns = 0;
    }

    public DiceRollStates CheckRulesOnDiceRoll(int diceRoll)
    {
        AddDiceRolled(diceRoll);
        return CheckDiceRollRules();
    }

    public PawnMoveStates CheckRulesOnMoveEnd(Pawn.PawnID pawnID) => InternalCheckRulesOnMoveEnd(pawnID);

    public KeyValuePair<Player, Pawn.PawnID> GetPawnKilledBy(Pawn.PawnID pawnID)
    {
        int currentPawnTilesTraveled = CurrentPlayer.GetTilesTraveled(pawnID);
        var nullValue = new KeyValuePair<Player, Pawn.PawnID>(null, Pawn.PawnID.nullID);
        if (CurrentPlayer.IsPawnInSafeTile(pawnID)) return nullValue; // Pawn landed on a safe tile, cannot kill pawn
        if (CurrentPlayer.GetAllPawnsTraveled(currentPawnTilesTraveled).Count > 1)
            return nullValue; // More than one pawn of the player in the spot forms a group
        var pawnKilled = nullValue;
        int tileNo = TileManager.Instance.GetTileNo(CurrentPlayer.GetPlayerBoardType(), currentPawnTilesTraveled);
        foreach (var player in players)
        {
            if (player != CurrentPlayer)
            {
                int pawnTraveled = TileManager.Instance.ConvertTileNoToTilesTraveled(player.GetPlayerBoardType(), tileNo);
                var pawnsInTile = player.GetAllPawnsTraveled(pawnTraveled);
                if (pawnsInTile.Count == 0) continue; // No pawns found to kill
                if (pawnsInTile.Count > 1) return nullValue; // More than one pawn in the spot cannot kill
                if (pawnsInTile.Count == 1 && !pawnKilled.Value.IsNull()) return nullValue; // Different player pawns in a group cannot kill
                pawnKilled = new KeyValuePair<Player, Pawn.PawnID>(player, pawnsInTile[0]);
            }
        }
        return pawnKilled;
    }

    public bool IsHighlightPawnsInStart(int diceRoll) { return diceRoll == Constants.DiceRoll.RollToGetOutFromStart; }
    public bool IsCurrentPlayerWon() { return IsPlayerWon(CurrentPlayer); }
    public bool IsPlayerWon(Player player) { return player.CountPawnsInHome() == Constants.NoOfPawnsPerPlayer; }
    public int GetTilesTraveledMaxToAllowMove(int diceRoll) { return Constants.Tiles.TotalStepsToReachHome - diceRoll; }

    private Player CurrentPlayer { get; set; }
    private List<Player> players = new List<Player>();
    private List<int> diceRolls = new List<int>();

    private int killPawnExtraTurns = 0;
    private int reachedHomeExtraTurns = 0;

    private void ChangePlayer(Player player) { CurrentPlayer = player; }
    private void ResetDiceRolls() { diceRolls.Clear(); }
    private void AddDiceRolled(int diceRoll) { diceRolls.Add(diceRoll); }
    private int LastRolledDice() { return diceRolls[diceRolls.Count - 1]; }
    

    private DiceRollStates CheckDiceRollRules()
    {
        // Rules:
        //  Rules For Highlight:
        //      If dice roll equals to roll to get out and we have more than one pawn in start
        //      If dice roll equals to roll to get out and we have one pawn in start and atleast one pawn out that can move
        //      For any roll if we have more than one pawn out that can move
        //  Rules for Move:
        //      If dice roll equals to roll to get out and we have only one pawn in start and no other pawn that can move to dice roll
        //      For any roll other than one to get out if we have only one pawn out
        //  Rules for EndTurn:
        //      When no move is possible we end turn even when we roll 6

        int diceRoll = LastRolledDice();
        int pawnsInStart = CurrentPlayer.CountPawnsInStart();
        int pawnsInOpen = CurrentPlayer.CountPawnsInOpenTraveledMax(GetTilesTraveledMaxToAllowMove(diceRoll));

        if (diceRoll == Constants.DiceRoll.RollToGetOutFromStart)
        {
            if (pawnsInStart > 1 || (pawnsInStart == 1 && pawnsInOpen >= 1))
                return DiceRollStates.Highlight;
            else if (pawnsInStart == 1 && pawnsInOpen == 0)
                return DiceRollStates.Move;
        }
        else if (pawnsInOpen > 1)
            return DiceRollStates.Highlight;
        else if (pawnsInOpen == 1)
            return DiceRollStates.Move;
        return DiceRollStates.EndTurn;
    }

    private PawnMoveStates InternalCheckRulesOnMoveEnd(Pawn.PawnID pawnID)
    {
        // Rules:
        //  Rules for RollDice:
        //      On Rolling a 6
        //      On sending any opponents pawn to start
        //      On Reaching a pawn to home
        //  Rules for KillPawn:
        //      When there is a pawn landed on a tile with other player's pawn
        //      If a piece lands upon a piece of the same colour, this forms a block. This block cannot be killed by any opposing piece.
        //  Rules for EndTurn:
        //      When there is no move to make

        if (IsCurrentPlayerWon()) return PawnMoveStates.GameOver;

        ProvideExtraTurnOnReachHome(pawnID);
        if (IsPawnKillsAny(pawnID)) { ProvideExtraTurnOnKillOther(); return PawnMoveStates.KillPawn; }

        if (LastRolledDice() == Constants.DiceRoll.RollForExtraTurn) return PawnMoveStates.RollDice;

        if (killPawnExtraTurns > 0) { killPawnExtraTurns--; return PawnMoveStates.RollDice; }
        if (reachedHomeExtraTurns > 0) { reachedHomeExtraTurns--; return PawnMoveStates.RollDice; }

        return PawnMoveStates.EndTurn;
    }

    private void ProvideExtraTurnOnReachHome(Pawn.PawnID pawnID)
    {
        if (CurrentPlayer.GetTilesTraveled(pawnID) == Constants.Tiles.TotalStepsToReachHome)
            reachedHomeExtraTurns++;
    }

    private void ProvideExtraTurnOnKillOther()
    {
            killPawnExtraTurns++;
    }

    private bool IsPawnKillsAny(Pawn.PawnID pawnID) => GetPawnKilledBy(pawnID).Value.IsNull() == false;

    private bool CanReachHome(int tilesTraveled, int diceRoll)
    {
        return tilesTraveled + diceRoll == Constants.Tiles.TotalStepsToReachHome;
    }
}
