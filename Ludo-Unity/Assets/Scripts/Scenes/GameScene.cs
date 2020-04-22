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

    private enum GameState
    {
        SelectPawn,
        WaitingForPawnSelection,
        PawnSelected,
        GameInProgress,
        GameWon,
        GameLoss,
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
                hud.ShowSelectPawnPopup(PawnSelected);
                gameState = GameState.WaitingForPawnSelection;
                break;
            case GameState.PawnSelected:
                break;
               
        }
    }

    public void Back()
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

    private void PawnSelected(LudoType pawn)
    {
        Debug.Log("Pawn Selected:" + pawn);
        InitGameBoard(pawn);
        gameState = GameState.PawnSelected;
    }

    private void InitGameBoard(LudoType playerTypeSelected)
    {
        boardUI.InitBoard(playerTypeSelected);
    }

    private void GotoMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenuSceneName);
    }

}
