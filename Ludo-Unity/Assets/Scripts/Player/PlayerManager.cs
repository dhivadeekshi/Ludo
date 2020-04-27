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

    public void CreateLocalPlayer(LudoType playerType, BoardPlayer boardPlayer, BoardPlayerUI boardPlayerUI)
    {
        var player = new LocalPlayer(playerType, boardPlayer, boardPlayerUI);
        rulesManager.AddPlayer(player);
        players.Add(player);
    }

    // Temp disabled, waiting for server implementation
    /*public*/private void CreateOnlinePlayer(LudoType playerType)
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
        switch (gamePlayState)
        {
            case GamePlayState.WaitingForPlayerInteraction:
                break;
            case GamePlayState.HighlightPawns:
                HighlightPawns();
                break;
            case GamePlayState.MovePawns:
                gamePlayState = GamePlayState.WaitingForPlayerInteraction;
                OnMoveEnded();
                break;
            case GamePlayState.TurnComplete:
                GiveTurnToNextPlayer();
                rulesManager.ChangeTurn(CurrentPlayer);
                gamePlayState = GamePlayState.WaitingForPlayerInteraction;
                break;
        }
    }

    public string[] GetPlayerNamesByRank()
    {
        string[] playerNames = new string[TotalPlayers];
        // TODO update playerNames by rank
        return playerNames;
    }


    private enum GamePlayState
    {
        None,
        WaitingForPlayerInteraction,
        RollDice,
        HighlightPawns,
        MovePawns,
        TurnComplete
    }

    private List<Player> players = new List<Player>();
    private GamePlayState gamePlayState = GamePlayState.None;
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
        switch(rulesManager.CheckRulesOnDiceRoll(diceRoll))
        {
            case RulesManager.DiceRollStates.Highlight:
                if (rulesManager.IsHighlightPawnsInStart(diceRoll))
                    CurrentPlayer.HighlightAllPawnsInStart();
                CurrentPlayer.HighlightPawnsInOpenTraveledMax(rulesManager.GetTilesTraveledMaxToAllowMove(diceRoll));
                gamePlayState = GamePlayState.HighlightPawns;
                break;
            case RulesManager.DiceRollStates.Move: // Will always be only one move possible
                var pawns = CurrentPlayer.GetAllPawnsInStart();
                if (pawns != null) CurrentPlayer.GetAPawnOutOfStart();
                gamePlayState = GamePlayState.MovePawns;
                break;
            case RulesManager.DiceRollStates.EndTurn:
                EndTurnAfter(Constants.WaitForDiceDisplayDuration);
                break;
        }   
    }

    private void OnMoveEnded()
    {
        switch(rulesManager.CheckRulesOnMoveEnd(Pawn.PawnID.nullID)) // TODO need to pass the id of the pawn moved
        {
            case RulesManager.PawnMoveStates.EndTurn:
                break;
            case RulesManager.PawnMoveStates.RollDice:
                break;
            case RulesManager.PawnMoveStates.KillPawn:
                break;
            case RulesManager.PawnMoveStates.GameOver:
                if (onGameOver != null)
                    onGameOver.Invoke();
                gamePlayState = GamePlayState.None;
                break;
        }
        // TEMP ------------
        EndTurnAfter(Constants.WaitForDiceDisplayDuration);
        // -----------------
    }

    private void HighlightPawns()
    {
        gamePlayState = GamePlayState.HighlightPawns;
    }

    private void EndTurnAfter(float secs)
    {
        delayManager.WaitForDuration(secs, () => { gamePlayState = GamePlayState.TurnComplete; });
    }

}
