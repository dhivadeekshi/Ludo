using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    [SerializeField]
    private HUD hud = null;
    [SerializeField]
    private BoardUI boardUI = null;
    [SerializeField]
    private DelayManager delayManager = null;

    private Board board = new Board();
    private PlayerManager playerManager = null;

    private enum GameState
    {
        SelectPawn,
        WaitingForPawnSelection,
        InitGameBoard,
        GameInProgress,
        GameOver,
        QuttingGame
    }

    private GameState gameState = GameState.SelectPawn;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = new PlayerManager(delayManager);
        SetListeners();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            default: // All Waiting states
                break;
            case GameState.SelectPawn:
                hud.ShowSelectPawnPopup(onPawnSelected:PlayerSelectedPawn);
                gameState = GameState.WaitingForPawnSelection;
                break;
            case GameState.InitGameBoard:
                playerManager.StartPlay();
                gameState = GameState.GameInProgress;
                break;
            case GameState.GameInProgress:
                playerManager.Update();
                if(playerManager.IsGameOver())
                {
                    string[] playerNames = playerManager.GetPlayerNamesByRank();
                    string player1Name = playerNames.Length > 0 ? playerNames[0] : "";
                    string player2Name = playerNames.Length > 1 ? playerNames[1] : "";
                    hud.ShowGameOverPopup(player1Name, player2Name, onPopupClose: GotoMainMenu);
                    gameState = GameState.GameOver;
                }
                break;
        }
    }

    private void Back()
    {
        // TODO quit the game and go back properly
        // TEMP ---------
        if (gameState == GameState.SelectPawn || gameState == GameState.WaitingForPawnSelection)
        {
            hud.HideSelectPawnPopup();
            GotoMainMenu();
        }
        else if (!hud.IsPopupActive())
            hud.ShowGameQuitPopup(onConfirm:GotoMainMenu);
        // --------------
    }

    private void SetListeners()
    {
        hud.SetOnBackListener(Back);
    }

    private void PlayerSelectedPawn(LudoType pawn)
    {
        LudoType player1Type = pawn;
        LudoType player2Type = (LudoType)(((int)pawn + 2) % (int)LudoType.END);
        Debug.Log("PlayerSelectedPawn player1Type:" + player1Type + " player2Type:" + player2Type);
        CreateTwoPlayerBoard(player1Type, player2Type, "You", "Opponent");
        gameState = GameState.InitGameBoard;
    }

    private void CreateTwoPlayerBoard(LudoType player1Type, LudoType player2Type, string player1Name, string player2Name)
    {
        boardUI.InitTwoPlayerBoard(player1Type, player1Name, player2Name);
        CreateLocalPlayer(player1Type, player1Name);
        CreateLocalPlayer(player2Type, player2Name);
    }

    private void CreateLocalPlayer(LudoType playerType, string playerName)
    {
        BoardPlayerUI boardPlayerUI = boardUI.GetPlayer(playerType);
        BoardPlayer boardPlayer = board.CreatePlayer(playerType);
        playerManager.CreateLocalPlayer(playerType, boardPlayer, boardPlayerUI);
    }

    private void GotoMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenuSceneName);
    }
}
