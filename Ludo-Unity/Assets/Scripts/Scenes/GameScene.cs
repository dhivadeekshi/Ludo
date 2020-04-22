﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{

    [SerializeField]
    private DiceUI diceUI = null;
    [SerializeField]
    private HUD hud = null;

    private Dice dice = new Dice();


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
        diceUI.SetOnRollDiceListener(RollDice);
        diceUI.SetDiceRollAnimationEndedListener(DiceRolled);
        hud.SetOnBackListener(Back);
        
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

    public void RollDice()
    {
        diceUI.RollDice();
    }


    public void DiceRolled()
    {
        int face = dice.RollDice();
        Debug.Log("Dice Rolled " + face);
        diceUI.DisplayFace(face);
    }


    private void PawnSelected(LudoType pawn)
    {
        Debug.Log("Pawn Selected:" + pawn);
        // TODO Update board
        gameState = GameState.PawnSelected;
    }

    private void GotoMainMenu()
    {
        SceneManager.LoadScene(SceneNames.MainMenuSceneName);
    }

}