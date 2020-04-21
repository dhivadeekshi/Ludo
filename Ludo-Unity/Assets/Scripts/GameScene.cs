using System.Collections;
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
    }

    public void Back()
    {
        // TODO quit the game and go back properly
        // TEMP ---------
        SceneManager.LoadScene(SceneNames.MainMenuSceneName);
        // --------------
    }

    public void RollDice()
    {
        diceUI.RollDice();
    }


    public void DiceRolled()
    {
        int face = dice.RollDice();
        Debug.Log("Dice Rolled "+face);
        diceUI.DisplayFace(face);
    }

}
