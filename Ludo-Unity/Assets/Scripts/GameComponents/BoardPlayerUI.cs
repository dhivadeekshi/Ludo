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

    public void EnableDice()
    {
        ShowDice();
    }

    public void DisableDice()
    {
        HideDice();
    }

    public void RollDice()
    {
        diceUI.RollDice();
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

    private LudoType playerType = LudoType.Red;

    // Start is called before the first frame update
    void Start()
    {
        HideDice();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ShowDice()
    {
        diceUI.gameObject.SetActive(true);
    }

    private void HideDice()
    {
        diceUI.gameObject.SetActive(false);
    }

    #endregion
}
