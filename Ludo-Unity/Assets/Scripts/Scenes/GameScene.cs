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

    private Board board = null;

    private enum GameState
    {
        SelectPawn,
        WaitingForPawnSelection,
        InitGameBoard,
        GameInProgress,
        GameOver,
        QuitGame,
        DiceRolling,
        MovePawn
    }

    private GameState gameState = GameState.SelectPawn;

    // Start is called before the first frame update
    void Start()
    {
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
        board = new Board(player1Type, player2Type);
    }

    private void GotoMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenuSceneName);
    }

}
