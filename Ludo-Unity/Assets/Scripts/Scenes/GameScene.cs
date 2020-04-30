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
    [SerializeField]
    private UITileManager uiTileManager = null;

    private Board board = null;
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
        board = new Board();
        playerManager = new PlayerManager(delayManager);
        TileManager.CreateTileManager();
        Debugger.CreateDebugger();
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
                hud.ShowSelectPawnPopup(onPawnSelected: CreateBoardWithPawnSelected);
                gameState = GameState.WaitingForPawnSelection;
                break;
            case GameState.InitGameBoard:
                playerManager.StartPlay(onGameOver: OnGameOver);
                gameState = GameState.GameInProgress;
                break;
            case GameState.GameInProgress:
                playerManager.Update();
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

    private void OnGameOver()
    {
        string[] playerNames = playerManager.GetPlayerNamesByRank();
        string player1Name = playerNames.Length > 0 ? playerNames[0] : "";
        string player2Name = playerNames.Length > 1 ? playerNames[1] : "";
        hud.ShowGameOverPopup(player1Name, player2Name, onPopupClose: GotoMainMenu);
        gameState = GameState.GameOver;
    }

    private void CreateBoardWithPawnSelected(LudoType pawn)
    {
        LudoType player1Type = pawn;
        LudoType player2Type = (LudoType)(((int)pawn + 2) % (int)LudoType.END);
        Debugger.Log("PlayerSelectedPawn player1Type:" + player1Type + " player2Type:" + player2Type);
        CreateTwoPlayerBoard(player1Type, player2Type, "You", "Opponent");
        TileManager.Instance.SetBottomLeftPlayer(player1Type);
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
        boardPlayerUI.SetUITileManager(uiTileManager);
        playerManager.CreateLocalPlayer(playerType, playerName, boardPlayer, boardPlayerUI);
    }

    private void GotoMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenuSceneName);
    }
}
