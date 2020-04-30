using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager
{
    public PlayerManager(DelayManager delayManager)
    {
        this.delayManager = delayManager;
        rulesManager = new RulesManager();
    }

    public void CreateLocalPlayer(LudoType playerType, string playerName, BoardPlayer boardPlayer, BoardPlayerUI boardPlayerUI)
    {
        var player = new LocalPlayer(playerType, playerName, boardPlayer, boardPlayerUI);
        rulesManager.AddPlayer(player);
        players.Add(player);
    }

    // Temp disabled, waiting for server implementation
    /*public*/
    private void CreateOnlinePlayer(LudoType playerType)
    {
        var player = new OnlinePlayer(playerType);
        //rulesManager.AddPlayer(player);
        players.Add(player);
    }

    public void StartPlay(Action onGameOver)
    {
        this.onGameOver = onGameOver;
        CurrentPlayer.SetPlayersTurn();
        CurrentPlayer.SetListeners(OnDiceRolled);
        rulesManager.ChangeTurn(CurrentPlayer);
    }


    public void Update()
    {
    }

    public string[] GetPlayerNamesByRank()
    {
        string[] playerNames = new string[TotalPlayers];
        // TODO update playerNames by rank
        return playerNames;
    }

    private List<Player> players = new List<Player>();
    private int currentTurnPlayer = 0;
    private DelayManager delayManager = null;
    private RulesManager rulesManager = null;
    private Action onGameOver = null;

    private int TotalPlayers { get { return players.Count; } }
    private Player CurrentPlayer { get { return players[currentTurnPlayer]; } }

    private void IterateCurrentPlayer()
    {
        currentTurnPlayer++;
        if (currentTurnPlayer >= TotalPlayers)
            currentTurnPlayer = 0;
    }

    private void GiveTurnToNextPlayer()
    {
        CurrentPlayer.EndPlayersTurn();
        CurrentPlayer.ClearListeners();
        IterateCurrentPlayer();
        CurrentPlayer.SetPlayersTurn();
        CurrentPlayer.SetListeners(OnDiceRolled);
    }

    private void OnDiceRolled(int diceRoll)
    {
        bool isRollToGetPawnOut = rulesManager.IsHighlightPawnsInStart(diceRoll);
        switch (rulesManager.CheckRulesOnDiceRoll(diceRoll))
        {
            case RulesManager.DiceRollStates.Highlight:
                if (isRollToGetPawnOut)
                    CurrentPlayer.HighlightAllPawnsInStart(GetSelectedPawnOutOfStart);
                CurrentPlayer.HighlightPawnsInOpenTraveledMax(rulesManager.GetTilesTraveledMaxToAllowMove(diceRoll),
                    (pawnID) => { MoveSelectedPawn(pawnID, diceRoll); });
                break;
            case RulesManager.DiceRollStates.Move: // Will always be only one move possible
                if (isRollToGetPawnOut)
                    CurrentPlayer.GetLastPawnOutOfStart(OnMoveEnded);
                else
                    CurrentPlayer.MakeOnlyPossibleMove(diceRoll, OnMoveEnded);
                break;
            case RulesManager.DiceRollStates.EndTurn:
                EndTurnAfter(Constants.DiceRoll.WaitForDiceDisplayDuration);
                break;
        }
    }

    private void OnMoveEnded(Pawn.PawnID pawnID)
    {
        switch (rulesManager.CheckRulesOnMoveEnd(pawnID))
        {
            case RulesManager.PawnMoveStates.EndTurn:
                EndTurnAfter(Constants.DiceRoll.WaitForDiceDisplayDuration);
                break;
            case RulesManager.PawnMoveStates.RollDice:
                CurrentPlayer.GainedExtraDiceThrow();
                break;
            case RulesManager.PawnMoveStates.KillPawn:
                // TEMP ------------
                EndTurnAfter(Constants.DiceRoll.WaitForDiceDisplayDuration);
                // -----------------
                break;
            case RulesManager.PawnMoveStates.GameOver:
                if (onGameOver != null)
                    onGameOver.Invoke();
                break;
        }
    }

    private void GetSelectedPawnOutOfStart(Pawn.PawnID pawnID) => CurrentPlayer.GetPawnOutOfStart(pawnID, OnMoveEnded);
    private void MoveSelectedPawn(Pawn.PawnID pawnID, int diceRoll) => CurrentPlayer.MovePawn(pawnID, diceRoll, OnMoveEnded);
    private void EndTurnAfter(float secs) => delayManager.WaitForDuration(secs, EndTurnImmediately);
    private void EndTurnImmediately()
    {
        GiveTurnToNextPlayer();
        rulesManager.ChangeTurn(CurrentPlayer);
        Debugger.Log("[PlayerManager] Player turn changed to " + CurrentPlayer.ToString());
    }
}
