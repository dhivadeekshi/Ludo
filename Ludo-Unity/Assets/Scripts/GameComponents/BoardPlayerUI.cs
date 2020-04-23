using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BoardPlayerUI : MonoBehaviour
{
    #region USER INTERFACE
    public void SetPlayerType(LudoType playerType)
    {
        for (int i = 0; i < pawns.Length; i++)
            pawns[i].SetPawnType(playerType);
        this.playerType = playerType;
    }

    public void SetPlayerName(string playerName)
    {
        playerNameText.text = playerName;
    }

    public void DisplayDice()
    {
        diceUI.gameObject.SetActive(true);
    }

    public void HideDice()
    {
        diceUI.gameObject.SetActive(false);
    }

    public void EnableDice(UnityAction onDiceTapped)
    {
        diceUI.EnableInteraction();
        diceUI.SetListeners(onDiceTapped);
    }

    public void DisableDice()
    {
        diceUI.DisableInteraction();
        diceUI.ClearListeners();
    }

    public void RollDice(UnityAction onAnimationEnded)
    {
        diceUI.RollDice(onAnimationEnded);
    }

    public void DisplayDiceFace(int face)
    {
        diceUI.DisplayFace(face);
    }

    public void ResetDice()
    {
        diceUI.Reset();
    }

    public void MovePawn(int pawnIndex, Vector2 position, UnityAction onMoveCompleted)
    {
        pawns[pawnIndex].MoveToPosition(position, onMoveCompleted);
    }



    #endregion

    #region INTERNALS
    [SerializeField]
    private Text playerNameText = null;
    [SerializeField]
    private DiceUI diceUI = null;
    [SerializeField]
    private PawnUI[] pawns = null;

    public LudoType playerType { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        playerType = LudoType.Red;
        HideDice();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion
}
