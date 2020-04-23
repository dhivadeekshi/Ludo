using System.Collections;
using System.Collections.Generic;

public class PlayerManager
{
    public PlayerManager(DelayManager delayManager)
    {
        this.delayManager = delayManager;
    }

    public void CreateLocalPlayer(LudoType playerType, BoardPlayer boardPlayer, BoardPlayerUI boardPlayerUI)
    {
        var player = new LocalPlayer(playerType, boardPlayer, boardPlayerUI);
        players.Add(player);
    }

    // Temp disabled, waiting for server implementation
    /*public*/private void CreateOnlinePlayer(LudoType playerType)
    {
        var player = new OnlinePlayer(playerType);
        players.Add(player);
    }
    
    public void StartPlay()
    {
        CurrentPlayer.SetPlayersTurn();
        CurrentPlayer.SetListeners(OnDiceRolled);
    }


    public void Update()
    { 
        switch (gamePlayState)
        {
            case GamePlayState.WaitingForPlayerInteraction:
                break;
            case GamePlayState.HighlightPawns:
                gamePlayState = GamePlayState.MovePawns;
                break;
            case GamePlayState.MovePawns:
                gamePlayState = GamePlayState.TurnComplete;
                break;
            case GamePlayState.TurnComplete:
                GiveTurnToNextPlayer();
                gamePlayState = GamePlayState.WaitingForPlayerInteraction;
                break;
        }
    }

    public bool IsGameOver()
    {
        return CurrentPlayer.IsPlayerWon();
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
        if (CurrentPlayer.HasPossibleMove(diceRoll))
        {
            if (CurrentPlayer.HasMoreThanOnePossibleMove())
                gamePlayState = GamePlayState.HighlightPawns;
            else
                gamePlayState = GamePlayState.MovePawns;
        }
        else
            delayManager.WaitForDuration(Constants.WaitForDiceDisplayDuration, () => { gamePlayState = GamePlayState.TurnComplete; });
    }

}
